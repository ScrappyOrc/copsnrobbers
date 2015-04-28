using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainRobShop : ActionBase
{
    public override ActionResult Execute()
    {
		character.QueueAction( new Rob( character.target ) );
		((Robber)character).CrimeLevel++;
		Debug.Log ("increasing crime level!!!");
        return ActionResult.RUNNING;
    }
}