using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Simple action for locating a bank to go to
/// </summary>
[RAINAction]
public class RainFindBank : ActionBase
{
	// How far away the bank can be (squared)
	// Note: a single block is about 75 units
	public float bankRange = 200 * 200;

	/// <summary>
	/// Grabs a target bank if the character is within an acceptable range
	/// </summary>
    public override ActionResult Execute()
    {
		// Get a random bank to go to
		GameObject targetBank = City.GetRandom(City.banks);
		if ((targetBank.transform.position - character.transform.position).sqrMagnitude < bankRange)
		{
			character.target = targetBank.GetComponent<Building>();
			return ActionResult.SUCCESS;
		}

		// Bank is too far away
		else return ActionResult.FAILURE;
    }
}