using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Camera attachedCamera;
    [Header("Orbit")]
    public float xSpeed = 120f, ySpeed = 120f;
    public float yMinLimit = -20f, yMaxLimit = 80f;
    [Header("Collision")]
    
    public bool cameraCollision = true;//Is camera collision enabled?
    public bool ignoreTriggers = true;//Will the Spherecast ignore trigger?
    public float castRadius = .3f;//Radius of sphere to cast
    public float castDistance = 1000f;  //Distance the cast travels
    private float originalDistance;//Record starting distance of the camera
    private float distance;//Current Distance of camera
    private float x, y;// X and Y mouse rotation
    public LayerMask hitLayers;//Layer that casting will hit



	// Use this for initialization
	void Start ()
    {
        //Set original distance
        originalDistance = Vector3.Distance(transform.position, attachedCamera.transform.position);
        //Set X and Y degrees to current camera rotation
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;


	}
	
	// Update is called once per frame
	void Update ()
    {
		// is right mose button pressed?
        if (Input.GetMouseButton(1))
        {
            //Disable Cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Orbit with input
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            //Restricting the Y limit
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            //Rotate the transform using Euler angles
            transform.rotation = Quaternion.Euler(y, x, 0);


        }
        else
        {
            //Enable Cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
	}

    void FixedUpdate()

    {
        //Set Distance to original distance
        distance = originalDistance;
        //Change distance to what we hit

        //Is camera collision enabled?
        if (cameraCollision)
        {
            //Create a ray starting from orbit poisiton and going in the direction of the camera
            Ray camRay = new Ray(transform.position, -transform.forward);
            RaycastHit hit;//Stores the hit information after cast
            //Shoot a sphere behind the camera
            if (Physics.SphereCast(camRay,//Ray in the direction of camera
                castRadius, //How thicc the sphere is
                out hit, // The hit information collected
                castDistance,//How far the cast goes
                hitLayers,//What layers we're allowed to hit
                ignoreTriggers ? // ignore triggers?
                QueryTriggerInteraction.Ignore//Ignore it
                ://Else
                QueryTriggerInteraction.Collide))//Don't ignore
            {
                //Set distance to distance of hit
                distance = hit.distance;

            }
        }
        //Apply distance to cameras
        attachedCamera.transform.position = transform.position - transform.forward * distance;

    }
}
