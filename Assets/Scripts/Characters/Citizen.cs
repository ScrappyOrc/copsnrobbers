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

	// Least amount of money a citizen can have before 
	// they need to visit a bank
	public float LOW_MONEY = 50.0f;

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
		// Out of money causes them to go to a bank
		if (money < LOW_MONEY) 
		{
			GameObject bank = City.GetNearest(City.banks, gameObject);
			QueueAction(new Seek(bank, 10));
			QueueAction(new Shop(bank.GetComponent<Building>()));
		} 
		else 
		{
			// Randomly goes to a shop or wanders around looking
			// for something to do
			if (Random.value < 0.7)
			{
				QueueAction(new Wander(4));
				QueueAction(new Idle(2));
			}
			else 
			{
				GameObject shop = City.GetRandom(City.shops);
				QueueAction(new Seek(shop, 10));
				QueueAction(new Shop(shop.GetComponent<Building>()));
			}
		}
	}
}
