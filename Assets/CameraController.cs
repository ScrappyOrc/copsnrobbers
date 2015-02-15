using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	Vector3 displacement;
	Vector3 forward;
	Vector3 right;

	// Use this for initialization
	void Start ()
	{
		displacement = Vector3.zero;
		forward = Quaternion.Euler (-1.0f * gameObject.transform.rotation.eulerAngles) * gameObject.transform.forward;
		right = gameObject.transform.right;
	}
	
	// Update is called once per frame
	void Update ()
	{
		displacement = Vector3.zero;

		if (Input.GetKeyDown (KeyCode.W)) {
			displacement = forward;
		} else if (Input.GetKeyDown (KeyCode.S)) {
			displacement = -1.0f * forward;
		} else if (Input.GetKeyDown (KeyCode.A)) {
			displacement = -1.0f * right;
		} else if (Input.GetKeyDown (KeyCode.D)) {
			displacement = right;
		}

		gameObject.transform.position += displacement;
	}
}
