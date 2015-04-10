using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainFindRobber : ActionBase
{
    public override ActionResult Execute()
    {
		// Get all nearby robbers in a certain radius
		List<GameObject> nearby = GameManager.singleton.GetNearby(CharacterType.ROBBER, character.transform.position, 50);

		// Temp var
		Robber robby;

		// Check each robber
		foreach (GameObject go in nearby) 
		{
			robby = go.GetComponent<Robber>();
			// If we have a robber that is a degree of suspicious
			if (robby != null && robby.CrimeLevel > 0) 
			{
				// Start following that robber
				robby = ((Cop)character).Robber;
				character.QueueAction(new Follow(robby.gameObject, 5.0f));

				// Stop the robber when we get to him
				character.QueueAction(new StopCharacter(robby.gameObject));

				return ActionResult.RUNNING;
			}
		}
        return ActionResult.FAILURE;
    }
}