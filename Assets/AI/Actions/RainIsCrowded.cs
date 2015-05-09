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
		int cops = GameManager.singleton.CountNearby(CharacterType.COP, character.transform.position, 50);
		int citizens = GameManager.singleton.CountNearby (CharacterType.CITIZEN, character.transform.position, 50);
		int escape = (int)(Random.value * City.escapes.Length);
	
		bool shouldRob = GameManager.singleton.bayes.ShouldRob(escape, citizens, cops);

		if (shouldRob) 
		{
			(character as Robber).targetEscape = City.escapes[escape];

			Observation obs = new Observation();
			obs.escapeIndex = escape;
			obs.citizens = citizens;
			obs.cops = cops;
			(character as Robber).robObservation = obs;

			return ActionResult.FAILURE;
		}
		else return ActionResult.SUCCESS;
    }
}