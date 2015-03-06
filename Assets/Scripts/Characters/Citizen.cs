using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a general NPC that wanders around the city or gets in 
/// line at stores, reacting to events happening around them
/// </summary>
public class Citizen : Character 
{
	// Max money a citizen can start with
	public float MAX_MONEY = 1000.0f;

	/// <summary>
	/// Characters start of wandering about the city
	/// </summary>
	override protected void Start () 
	{
		type = STEERING_TYPE.NONE;
		base.Start ();

		money = MAX_MONEY * Random.value;
		Decide ();
	}

	/// <summary>
	/// When an action finishes, give them something new to do
	/// </summary>
	override protected void Update () 
	{
		base.Update ();

		if (this.actionQueue.Count == 0) 
		{
			Decide ();
		}
	}

	/// <summary>
	/// Tells the citizen to decide what to do next
	/// under normal circumstances. 
	/// </summary>
	private void Decide() 
	{
		// Wander around the city looking for something to do
		if (Random.value < 0.7)
		{
			QueueAction(new Wander(4));
			QueueAction(new Idle(2));
		}

		// Look for a shop to buy from
		else 
		{
			GameObject shop = City.GetRandom(City.shops);
			Building script = shop.GetComponent<Building>();

			// Need to go to a bank if does not have enough money to
			// buy from the desired shop
			if (money < script.MONEY_AMOUNT)
			{
				GameObject bank = City.GetRandom(City.banks);
				QueueAction(new Seek(bank, 10));
				QueueAction(new Shop(bank.GetComponent<Building>()));
			}

			// Move to and then get in line at the shop
			QueueAction(new Seek(shop, 10));
			QueueAction(new Shop(script));
		}
	}
}
