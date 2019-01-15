using UnityEngine;

public class CannonsRotate : MonoBehaviour {

    private float MaxRotUpDown = 8f;
    private float MaxRotRightLeft = 5f;
    private float RotSpeed = 8f;

    private bool left = false;
    private bool right = false;
    private bool up = false;
    private bool down = false;

    public GameObject cannonLeft;
    public GameObject cannonRight;
    public GameObject cannonBack;
    public GameObject cannonFront;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // MOZDA DOVESTI STATIC VARIJABLE U OVU KLASU
        if (left || right || up || down)
        {

            switch (InGameControls.currentCamera)
            {
                case 1: // Front camera
                    if (left == true && (cannonFront.transform.localEulerAngles.y > (360 - MaxRotRightLeft) || cannonFront.transform.localEulerAngles.y < MaxRotRightLeft+2))
                    { 
                        cannonFront.transform.Rotate(0, -RotSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (right == true && (cannonFront.transform.localEulerAngles.y > (358 - MaxRotRightLeft) || cannonFront.transform.localEulerAngles.y <  MaxRotRightLeft))
                    {
                        cannonFront.transform.Rotate(0, RotSpeed * Time.deltaTime, 0);
                        
                    } else if (up == true && (cannonFront.transform.localEulerAngles.x > (360 - MaxRotUpDown) || cannonFront.transform.localEulerAngles.x < MaxRotUpDown+2))
                    {                        
                        cannonFront.transform.Rotate(-RotSpeed * Time.deltaTime, 0, 0);
                        
                        
                    }
                    else if (down == true && (cannonFront.transform.localEulerAngles.x > (358 - MaxRotUpDown) || cannonFront.transform.localEulerAngles.x < MaxRotUpDown))
                    {
                        cannonFront.transform.Rotate(RotSpeed * Time.deltaTime, 0, 0);
                        
                    }
                    break;
                case 2: // Right camera
                    if (left == true && (cannonRight.transform.localEulerAngles.y > (360 - MaxRotRightLeft) || cannonRight.transform.localEulerAngles.y < MaxRotRightLeft+2))
                    {
                        cannonRight.transform.Rotate(0, -RotSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (right == true && (cannonRight.transform.localEulerAngles.y > (358 - MaxRotRightLeft) || cannonRight.transform.localEulerAngles.y < MaxRotRightLeft))
                    {
                        cannonRight.transform.Rotate(0, RotSpeed * Time.deltaTime, 0);
                       
                    }
                    else if (up == true && (cannonRight.transform.localEulerAngles.z > (358 - MaxRotUpDown) || cannonRight.transform.localEulerAngles.z < MaxRotUpDown))
                    {
                        cannonRight.transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
                    }
                    else if (down == true && (cannonRight.transform.localEulerAngles.z > (360 - MaxRotUpDown) || cannonRight.transform.localEulerAngles.z < MaxRotUpDown+2))
                    {
                        cannonRight.transform.Rotate(0, 0, -RotSpeed * Time.deltaTime);
                    }
                    break;
                case 3: // Back camera
                    if (left == true && (cannonBack.transform.localEulerAngles.y > (360 - MaxRotRightLeft) || cannonBack.transform.localEulerAngles.y < MaxRotRightLeft+2))
                    {
                        cannonBack.transform.Rotate(0, -RotSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (right == true && (cannonBack.transform.localEulerAngles.y > (358 - MaxRotRightLeft) || cannonBack.transform.localEulerAngles.y < MaxRotRightLeft))
                    {
                        cannonBack.transform.Rotate(0, RotSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (up == true && (cannonBack.transform.localEulerAngles.x > (358 - MaxRotUpDown) || cannonBack.transform.localEulerAngles.x < MaxRotUpDown))
                    {
                        cannonBack.transform.Rotate(RotSpeed * Time.deltaTime, 0, 0);
                        
                    }
                    else if (down == true && (cannonBack.transform.localEulerAngles.x > (360 - MaxRotUpDown) || cannonBack.transform.localEulerAngles.x < MaxRotUpDown + 2))
                    {
                        cannonBack.transform.Rotate(-RotSpeed * Time.deltaTime, 0, 0);
                        
                    }
                    break;
                case 4: // Left camera
                    if (left == true && (cannonLeft.transform.localEulerAngles.y > (360 - MaxRotRightLeft) || cannonLeft.transform.localEulerAngles.y < MaxRotRightLeft + 2))
                    {
                        cannonLeft.transform.Rotate(0, -RotSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (right == true && (cannonLeft.transform.localEulerAngles.y > (358 - MaxRotRightLeft) || cannonLeft.transform.localEulerAngles.y < MaxRotRightLeft))
                    {
                        cannonLeft.transform.Rotate(0, RotSpeed * Time.deltaTime, 0);
                        
                    }
                    else if (up == true && (cannonLeft.transform.localEulerAngles.z > (360 - MaxRotUpDown) || cannonLeft.transform.localEulerAngles.z < MaxRotUpDown + 2))
                    {
                        cannonLeft.transform.Rotate(0, 0, -RotSpeed * Time.deltaTime);
                        
                    }
                    else if (down == true && (cannonLeft.transform.localEulerAngles.z > (358 - MaxRotUpDown) || cannonLeft.transform.localEulerAngles.z < MaxRotUpDown))
                    {
                        cannonLeft.transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
                        
                    }
                    break;

                default:
                    Debug.Log("Ne mogu pronaci trenutni top.");
                    break;
            }

            // Zamijeniti necim optimiziranijim !!!
        }
        
        
    } // Treba Freezati x Rotaciju !!!

    public void StopRotations()
    {
        left = false;
        right = false;
        up = false;
        down = false;
    }

    public void ControlsOfCannon(int index)
    {
        // 0 - up, 1 - right, 2 - down, 3 - left
        if (index == 0) up = true;
        else if (index == 1) right = true;
        else if (index == 2) down = true;
        else if (index == 3) left = true;
    }
}
