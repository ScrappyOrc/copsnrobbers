using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Condition to check if a character has money before stopping at
/// a shop to buy something
/// </summary>
[RAINAction]
public class RainHasMoney : ActionBase
{
	/// <summary>
	/// Checks whether or not the character has enough money for their targeted shop
	/// </summary>
	public override ActionResult Execute()
	{
		if (character.target != null && character.money >= character.target.MONEY_AMOUNT) {
			return ActionResult.SUCCESS;
		}
		else {
			character.lowMoney = character.target != null;
			return ActionResult.FAILURE; 
		}
	}
}