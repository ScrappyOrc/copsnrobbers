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
	const int CIT_COUNT = 10;
	int citCount;
	const int COP_COUNT = 0;

	/// <summary>
	/// Checks whether or not the area around the character is crowded
	/// </summary>
    public override ActionResult Execute()
    {
		// TODO - give the number of cops/citizens a "weight" and measure that
		// against the character's aggressiveness/craziness/whathaveyou instead
		// of having flat amounts to make them unique individuals

		citCount = CIT_COUNT;

		// If the character's Y chromosome == 0, then they are more reckless. They will consider a place crowded
		// if there are more people than a non-reckless robber would find a place crowded. (i.e. 13 vs 10 citizens)
		if (character.chromosome == 0 || character.chromosome == 1 || character.chromosome == 4 || character.chromosome == 5)
			citCount += 3;

		bool crowded = GameManager.singleton.CountNearby(CharacterType.COP, character.transform.position, 50) > COP_COUNT
			|| GameManager.singleton.CountNearby(CharacterType.CITIZEN, character.transform.position, 50) > citCount;

		return crowded ? ActionResult.SUCCESS : ActionResult.FAILURE;
    }
}