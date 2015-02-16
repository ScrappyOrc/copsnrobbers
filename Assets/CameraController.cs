using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	Vector3 displacement;
	Quaternion rotation;
	Vector3 eulerAngle;
	Vector3 forward;
	Vector3 right;

	// Use this for initialization
	void Start ()
	{
		displacement = Vector3.zero;
		rotation = Quaternion.identity;
		eulerAngle = Quaternion.Euler(new Vector3(0.0f, 10.0f, 0.0f)).eulerAngles;
		forward = Quaternion.Euler (-1.0f * gameObject.transform.rotation.eulerAngles) * gameObject.transform.forward;
		right = gameObject.transform.right;
	}
	
	// Update is called once per frame
	void Update ()
	{
		displacement = Vector3.zero;
		rotation = Quaternion.identity;

		if (Input.GetKeyDown (KeyCode.W)) {
			displacement = forward;
		} else if (Input.GetKeyDown (KeyCode.S)) {
			displacement = -1.0f * forward;
		} else if (Input.GetKeyDown (KeyCode.A)) {
			displacement = -1.0f * right;
		} else if (Input.GetKeyDown (KeyCode.D)) {
			displacement = right;
		} else if (Input.GetKeyDown (KeyCode.Q)) {
			rotation = Quaternion.Euler(-1.0f * eulerAngle);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			rotation = Quaternion.Euler(eulerAngle);
		}
		

		gameObject.transform.position += displacement;
		gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + rotation.eulerAngles);
	}
}
