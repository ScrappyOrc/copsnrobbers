using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainStalkTarget : ActionBase
{
	float followDistance = 10.0f;

	// this action will stalk a citizen if they have a citizen as a target
    public override ActionResult Execute()
    {
		Debug.Log ("Robber is stalking a target, he is slightly more fit (+10)");
		character.fitness += 10;
		
		character.QueueAction(new Idle(3));
		return ActionResult.RUNNING;
    }
}