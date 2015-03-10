using UnityEngine;
using System.Collections;
using System.Reflection;

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
			Old_Decide ();
		}
	}

	/// <summary>
	/// Tells the citizen to decide what to do next
	/// under normal circumstances. 
	/// </summary>
	private void Old_Decide() 
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

	/// <summary>
	/// Decide what action to take
	/// </summary>
	private void Decide() {
		MakeDecision();
	}

	// this is here because we weren't sure if the parent would be able to access the child's functions through reflection
	// maybe it can? try later
	public override void MakeDecision() {
		DecisionTree.Node node = dTree.Root;
		bool inTree = true;
		while (inTree) {
			MethodInfo method = this.GetType().GetMethod(node.Data);
			if ((bool)method.Invoke(this, null)) {
				if (node.IsLeaf()) inTree = false;
				else {
					node = node.YesPtr;
				}
			} else {
				if (node.IsLeaf()) inTree = false;
				else {
					node = node.NoPtr;
				}
			}
		}
	}

	/// <summary>
	/// Am I shopping
	/// </summary>
	private bool TryShop() {
		
		return true;
	}

	/// <summary>
	/// Do I have money?
	/// </summary>
	private bool HaveMoney() {
		return true;
	}

	/// <summary>
	/// am i in line?
	/// </summary>
	private bool CheckInLine() {
		return true;
	}

	/// <summary>
	/// am I looking for a bank?
	/// </summary>
	private bool LookForBank() {
		return true;
	}

	/// <summary>
	/// CHANCE!
	/// </summary>
	private bool Chance() {
		return true;
	}

	/// <summary>
	/// Am I looking for a shop
	/// </summary>
	private bool LookForShop() {
		return true;
	}

	/// <summary>
	/// Should I start wandering around
	/// </summary>
	private bool StartWander() {
		return true;
	}
}
