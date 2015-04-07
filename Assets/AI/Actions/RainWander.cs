using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Basic action that causes a character to move around 
/// randomly within the city.
/// </summary>
[RAINAction]
public class RainWander : ActionBase
{
	/// <summary>
	/// Causes the character to start wandering
	/// </summary>
    public override ActionResult Execute()
    {
		character.QueueAction (new Wander (character.wanderBlocks));
        return ActionResult.RUNNING;
    }
}