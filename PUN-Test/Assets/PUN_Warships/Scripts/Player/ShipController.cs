using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;

public class ShipController : MonoBehaviour
{
    // Whether this ship is controlled by player input
    public bool controlledByInput = false;

    /// <summary>
    /// Raycast points used to determine if the ship has hit shallow water.
    /// </summary>

    public Transform[] raycastPoints;

    /// <summary>
    /// Mask to use when raycasting.
    /// </summary>

    public LayerMask raycastMask;

    // Left/right, acceleration
    Vector2 mInput = Vector2.zero;
    Vector2 mSensitivity = new Vector2(6f, 1f);

    float mSpeed = 0f;
    float mSteering = 0f;
    float mTargetSpeed = 0f;
    float mTargetSteering = 0f;

    Transform mTrans;
    GameShip mStats;
    public Cannon[] mCannonsLeft, mCannonsRight;



    public RPGCamera RPGcamera;

    /// <summary>
    /// For controlling the ship via external means (such as AI)
    /// </summary>

    public Vector2 input { get { return mInput; } set { mInput = value; } }

    /// <summary>
    /// Current speed (0-1 range)
    /// </summary>

    public float speed { get { return mSpeed; } }

    /// <summary>
    /// Current steering value (-1 to 1 range)
    /// </summary>

    public float steering { get { return mSteering; } }

    /// <summary>
    /// Helper function that finds the ship control script that contains the specified child in its transform hierarchy.
    /// </summary>

    static public ShipController Find(Transform trans)
    {
        while (trans != null)
        {
            ShipController ctrl = trans.GetComponent<ShipController>();
            if (ctrl != null) return ctrl;
            trans = trans.parent;
        }
        return null;
    }

