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

	/// <summary>
	/// Characters start of wandering about the city
	/// </summary>
	override protected void Start () 
	{
		base.Start ();
		money = MAX_MONEY * UnityEngine.Random.value;
		type = CharacterType.CITIZEN;
	}
}
