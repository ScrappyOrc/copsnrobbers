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
		if( character.target.cashRegister > 250)
			return ActionResult.SUCCESS;

        return ActionResult.FAILURE;
    }
}