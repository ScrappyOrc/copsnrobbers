using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a seek action where the character 
/// moves towards a given location
/// </summary>
public class Seek : Action 
{
	private Vector3 target;
	private bool done = false;
	private float distanceSq;

	/// <summary>
	/// Creates a Seek action from a GameObject containing the target position
	/// </summary>
	/// <param name="target">GameObject containing the target position</param>
	public Seek(GameObject target, float distance = 0.01f) 
	{
		this.target = target.transform.position;
		this.distanceSq = distance * distance;
	}

	/// <summary>
	/// Creates a Seek action from a transform containing the target position
	/// </summary>
	/// <param name="target">Transform containing the target position</param>
	public Seek(Transform target, float distance = 0.01f) 
	{
		this.target = target.position;
		this.distanceSq = distance * distance;
	}

	/// <summary>
	/// Creates a Seek action from a given vector coordinate
	/// </summary>
	/// <param name="target">Target position</param>
	public Seek(Vector3 target, float distance = 0.01f) 
	{
		this.target = target;
		this.distanceSq = distance * distance;
	}

	/// <summary>
	/// Updates the target of the character, moving them
	/// closer to the target position
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character) 
	{
		// Debug - let use see what the character is doing
		character.type = STEERING_TYPE.SEEK;

		if ((target - character.transform.position).sqrMagnitude < distanceSq) 
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
