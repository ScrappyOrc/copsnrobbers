using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Condition to check whether or not a character doesn't have much money left
/// </summary>
[RAINAction]
public class RainIsLowMoney : ActionBase
{
	/// <summary>
	/// Checks whether or not the character is low on money
	/// </summary>
	public override ActionResult Execute()
	{
		Debug.Log("Low? " + character.lowMoney);
		return character.lowMoney ? ActionResult.SUCCESS : ActionResult.FAILURE;
	}
}