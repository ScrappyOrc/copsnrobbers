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
        character.QueueAction (new Seek ((character as Robber).targetEscape, 10.0f));
		
        return ActionResult.RUNNING;
    }
}