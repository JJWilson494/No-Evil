using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;

    private Vector3 rotation = Vector3.zero;

    private float cameraRot = 0.0f;

    private float currCameraRotation = 0.0f;


    [SerializeField]
    private float cameraMaxRot = 85f;

    [SerializeField]
    private Camera cam;

    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        PerformMovement();
        PerformRotation();
	}

    public void Move (Vector3 velocity)
    {
        this.velocity = velocity;
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            currCameraRotation -= cameraRot;
            currCameraRotation = Mathf.Clamp(currCameraRotation, -cameraMaxRot, cameraMaxRot);

            cam.transform.localEulerAngles = new Vector3(currCameraRotation, 0.0f, 0.0f);
        }

    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void RotateCamera(float rotation)
    {
        cameraRot = rotation;
    }
}