    /// <summary>
    /// Cache the transform
    /// </summary>
    PhotonView photonView;
    public BoxCollider boxCollider;
    void Start()
    {
        mTrans = transform;
        mStats = GetComponent<GameShip>();
        //mCannons = GetComponentsInChildren<Cannon>();

        photonView = GetComponent<PhotonView>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    /// <summary>
    /// Update the input values, calculate the speed and steering, and move the transform.
    /// </summary>

    void Update()
    {
        if (!photonView.isMine || mStats.isDead) return;

        // Update the input values if controlled by the player
        if (controlledByInput) UpdateInput();

        bool shallowWater = false;

        // Determine if the ship has hit shallow water
        if (raycastPoints != null)
        {
            foreach (Transform point in raycastPoints)
            {
                if (Physics.Raycast(point.position + Vector3.up * 10f, Vector3.down, 10f, raycastMask))
                {
                    shallowWater = true;
                    Debug.Log("ShallowWater!!!");
                    break;
                }
            }
        }

        // Being in shallow water immediately cancels forward-driving input
        if (shallowWater) mInput.y = 0f;
        float delta = Time.deltaTime;

        // Slowly decay the speed and steering values over time and make sharp turns slow down the ship.
        mTargetSpeed = Mathf.Lerp(mTargetSpeed, 0f, delta * (0.5f + Mathf.Abs(mTargetSteering)));
        mTargetSteering = Mathf.Lerp(mTargetSteering, 0f, delta * 3f);

        // Calculate the input-modified speed
        mTargetSpeed = shallowWater ? 0f : Mathf.Clamp01(mTargetSpeed + delta * mSensitivity.y * mInput.y);
        mSpeed = Mathf.Lerp(mSpeed, mTargetSpeed, Mathf.Clamp01(delta * (shallowWater ? 8f : 5f)));

        // Steering is affected by speed -- the slower the ship moves, the less maneuverable is the ship
        mTargetSteering = Mathf.Clamp(mTargetSteering + delta * mSensitivity.x * mInput.x * (0.1f + 0.9f * mSpeed), -1f, 1f);
        mSteering = Mathf.Lerp(mSteering, mTargetSteering, delta * 5f);

        // Move the ship
        mTrans.localRotation = mTrans.localRotation * Quaternion.Euler(0f, mSteering * delta * mStats.turningSpeed, 0f);
        mTrans.localPosition = mTrans.localPosition + mTrans.localRotation * Vector3.forward * (mSpeed * delta * mStats.movementSpeed);
    }

    /// <summary>
    /// Update the input (used when ship is controlled by the player).
    /// </summary>

    void UpdateInput()
    {
        mInput.y = Mathf.Clamp01(Input.GetAxis("Vertical"));
        mInput.x = Input.GetAxis("Horizontal");



        // Fire the cannons
        if (mCannonsLeft != null && mCannonsRight != null && (Input.GetMouseButtonDown(1)))
        {
            Vector3 dir = RPGcamera.m_LocalForwardVector;  //GameCamera.flatDirection;
            Vector3 left = mTrans.rotation * Vector3.left;
            Vector3 right = mTrans.rotation * Vector3.right;

            left.y = 0f;
            right.y = 0f;

            left.Normalize();
            right.Normalize();

            // Calculate the maximum firing range using the best available cannon
            float maxRange = 500f;
            ///
            /*  foreach (Cannon cannon in mCannonsLeft)
              {
                  float range = cannon.maxRange;
                  if (range > maxRange) maxRange = range;
              }
              */

            // Aim and fire the cannons on each side of the ship, force-firing if the camera is looking that way
            // AimAndFire(left, maxRange, Vector3.Angle(dir, left) < 60f);
            /// AimAndFire(right, maxRange, Vector3.Angle(dir, right) < 60f);
            /// 




            float camRotY = RPGcamera.transform.localEulerAngles.y;
            Debug.Log("Angle: " + camRotY);
            if (camRotY < 330 && camRotY > 210)
            {
                Fire(left, maxRange, true);
            }
            else if (camRotY > 30 && camRotY < 150)
            {
                Fire(right, maxRange, false);
            }




        }

        // Debug.Log("Camera Angle: " +  RPGcamera.transform.forward);

    }

    /// <summary>
    /// Aim and fire the cannons given the specified direction and maximum range.
    /// </summary>

    void AimAndFire(Vector3 dir, float maxRange, bool forceFire)
    {
        float distance = maxRange * 1.2f;
        //GameUnit gu = GameUnit.Find(mStats, dir, distance, 60f);

        // If a unit was found, override the direction and angle
        /*  if (gu != null)
          {
              dir = gu.transform.position - mTrans.position;
              distance = dir.magnitude;
              if (distance > 0f) dir *= 1.0f / distance;
              else distance = maxRange;

              // Fire in the target direction
              Fire(dir, distance);
          }
          else */
        if (forceFire)
        {
            // No target found -- only fire if asked to
            Fire(dir, distance, false);
        }
    }

    /// <summary>
    /// Fire the ship's cannons in the specified direction.
    /// </summary>

    public void Fire(Vector3 dir, float distance, bool leftSide)
    {
        if (mCannonsLeft != null && mCannonsRight != null)
        {
            if (leftSide)
            {
                foreach (Cannon cannon in mCannonsLeft)
                {
                    cannon.Fire(dir, distance);
                }
            }
            else
            {
                foreach (Cannon cannon in mCannonsRight)
                {
                    cannon.Fire(dir, distance);
                }
            }

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + ":  hit!!");

        Cannonball cb = collision.gameObject.GetComponent<Cannonball>();

        if (cb != null && cb.damage > 0f && photonView.isMine)
        {
            float damage = mStats.ApplyDamageToSails(cb.damage);
            ///////photonView.RPC("ApplyDamageToSails", RpcTarget.All, cb.damage);


            //TextUPdate
            BattleManger.Instance.PlayerHealthText.text = mStats.sailHealth.x.ToString() + "/100";
            BattleManger.Instance.PlayerHealthSlider.value = mStats.sailHealth.x;
            photonView.RPC("MajVie", PhotonTargets.Others, mStats.sailHealth.x);

            if (mStats.sailHealth.x <= 0)
            {
                BattleManger.Instance.losePanel.SetActive(true);
                BattleManger.Instance.PlayersUI.SetActive(false);


                BattleManger.Instance.winner = BattleManger.Instance.EnemyNameText.text.ToString();
                photonView.RPC("BattleWinnerIs", PhotonTargets.Others);

                photonView.RPC("GameWin", PhotonTargets.Others);
                //BattleManger.Instance.winner = BattleManger.Instance.EnemyNameText.text.ToString();

                ///Debug.LogError("OnCollisionEnter.Winner is " + BattleManger.Instance.winner + " : MasterClient" + PhotonNetwork.isMasterClient);
                ///

            }

            //// cb.DestroyOnHit();



        }

    }

    [PunRPC]
    public void BattleWinnerIs()
    {
        //if(playerIndex == 1)
        //{

            BattleManger.Instance.winner = BattleManger.Instance.PlayerNameText.text.ToString();
            ///Debug.LogError("BattleWinnerIs.Winner is " + BattleManger.Instance.winner + " : MasterClient" + PhotonNetwork.isMasterClient);

    }




    [PunRPC]
    void MajVie(float vie)
    {
        BattleManger.Instance.EnemyHealthText.text = vie.ToString() + "/100";
        BattleManger.Instance.EnemyHealthSlider.value = vie;
        Debug.Log("Hit!!!");
    }

    [PunRPC]
    void GameWin()
    {
        BattleManger.Instance.winPanel.SetActive(true);
        BattleManger.Instance.PlayersUI.SetActive(false);

        //StartCoroutine( InsertWinnerMySQL());
        //if (BattleManger.Instance.winner == BattleManger.Instance.PlayerNameText.text)// Ako je prvi igrac pobjedio
        //{
        //    InsertWinnerMySQL(0);
        //}
        //else if (BattleManger.Instance.winner == BattleManger.Instance.PlayerNameText.text) // Ako je drugi igrac pobjedio
        //{
        //    InsertWinnerMySQL(1);
        //}


    }
    //// NE BRISATI
    //IEnumerator InsertWinnerMySQL()
    //{
    //    WWWForm form = new WWWForm();

    //    form.AddField("username", DBManager.username);
    //    form.AddField("username_enemy", BattleManger.Instance.EnemyNameText.text); // Dodati neprijatelja
 

    //    WWW www = new WWW("http://localhost/theWarships/saveData.php", form);

    //    yield return www;
    //    if (www.text[0] == '0'){
    //        Debug.Log("Battle successfully inserted in MySQL.");
    //    }else{
    //        Debug.Log("Battle creation failed in MySQL. Error #" + www.text);
    //    }

    //}
}

