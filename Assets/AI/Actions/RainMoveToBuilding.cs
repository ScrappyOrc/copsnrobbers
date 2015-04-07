using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Action for moving to a targeted building
/// </summary>
[RAINAction]
public class RainMoveToBuilding : ActionBase
{
	/// <summary>
	/// Grabs a target bank if the character is within an acceptable range
	/// </summary>
	public override ActionResult Execute()
	{
		// Move to the building
		if (character.target != null) {
			character.QueueAction(new Seek(character.target.transform, GameManager.HALT_DISTANCE));
			return ActionResult.RUNNING;
		}

		// No building to move to
		else return ActionResult.FAILURE;
	}
}