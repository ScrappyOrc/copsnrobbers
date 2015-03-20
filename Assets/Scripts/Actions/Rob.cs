﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a complex action where the character gets in line at a building
/// and will wait until they can spend/buy money.
/// </summary>
public class Rob : Action {
	
	private Building building;
	private bool done = false;
	private bool robbing = false;
	private float heistTime;
	
	/// <summary>
	/// Creates a Shop action that adds the character to a building's line and
	/// spends or receives money after they wait.
	/// </summary>
	/// <param name="building">building to shop at</param>
	public Rob(Building building) 
	{
		this.building = building;
		this.heistTime = building.ROB_TIME;
	}
	
	/// <summary>
	/// Updates the target of the character, moving them
	/// closer to the target position
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character) 
	{
		GameObject go = character.gameObject;

		if (!robbing) {
			robbing = true;
		}
		Vector3 dest = building.GetRobPosition (go);

		if (dest != character.Agent.destination)
			character.Agent.SetDestination (dest);

		heistTime -= Time.deltaTime;
		if (heistTime <= 0) {
			character.money += building.Rob();
			done = true;
		}
		/* Taken from the shop action
		 * // Need to have the right amount of money
		if (character.money < -building.MONEY_AMOUNT) {
			done = true;
			return;
		}
		
		// Debug - let use see what the character is doing
		GameObject go = character.gameObject;
		
		// Need to get in line
		if (!inLine) 
		{
			building.Queue (go);
			inLine = true;
		}
		
		// Get the waiting position
		Vector3 dest = building.GetDestination (go);
		if (dest != character.Agent.destination) 
		{
			character.Agent.SetDestination (dest);
		}
		
		// Handle transaction when first in line
		if (building.IsFirstInLine (go)) 
		{
			transactionTime -= Time.deltaTime;
			if (transactionTime <= 0)
			{
				building.Dequeue();
				character.money += building.MONEY_AMOUNT;
				done = true;
			}
		}*/
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
