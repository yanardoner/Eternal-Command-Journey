using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float fastSpeed = 0.3f;
	public float cameraSpeed = .08f;
	public GameObject defaultCam;
	public float defaultSpeed = .08f;

	Vector2 _mouseAbsolute;
	Vector2 _smoothMouse;
	
	public Vector2 clampInDegrees = new Vector2(360, 180);
	public bool lockCursor;
	public Vector2 sensitivity = new Vector2(2, 2);
	public Vector2 smoothing = new Vector2(3, 3);
	public Vector2 targetDirection;

	private  bool cameraSwitch = false;

	private Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float heightOffset = 1f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public float rotationDamping = 10.0f;
	
	private void Start()
	{
		defaultSpeed = cameraSpeed;
		targetDirection = transform.localRotation.eulerAngles;
		target = GameObject.FindWithTag("Player").transform;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
		    if (!cameraSwitch)
			{
				cameraSwitch = true;
			}
		    else
			{
				cameraSwitch = false;
			}
		}

		if (cameraSwitch)
		{
			if (Input.GetKey(KeyCode.W))
				defaultCam.transform.position += cameraSpeed * transform.forward;
			
			if (Input.GetKey(KeyCode.S))
				defaultCam.transform.position += cameraSpeed * -transform.forward;
			
			if (Input.GetKey(KeyCode.A))
				defaultCam.transform.position += cameraSpeed * -transform.right;
			
			if (Input.GetKey(KeyCode.D))
				defaultCam.transform.position += cameraSpeed * transform.right;
			
			if (Input.GetKey(KeyCode.Q))
				defaultCam.transform.position += cameraSpeed * Vector3.up;
			
			if (Input.GetKey(KeyCode.E))
				defaultCam.transform.position += cameraSpeed * -Vector3.up;
			
			if (Input.GetKey(KeyCode.LeftShift))
			{
				cameraSpeed = fastSpeed;
			}
			else
			{
				cameraSpeed = defaultSpeed;
			}
		}
	}
}