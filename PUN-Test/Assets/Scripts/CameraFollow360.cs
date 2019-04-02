using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow360 : MonoBehaviour
{
 
    
    public Transform target;

    public Camera camMain;

    [SerializeField]
    private float speedMouseScroll;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Vector3 _cameraOffset;

    [SerializeField]
    private float RotationsSpeed;

    [SerializeField]
    private float SmoothFactor;

    private void Start()
    {
        _cameraOffset = transform.position - target.transform.position;
        Refresh();
    }

    void Update()
    {
        Refresh();
    }

    public float AngleBetweenVectors()
    {
         Vector3 targetDir = this.transform.position - target.position;
         Vector3 forward = target.forward;
         
        //Vector3 targetDir = to.position - from.position;
        //Vector3 forward = from.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        return angle;
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);
            return;
        }

        // compute position
        
        transform.position = target.position + offsetPosition;

        if (Input.GetMouseButton(0))
        {
            Quaternion camTurnAnglex = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
            
            _cameraOffset = camTurnAnglex * _cameraOffset;
        }
        
        transform.position = Vector3.Slerp(transform.position, target.position + _cameraOffset, SmoothFactor);

        transform.LookAt(target);

        camMain.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * speedMouseScroll;
        if (camMain.fieldOfView < 60) camMain.fieldOfView = 60;
        else if (camMain.fieldOfView > 90) camMain.fieldOfView = 90;
    }
}

// Input.GetAxis("Mouse Y") - Dovrsiti kontrolu za y