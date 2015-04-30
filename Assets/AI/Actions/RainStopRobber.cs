using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainStopRobber : ActionBase
{
    public override ActionResult Execute()
    {
		// Start with default of failure for this action
		ActionResult result = ActionResult.FAILURE;

		// Try to grab the cop's target
		Robber robby = ((Cop)character).Robber;

		if ( robby.CrimeLevel == 0)
			return ActionResult.FAILURE;

		Debug.Log ("Got 'em");

		// Stop the robber
		character.QueueAction(new StopCharacter(robby.gameObject));

		result = ActionResult.SUCCESS;

		// Return status/result
		return result;
    }
}