using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainMoveToCall : ActionBase
{
    public override ActionResult Execute()
    {
		// Start with default of failure for this action
		ActionResult result = ActionResult.FAILURE;

		// Attempt to grab the robber
		Robber robby = ((Cop)character).Robber;

		// If we still have a robber (read: Call) then head towards its location
        if(robby != null)
		{
			// We are now 'running' this action
			character.QueueAction(new Seek(robby.transform.position));
			result = ActionResult.RUNNING;
		}

		// Return status/result
		return result;
    }
}