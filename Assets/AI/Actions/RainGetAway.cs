using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainGetAway : ActionBase
{
    public override ActionResult Execute()
    {
		//character.QueueAction( new Flee( character.target.gameObject, 150.0f ) );
		character.fitness += 10;
		Debug.Log ("Robber is trying to get away, he is slightly more fit (+10)");

        
        character.QueueAction (new Seek ((character as Robber).targetEscape, 10.0f));
		
        return ActionResult.RUNNING;
    }
}