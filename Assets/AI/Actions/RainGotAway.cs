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
        return ActionResult.SUCCESS;
    }
}