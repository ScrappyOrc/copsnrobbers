using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainStalkTarget : ActionBase
{
	float followDistance = 10.0f;

	// this action will stalk a citizen if they have a citizen as a target
    public override ActionResult Execute()
    {
		//Check to see if the target is a citizen
		// TODO: make targets be game objects
		// then the robbers can actually stalk the target

		if( character.target.GetType() == typeof(Character) )
		{
			//character.QueueAction( new Follow( character.target, followDistance ) );
			//return ActionResult.SUCCESS;
		}

		// TODO: maybe expand this later to have them just wait around if its too crowded,
		// for now they will just rob right away if they have a building targeted
		return ActionResult.FAILURE;
    }
}