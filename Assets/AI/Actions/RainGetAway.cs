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
        List<GameObject> escapePoints = new List<GameObject> ();
        // Create a weighted list of all the escape points
        for (int i = 0; i < City.escapes.Length; i++) {
            for (int j = 0; j <= Bayes.getSuccessfulRobberies ((character as Robber).targetBank, City.escapes [i]); j++) {
                escapePoints.Add (City.escapes [i]);
            }
        }
        // choose an escape point.
        (character as Robber).targetEscape = escapePoints [(int)(Random.value * escapePoints.Count)];
        character.QueueAction (new Seek ((character as Robber).targetEscape, 10.0f));

		//character.QueueAction( new Flee( character.target.gameObject, 150.0f ) );
        return ActionResult.RUNNING;
    }
}