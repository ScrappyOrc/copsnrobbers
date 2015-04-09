using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Condition to check whether or not the character currently is
/// targeting a building for some reason
/// </summary>
[RAINAction]
public class RainHasTarget : ActionBase
{
	/// <summary>
	/// Checks whether or not the character has a target building
	/// </summary>
    public override ActionResult Execute()
    {
		return character.target != null ? ActionResult.SUCCESS : ActionResult.FAILURE;
    }
}