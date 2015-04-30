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

		// We succeeded so we are considered more fit
		character.fitness += 50;

        return ActionResult.SUCCESS;
    }
}