using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainStalkTarget : ActionBase
{
	public int STALK_LIMIT = 10;

	float followDistance = 10.0f;
	int stalkCount;

	// this action will stalk a citizen if they have a citizen as a target
    public override ActionResult Execute()
    {
		if (stalkCount == 0) 
		{
			stalkCount = STALK_LIMIT;
			return ActionResult.FAILURE;
		}
		stalkCount--;

		character.QueueAction(new Idle(3));
		return ActionResult.RUNNING;
    }
}