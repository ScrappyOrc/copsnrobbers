using UnityEngine;
using System.Collections;

/// <summary>
/// Action where the character "fights" another character
/// </summary>
public class Fight : Action 
{
	private Character target;
	private float time;
	private bool done = false;
	private bool started = false;
	
	/// <summary>
	/// Causes a "fight" between the two characters that just keeps them in place for a duration
	/// </summary>
	/// <param name="target">Character to fight</param>
	/// <param name="time">Distance to keep when following</param> 
	public Fight(Character target, float time) 
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
		if (!started)
		{
			target.InsertAction(new Idle(time));
			character.Agent.Stop();
			started = true;
		}

		time -= Time.deltaTime;
		done = time <= 0;
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
