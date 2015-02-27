using UnityEngine;
using System.Collections;

/// <summary>
/// Action for fleeing away from another character
/// </summary>
public class Flee : Action
{
	private GameObject target;
	private float distance;
	private NavMeshHit hit;
	private bool done = false;

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

		// Shouldn't ever provide a null target, but just in case
		if (target == null) 
		{
			this.done = true;
		}
	}
	
	/// <summary>
	/// Updates the target position of the character
	/// to flee the target GameObject.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character)
	{
		// Debug - let use see what the character is doing
		character.type = STEERING_TYPE.FLEE;

		// Shouldn't ever happen, but just in case
		if (target == null) 
		{
			return;
		}

		// Keep fleeing while too close to the threat
		if ((character.transform.position - target.transform.position).sqrMagnitude < distance * distance)
		{
			// Don't need to update the target constantly
			if ((character.Agent.destination - character.transform.position).sqrMagnitude > 5) 
			{
				return;
			}

			// Calculate the vector between the character and its target
			Vector3 between = character.transform.position - target.transform.position;
			between.Normalize();

			// Use the vector between to give the character a new position
			Vector3 newPosition = character.transform.position + between * distance;

			// Sample the nav mesh and move the character to the new position
			NavMesh.SamplePosition (newPosition, out hit, 500, 1);
			character.Agent.SetDestination (hit.position); 
		}

		// Done fleeing when far enough away
		else
		{
			done = true;
		}
	}

	/// <summary>
	/// Checks whether or not the Action has been completed
	/// </summary>
	/// <returns>true if complete, false if still going</returns>
	public bool IsDone() 
	{
		return done;
	}
}
