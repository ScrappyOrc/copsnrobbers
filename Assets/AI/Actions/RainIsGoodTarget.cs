using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainIsGoodTarget : ActionBase
{
    public override ActionResult Execute()
    {
		// check to see if the target character/building has a lot of money to rob
		Debug.Log("Target: " + character.target);
		Debug.Log("Money: " + character.target.cashRegister);
		if( character.target.cashRegister > 250)
			return ActionResult.SUCCESS;

		character.target = null;
        return ActionResult.FAILURE;
    }
}