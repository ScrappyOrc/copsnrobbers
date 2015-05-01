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
        if (rnd < 0.4) {
            (character as Robber).targetBank = City.GetRandom (City.banks);
        } else {
            (character as Robber).targetBank = City.GetNearest (City.banks, character.gameObject);
        }

        character.target = (character as Robber).targetBank.GetComponent<Building> ();

        //Debug.Log("Added target bank: " + (character as Robber).targetBank);

        /* TEMPORARILY CHANGED TO ONLY PICK BANKS FOR BAYES IMPLEMENTATION

		// The robber will sometimes pick a random shop/bank, but will usually pick the nearest shop/bank
		if (rnd < 0.2)
		{
			character.target = City.GetRandom (City.shops).GetComponent<Building>();
		}
		else if( rnd < 0.4 )
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
        */
        return ActionResult.SUCCESS;
    }
}