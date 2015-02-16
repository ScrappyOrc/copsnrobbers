using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a seek action where the character 
/// moves towards a given location
/// </summary>
public class Seek : Action {

	private Vector3 target;

	/// <summary>
	/// Creates a Seek action from a GameObject containing the target position
	/// </summary>
	/// <param name="target">GameObject containing the target position</param>
	public Seek(GameObject target) {
		this.target = target.transform.position;
	}

	/// <summary>
	/// Creates a Seek action from a transform containing the target position
	/// </summary>
	/// <param name="target">Transform containing the target position</param>
	public Seek(Transform target) {
		this.target = target.position;
	}

	/// <summary>
	/// Creates a Seek action from a given vector coordinate
	/// </summary>
	/// <param name="target">Target position</param>
	public Seek(Vector3 target) {
		this.target = target;
	}

	/// <summary>
	/// Updates the target of the character, moving them
	/// closer to the target position
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character) {
		character.Agent.SetDestination(target);
	}
}
