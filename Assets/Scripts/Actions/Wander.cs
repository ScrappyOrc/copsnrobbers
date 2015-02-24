using UnityEngine;
using System.Collections;

/// <summary>
/// Action for wandering around randomly
/// </summary>
public class Wander : Action 
{
	private Vector3 target;
	private bool done = false;

	/// <summary>
	/// Creates a wander action that causes the target
	/// to pick a random location within range and move there
	/// </summary>
	/// <param name="character">the character that will be wandering</param>
    public Wander (Character character, float range) {
		target = character.transform.position + new Vector3(Random.Range(-range, range),
															0.61f,
															Random.Range(-range, range));
	}

	/// <summary>
	/// Updates the target of the character, moving them
	/// closer to the target position
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply (Character character) {
		if ((target - character.transform.position).sqrMagnitude < 0.001) 
		{
			done = true;
		} 
		else 
		{
			character.Agent.SetDestination (target);
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
