using UnityEngine;
using System.Collections;

/// <summary>
/// Action where the character follows another character
/// </summary>
public class Follow : MonoBehaviour, Action {

	private GameObject target;
	private float distance;

	/// <summary>
	/// Creates a follow action that causes the target
	/// to follow the given GameObject
	/// </summary>
	/// <param name="target">GameObject to follow</param>
	/// <param name="distance">Distance to keep when following</param> 
	public Follow(GameObject target, float distance) {
		this.target = target;
		this.distance = distance;
	}

	/// <summary>
	/// Updates the target position of the character
	/// to follow the target GameObject.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character) {
		if ((character.transform.position - target.transform.position).sqrMagnitude > distance * distance) {
			character.Agent.SetDestination(target.transform.position);
		}
		else {
			character.Agent.Stop();
		}
	}
}
