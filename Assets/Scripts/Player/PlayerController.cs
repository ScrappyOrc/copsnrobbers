using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float JUMP_HEIGHT = 1.0f;

	public float TRANSLATE_SPEED = 0.25f;
	public float ROTATE_SPEED = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float forwardMovement = Input.GetAxis("Vertical");
		float horizontalMovement = Input.GetAxis("Horizontal");

		float flight = Input.GetAxis("Jump");

		//Debug.Log("Vertical: " + forwardMovement + ", Horizontal: " + horizontalMovement + ", Jump: " + jump);

		Vector3 displacement = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		
		Vector3 forward = transform.forward;
		forward.Set(forward.x, 0, forward.z);
		forward = forward.normalized * TRANSLATE_SPEED;

		//Debug.Log( "Forward: " + forward );

		if( horizontalMovement < -0.5f )
		{
			displacement += -transform.right * TRANSLATE_SPEED;
		}
		if( horizontalMovement > 0.5f )
		{
			displacement += transform.right * TRANSLATE_SPEED;
		}
		if( forwardMovement > 0.5f )
		{
			displacement += forward;
		}
		if( forwardMovement < -0.5f )
		{
			displacement += -forward;
		}

		if( flight > 0.5f )
		{
			displacement += transform.up * TRANSLATE_SPEED;
		}

		if( flight < -0.5f )
		{
			displacement -= transform.up * TRANSLATE_SPEED;
		}

		// Rotate camera based on mouse movement
		float h = 2.0f * Input.GetAxis("Mouse X");
		float v = 2.0f * Input.GetAxis("Mouse Y") * -1.0f;
		Vector3 hi = new Vector3(v, h, 0);
		gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + hi);
		
		gameObject.transform.position += displacement;

	}
}
