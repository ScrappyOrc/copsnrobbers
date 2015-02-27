using UnityEngine;
using System.Collections;

/// <summary>
/// Action where the character follows another character
/// </summary>
public class Follow : Action 
{
	private GameObject target;
	private float distance;
	private bool done = false;

	/// <summary>
	/// Creates a follow action that causes the target
	/// to follow the given GameObject
	/// </summary>
	/// <param name="target">GameObject to follow</param>
	/// <param name="distance">Distance to keep when following</param> 
	public Follow(GameObject target, float distance) 
	{
		this.target = target;
		this.distance = distance;
	}

	/// <summary>
	/// Updates the target position of the character
	/// to follow the target GameObject.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character) 
	{
		// Debug - let use see what the character is doing
		character.type = STEERING_TYPE.FOLLOW;

		if ((character.transform.position - target.transform.position).sqrMagnitude > distance * distance) 
		{
			character.Agent.SetDestination(target.transform.position);
		}
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
