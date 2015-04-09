using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Condition for checking whether or not the area around the character is crowded.
/// </summary>
[RAINAction]
public class RainIsCrowded : ActionBase
{
	/// <summary>
	/// Checks whether or not the area around the character is crowded
	/// </summary>
    public override ActionResult Execute()
    {
		// TODO - give the number of cops/citizens a "weight" and measure that
		// against the character's aggressiveness/craziness/whathaveyou instead
		// of having flat amounts to make them unique individuals

		bool crowded = GameManager.singleton.CountNearby(CharacterType.COP, character.transform.position, 50) > 0
			|| GameManager.singleton.CountNearby(CharacterType.CITIZEN, character.transform.position, 50) > 10;

		return crowded ? ActionResult.SUCCESS : ActionResult.FAILURE;
    }
}