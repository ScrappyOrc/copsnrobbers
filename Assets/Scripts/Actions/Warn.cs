using UnityEngine;
using System.Collections;

/// <summary>
/// Action for wandering around randomly
/// </summary>
public class Warn : Action 
{
	private Cop cop;
	private Robber robber;
	private bool done = false;

	/// <summary>
	/// Action that starts a call for the given cop targeting the robber
	/// </summary>
	/// <param name="cop">Cop to warn</param>
	/// <param name="robber">Robber to track down</param>
	public Warn (Cop cop, Robber robber) 
	{
		this.cop = cop;
		this.robber = robber;
	}

	/// <summary>
	/// Starts a call using the cop to chase the robber
	/// </summary>
	/// <param name="character">Character to apply the action for</param>
	public void Apply (Character character) 
	{
		// TODO - start call for the cop

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
