using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float TRANSLATE_SPEED = 0.25f;
	public float ROTATE_SPEED = 0.2f;

	private Vector3 eulerAngle;

	// Use this for initialization
	void Start ()
	{
		eulerAngle = Quaternion.Euler(new Vector3(0.0f, 10.0f * ROTATE_SPEED, 0.0f)).eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 displacement = Vector3.zero;
		Quaternion rotation = Quaternion.identity;

		Vector3 forward = transform.forward;
		forward.Set(forward.x, 0, forward.z);
		forward = forward.normalized * TRANSLATE_SPEED;

		if (Input.GetKey (KeyCode.W)) {
			displacement = forward;
		} 
		if (Input.GetKey (KeyCode.S)) {
			displacement = -forward;
		} 
		if (Input.GetKey (KeyCode.A)) {
			displacement = -transform.right * TRANSLATE_SPEED;
		} 
		if (Input.GetKey (KeyCode.D)) {
			displacement = transform.right * TRANSLATE_SPEED;
		}
		if (Input.GetKey (KeyCode.Q)) {
			rotation = Quaternion.Euler(-eulerAngle);
		} 
		if (Input.GetKey (KeyCode.E)) {
			rotation = Quaternion.Euler(eulerAngle);
		}
		

		gameObject.transform.position += displacement;
		gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + rotation.eulerAngles);
	}
}
