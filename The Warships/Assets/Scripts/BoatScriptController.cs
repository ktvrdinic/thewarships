using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatScriptController : MonoBehaviour
{

    public float speedMovement = 10.0f;
    public float speedSteer = 1.0f;
    public float movementTresold = 9.0f;
    public float steerTresold = 8.0f;
    public float windForce;
    private bool windMode = false;

    private Rigidbody rb;

    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Balance();
        ShipMovement();
    }

    void ShipMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        movementFactor = Mathf.Lerp(movementFactor, verticalInput / 4, Time.deltaTime / movementTresold);

        // U slucaju da brod ide unatrag
        if (verticalInput < 0) steerFactor = Mathf.Lerp(steerFactor, (movementFactor < 0) ? -horizontalInput / 2f : 0, Time.deltaTime / steerTresold);
        else steerFactor = Mathf.Lerp(steerFactor, (movementFactor > 0) ? horizontalInput / 2f : 0, Time.deltaTime / steerTresold);

        transform.Translate(0, 0, movementFactor * speedMovement);
        // Debug.Log("Move: " + movementFactor + ", Steer: " + steerFactor + ", Movefactor*speed: " + movementFactor * speedMovement);
        transform.Rotate(0, steerFactor * speedSteer, 0);
    }

    public void WindSpeedShip()
    {
        rb.AddRelativeForce(Vector3.forward * windForce,ForceMode.Acceleration);
        Debug.Log("Velocity: " + rb.velocity);
    }
}