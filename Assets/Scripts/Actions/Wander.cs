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
	/// <param name="blocks">Max deviation from the center of the city in blocks</param> 
    public Wander (int blocks = 4) 
	{
		target = City.GetRandomPoint(blocks);
	}

	/// <summary>
	/// Updates the target of the character, moving them
	/// closer to the target position
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply (Character character) 
	{
		if (character.Agent.destination != target)
		{
			character.Agent.SetDestination (target);
		}
		if (character is Cop) {
			if (RainFindRobber.Find(character) == RAIN.Action.RAINAction.ActionResult.SUCCESS)
			{
				done = true;
			}
		}
		done = (target - character.transform.position).sqrMagnitude < 0.001;
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
