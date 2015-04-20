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
		// Start with default of failure for this action
		ActionResult result = ActionResult.FAILURE;

		// Get all nearby robbers in a certain radius
		List<GameObject> nearby = GameManager.singleton.GetNearby(CharacterType.ROBBER, character.transform.position, 50);

		// Temp var
		Robber robby;

		// Check each robber
		foreach (GameObject go in nearby) 
		{
			robby = go.GetComponent<Robber>();
			Debug.Log("Crime Level = " + robby.CrimeLevel);
			// If we have a robber that is a degree of suspicious
			if (robby.CrimeLevel > 0) 
			{
				// Start following that robber
				character.QueueAction(new Follow(robby.gameObject, 5.0f));

				result = ActionResult.RUNNING;
			}
		}

		// Return status/result
		return result;
    }
}