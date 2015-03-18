using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a pause in action where the character
/// waits for a moment before doing another action
/// </summary>
public class Idle : Action 
{
	private float timeLeft;
	private bool done = false;

	/// <summary>
	/// Sets up an Idle action for the given number of seconds
	/// </summary>
	/// <param name="seconds">Seconds to remain idle for</param>
	public Idle(float seconds) 
	{
		this.timeLeft = seconds;
	}
	
	/// <summary>
	/// Checks the elapsed time for when the action finishes
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character) 
	{
		timeLeft -= Time.deltaTime;
		done = timeLeft <= 0;
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
