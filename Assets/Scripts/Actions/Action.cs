﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Base template for actions to be used within 
/// the AI system.
/// </summary>
public interface Action {

	/// <summary>
	/// Applies the action each update, updating
	/// the target if necessary, keeping track
	/// of data, or other needed tasks.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	void Apply(Character character);

	/// <summary>
	/// Checks whether or not the Action has been completed
	/// </summary>
	/// <returns>true if complete, false if still going</returns>
	bool IsDone();
}
