using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class FloatObjectScript : MonoBehaviour {
    /*
    public float waterLevel = 0.0f;
    public float floatTreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 4.0f ;

    float forceFactor;
    Vector3 floatForce;
*/
    /* public float maxSpeed;

     // Use this for initialization
     void Start () {

     }

     // Update is called once per frame
     void FixedUpdate () {
         maxSpeed = Mathf.Lerp(2f, -2f, 0.5f);
         transform.position = new Vector3(0, maxSpeed, 0);
         Debug.Log("Lerp: " + maxSpeed);
         /*forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatTreshold);

         Debug.Log("ForceFactor: " + forceFactor);
         if (forceFactor > 0.0f)
         {
             floatForce = -Physics.gravity * GetComponent<Rigidbody>().mass * (forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
             floatForce += new Vector3(0.0f, -downForce * GetComponent<Rigidbody>().mass, 0.0f);
             GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
             Debug.Log("ForceFactorIF: " + forceFactor);
         }
     }*/

    //public float degreesPerSecond = 15.0f;
    public float amplitude = 0.3f;
    public float frequency = 0.2f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3(0,0,0);
    Vector3 tempPos = new Vector3(0,0,0);

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        //transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos.y = posOffset.y;
        tempPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x,tempPos.y,transform.position.z);
    }
}
