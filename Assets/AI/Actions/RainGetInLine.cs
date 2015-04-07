using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Action for shopping at a targeted building
/// </summary>
[RAINAction]
public class RainGetInLine : ActionBase
{
	/// <summary>
	/// Gets in line at the target building
	/// </summary>
	public override ActionResult Execute()
	{
		// Get in line at the target building
		if (character.target != null) {
			character.QueueAction(new Shop(character.target));
			character.lowMoney = false;
			return ActionResult.RUNNING;
		}

		// Didn't have a target
		else return ActionResult.FAILURE;
	}
}