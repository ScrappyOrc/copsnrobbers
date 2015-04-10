using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainHasCall : ActionBase
{
    public override ActionResult Execute()
    {
		// If this cop has a robber target, he DOES HAVE A CALL
		if(character != null && ((Cop)character).Robber != null)
			return ActionResult.SUCCESS;

		// This cop does not have a call
		return ActionResult.FAILURE;
    }
}