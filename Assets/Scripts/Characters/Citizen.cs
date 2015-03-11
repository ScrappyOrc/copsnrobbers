using UnityEngine;
using System.Collections;
//using System.Reflection;
//using System;


/// <summary>
/// Represents a general NPC that wanders around the city or gets in 
/// line at stores, reacting to events happening around them
/// </summary>
public class Citizen : Character 
{
	// Max money a citizen can start with
	public float MAX_MONEY = 1000.0f;
	public float shoppingRange = 100.0f;
	GameObject targetShop;
	Building targetBuilding;
	GameObject targetBank;

	/// <summary>
	/// Characters start of wandering about the city
	/// </summary>
	override protected void Start () 
	{
		type = STEERING_TYPE.NONE;
		base.Start ();
		money = MAX_MONEY * UnityEngine.Random.value;
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
	private void Old_Decide() 
	{
		// Wander around the city looking for something to do
		if (UnityEngine.Random.value < 0.7)
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
				QueueAction(new Seek(bank, GameManager.HALT_DISTANCE));
				QueueAction(new Shop(bank.GetComponent<Building>()));
			}

			// Move to and then get in line at the shop
			QueueAction(new Seek(shop, GameManager.HALT_DISTANCE));
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
	public void MakeDecision() {
        DecisionTree.Node node = dTree.Root;
        bool inTree = true;
        while (inTree)
        {
            bool result = true;
            switch (node.Data)
            {
                case "Chance":
                    result = Chance();
                    break;
                case "StartWander":
                    result = StartWander();
                    break;
                case "HaveMoney":
                    result = HaveMoney();
                    break;
                case "LookForBank":
                    result = LookForBank();
                    break;
                case "CheckInLine":
                    result = CheckInLine();
                    break;
                case "LookForShop":
                    result = LookForShop();
                    break;
                case "GetInLine":
                    result = GetInLine();
                    break;

                default:
                    break;
            }
            if (node.IsLeaf()) inTree = false;
            else
            {
                if (result) node = node.YesPtr;
                else node = node.NoPtr;
            }
        }
	}



	/// <summary>
	/// Am I shopping
	/// </summary>
	private bool AmIShopping() {
		
		return true;
	}

	/// <summary>
	/// Do I have money?
	/// </summary>
    private bool HaveMoney()
    {
        //Debug.Log("Checking to see if I have money");
        targetShop = City.GetRandom(City.shops);
		targetBuilding = targetShop.GetComponent<Building>();
		if (money < targetBuilding.MONEY_AMOUNT) return false;
        else return true; 
	}

	/// <summary>
	/// Am I in line?
	/// </summary>
	private bool CheckInLine()
	{
		return true;
	}

	/// <summary>
	/// Am I looking for a bank?
	/// </summary>
    private bool LookForBank()
    {
        //Debug.Log("I don't have money, I'm going to the bank");
        targetBank = City.GetRandom(City.banks);
		if ((targetBank.transform.position - this.transform.position).sqrMagnitude < shoppingRange)
		{
			targetBuilding = targetBank.GetComponent<Building>();
			QueueAction(new Seek(targetBank, GameManager.HALT_DISTANCE));
			Debug.Log("I need to go to the bank!");
			return true;
		}
		Debug.Log("That Bank is way too far!");
		return false;
	}

	/// <summary>
	/// CHANCE!
	/// </summary>
    private bool Chance()
    {
        //Debug.Log ("Chance time!");
        if (UnityEngine.Random.value < 0.7)
            return true;
        else
            return false;
	}

	/// <summary>
    /// Move to and then get in line at the shop
	/// </summary>
    private bool LookForShop()
    {
        //Debug.Log("I want to go shopping, I'll find a shop");
		Debug.Log((targetShop.transform.position - this.transform.position).sqrMagnitude);
        if ((targetShop.transform.position - this.transform.position).sqrMagnitude < shoppingRange)
		{
			QueueAction(new Seek(targetShop, GameManager.HALT_DISTANCE));
			Debug.Log("Lets go shopping!");
			return true;
		}
		Debug.Log("That store is way too far!");
		return false;
	}

	/// <summary>
	/// Start wandering around
	/// </summary>
    private bool StartWander()
    {
        //Debug.Log("Meh, maybe I'll wander around for a while");
        QueueAction(new Wander(4));
        QueueAction(new Idle(2));
        return true;
	}

	/// <summary>
	/// Get in line at a shop.
	/// </summary>
    private bool GetInLine()
    {
		QueueAction(new Shop(targetBuilding));
        return true;
    }
}
