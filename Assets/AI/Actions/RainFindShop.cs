using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Basic action to locate a shop to go to
/// </summary>
[RAINAction]
public class RainFindShop : ActionBase
{
	// How far away the shop can be (squared)
	// Note: a single block is about 75 units
	public float shopRange = 200 * 200;
	
	/// <summary>
	/// Grabs a target shop if the character is within an acceptable range
	/// </summary>
	public override ActionResult Execute()
	{
		// Get a random shop to go to
		GameObject targetShop = City.GetRandom(City.shops);
		if ((targetShop.transform.position - character.transform.position).sqrMagnitude < shopRange)
		{
			character.target = targetShop.GetComponent<Building>();
			return ActionResult.SUCCESS;
		}
		
		// Shop is too far away
		else return ActionResult.FAILURE;
	}
}