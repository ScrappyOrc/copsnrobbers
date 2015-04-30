using UnityEngine;
using System.Collections;

/// <summary>
/// Action for fleeing away from another character
/// </summary>
public class StopCharacter : Action
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
	public StopCharacter(GameObject target)
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
		// Shouldn't ever happen, but just in case
		if (target == null) 
		{
			return;
		}

		// For now, just delete the robber from the game world when captured
		(character as Cop).Robber.Arrest ();
		(character as Cop).Robber.ForceAction (new Wander ());

		// After one run this action will be completed no matter what (COULD CHANGE LATER)
		done = true;
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
