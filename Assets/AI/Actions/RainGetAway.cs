using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainGetAway : ActionBase
{
    public override ActionResult Execute()
    {
		character.QueueAction( new Flee( character.target.gameObject, 150.0f ) );
        return ActionResult.RUNNING;
    }
}