using UnityEngine;
using System.Collections;

public class MouseLooker : MonoBehaviour {

	// Use this for initialization
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	float mouseX, mouseY;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;

	public float MinimumY = -90F;
	public float MaximumY = 90F;

	public bool smooth;
	public float smoothTime = 5f;
	
	// internal private variables
	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;
	private Transform character;
	private Transform cameraTransform;

	void Start() {
		// start the game with the cursor locked
		LockCursor (true);

		// get a reference to the character's transform (which this script should be attached to)
		character = gameObject.transform;

		// get a reference to the main camera's transform
		cameraTransform = Camera.main.transform;

		// get the location rotation of the character and the camera
		m_CharacterTargetRot = character.localRotation;
		m_CameraTargetRot = cameraTransform.localRotation;
	}
	
	void Update() {
		// rotate stuff based on the mouse
		LookRotation ();

		// if ESCAPE key is pressed, then unlock the cursor



		if(Input.GetButtonDown("Cancel")){
			LockCursor (false);
		}

		// if the player fires, then relock the cursor
		
	}
	
	private void LockCursor(bool isLocked)
	{
		if (isLocked) 
		{
			// make the mouse pointer invisible
			Cursor.visible = false;

			// lock the mouse pointer within the game area
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			// make the mouse pointer visible
			Cursor.visible = true;

			// unlock the mouse pointer so player can click on other windows
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void LookRotation()
	{
		//get the y and x rotation based on the Input manager
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		mouseX += Input.GetAxis("Mouse X") * XSensitivity;
		mouseY -= Input.GetAxis("Mouse Y") * YSensitivity;
		

		if (Input.GetKey(KeyCode.LeftAlt))
		{
			m_CameraTargetRot = Quaternion.Euler(0f, mouseX, 0f);
		}
        else
        {
			m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
			m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
		}

		// calculate the rotation
		

		// clamp the vertical rotation if specified
		if(clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

		// update the character and camera based on calculations
		if(smooth) // if smooth, then slerp over time
		{
			character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
			                                            smoothTime * Time.deltaTime);
			cameraTransform.localRotation = Quaternion.Slerp (cameraTransform.localRotation, m_CameraTargetRot,
			                                         smoothTime * Time.deltaTime);
		}
		else // not smooth, so just jump
		{
			character.localRotation = m_CharacterTargetRot;
			cameraTransform.localRotation = m_CameraTargetRot;
		}
	}
	
	// Some math ... eeck!
	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;
		
		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);

		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

		angleY = Mathf.Clamp(angleY, MinimumY, MaximumY);


		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);


		return q;
	}
}
