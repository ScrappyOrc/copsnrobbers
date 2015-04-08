using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float forwardMovement = Input.GetAxis("Vertical");
		float horizontalMovement = Input.GetAxis("Horizontal");

		float jump = Input.GetAxis("Jump");

		Debug.Log("Vertical: " + forwardMovement + ", Horizontal: " + horizontalMovement + ", Jump: " + jump);

		if( horizontalMovement > 0.5f )
		{

		}

	}
}
