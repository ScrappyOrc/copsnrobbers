using UnityEngine;
using System.Collections;

public class Flee : Action
{
	GameObject target;
	float distance;
	NavMeshHit hit;

	/// <summary>
	/// Creates a Flee action that causes the target
	/// to flee the given GameObject
	/// </summary>
	/// <param name="target">GameObject to flee</param>
	/// <param name="distance">Distance to keep away from "target" </param>
	public Flee(GameObject target, float distance)
	{
		this.target = target;
		this.distance = distance;

		hit = new NavMeshHit();
	}
	
	/// <summary>
	/// Updates the target position of the character
	/// to follow the target GameObject.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character)
	{
		if (target != null) {
			if ((character.transform.position - target.transform.position).sqrMagnitude < distance * distance) {
				int rand = Random.Range (0, 4);
				Vector3 newPosition = character.transform.position;

				// find vector from me to target then move away by that vector*distance

				if (rand == 0) {
					newPosition.z += distance * 2;
				} else if (rand == 1) {
					newPosition.z -= distance * 2;
				} else if (rand == 2) {
					newPosition.x += distance * 2;
				} else {
					newPosition.x -= distance * 2;
				}

				//newPosition = new Vector3(-0.4319409f, 0.5093095f, -1.7f);

				int navLayer = NavMesh.GetNavMeshLayerFromName("Default");
	
				NavMesh.SamplePosition (newPosition, out hit, distance, 1);
				Debug.Log(hit.position);
				character.Agent.SetDestination (hit.position); 
				//character.Agent.SetDestination (newPosition);
				//character.Agent.Resume();
			} else {
				character.Agent.Stop ();
			}
		}
	}
}
