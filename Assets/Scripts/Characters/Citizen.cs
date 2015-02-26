using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a general NPC that wanders around the city or gets in 
/// line at stores, reacting to events happening around them
/// </summary>
public class Citizen : Character 
{
	/// <summary>
	/// Characters start of wandering about the city
	/// </summary>
	override protected void Start () 
	{
		base.Start ();

		QueueAction (new Wander (4));
	}

	/// <summary>
	/// When an action finishes, give them something new to do
	/// </summary>
	override protected void Update () 
	{
		base.Update ();

		if (this.actionQueue.Count == 0) 
		{
			QueueAction(new Idle(2.0f));
			QueueAction(new Wander(4));
		}
	}
}
