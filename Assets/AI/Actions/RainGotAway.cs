using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainGotAway : ActionBase
{
    public override ActionResult Execute()
    {
		character.target = null;
		(character as Robber).trigger.collider.enabled = false;
        return ActionResult.SUCCESS;
    }
}