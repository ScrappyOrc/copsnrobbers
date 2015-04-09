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
		List<GameObject> nearby = GameManager.singleton.GetNearby(CharacterType.ROBBER, character.transform.position, 50);

		foreach (GameObject go in nearby) 
		{
			if (go.GetComponent<Robber>().CrimeLevel > 0) 
			{
				character.follow = go;
				return ActionResult.SUCCESS;
			}
		}
        return ActionResult.FAILURE;
    }
}