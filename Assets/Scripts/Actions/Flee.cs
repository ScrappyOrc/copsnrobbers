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
	/// to flee the target GameObject.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character)
	{
		if (target != null)
		{
			if ((character.transform.position - target.transform.position).sqrMagnitude < distance * distance)
			{
				// Calculate the vector between the character and its target
				Vector3 between = character.transform.position - target.transform.position;
				between.Normalize();

				// Use the vector between to give the character a new position
				Vector3 newPosition = character.transform.position + between * 500;
	
				// Sample the nav mesh and move the character to the new position
				NavMesh.SamplePosition (newPosition, out hit, 500, 1);
				character.Agent.SetDestination (hit.position); 
			}
			else
			{
				character.Agent.Stop ();
			}
		}
	}
}
