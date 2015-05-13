using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RainPickTarget : ActionBase
{
    public override ActionResult Execute()
    {
		float rnd = Random.value;

		// Chromosome X decides if I choose bank or shop
		// Chromosome Z decides if I am scared and don't want to rob

		// If I am frightened (chromosome z == 0) then there is a better chance of wandering instead
		/*if (character.chromosome == 0 || character.chromosome == 2 || character.chromosome == 4 || character.chromosome == 6)
		{
			if(rnd < 0.2)
			{
				character.QueueAction(new Wander());
				return ActionResult.RUNNING;
			}
		}*/

		// The robber will sometimes pick a random shop/bank, but will usually pick the nearest shop/bank
		if (rnd < 0.2)
		//if(character.chromosome == 0 || character.chromosome == 1 || character.chromosome == 2 || character.chromosome == 3)	// If X bit is 0, rob shop 
		{
			character.target = City.GetRandom (City.shops).GetComponent<Building>();
		}
		else if( rnd < 0.4 )
		//else 																													// If X bit is 1, rob bank
		{
			character.target = City.GetRandom (City.banks).GetComponent<Building>();
		}
		else
		{

			GameObject closestStore = City.GetNearest (City.shops, character.gameObject);
			GameObject closestBank = City.GetNearest (City.banks, character.gameObject);
			
			// Get the closer of the two for the target
			if ((closestBank.transform.position - character.transform.position).sqrMagnitude 
			    < (closestStore.transform.position - character.transform.position).sqrMagnitude) 
			{
				character.target = closestBank.GetComponent<Building>();
			}
			else 
			{
				character.target = closestStore.GetComponent<Building>();
			}
		}

        return ActionResult.SUCCESS;
    }
}