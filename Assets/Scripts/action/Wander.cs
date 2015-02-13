using UnityEngine;
using System.Collections;

public class Wander : Action {

	public int wanderRange = 10;
	private Vector3 target;

	/// <summary>
	/// Creates a wander action that causes the target
	/// to pick a random location within range and move there
	/// </summary>
	/// <param name="character">the character that will be wandering</param>
	public Wander (Wanderer character) {
		target = character.transform.position + new Vector3(Random.Range(-wanderRange, 0, wanderRange);
	}

	/// <summary>
	/// Updates the target of the character, moving them
	/// closer to the target position
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply (Wanderer character) {
	
	}
}
