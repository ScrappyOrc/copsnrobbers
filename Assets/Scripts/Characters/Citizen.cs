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

	// Aggressiveness of the citizen to decide how to deal with robbers
	private float aggressiveness = Random.value;

	/// <summary>
	/// Characters start of wandering about the city
	/// </summary>
	override protected void Start () 
	{
		base.Start ();
		money = MAX_MONEY * UnityEngine.Random.value;
		type = CharacterType.CITIZEN;
	}

	/// <summary>
	/// Handles being near a robbery
	/// </summary>
	/// <param name="other">the thing colliding with</param>
	public void Trigger(Collider other) 
	{
		if (other.CompareTag("Robber")) 
		{
			GameObject robber = other.transform.parent.gameObject;

			if (aggressiveness < 0.25) 
			{
				ForceAction(new Flee(robber, 100));
			}
			else if (aggressiveness < 0.5)
			{
				ForceAction(new Idle(30));
			}
			else if (aggressiveness < 0.75)
			{
				var cop = GameManager.singleton.GetClosest(CharacterType.COP, this).GetComponent<Cop>();
				ForceAction(new Seek(cop.gameObject, 1));
				QueueAction(new Warn(cop, other.GetComponent<Robber>()));
			}
			else 
			{
				ForceAction(new Seek(robber, 0.5f));
				QueueAction(new Fight(robber.GetComponent<Robber>(), 5.0f + (aggressiveness - 0.75f) * 60));
			}
		}
	}
}
