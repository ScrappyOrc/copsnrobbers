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
        return ActionResult.SUCCESS;
    }
}