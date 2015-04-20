using UnityEngine;
using System.Collections;

public class CitizenTrigger : MonoBehaviour {

	public Citizen citizen;

	void OnTriggerEnter(Collider other)
	{
		citizen.Trigger (other);
	}
}
