using UnityEngine;
using RAIN.Action;
using RAIN.Core;

/// <summary>
/// Base class for RAIN actions to simplify using them
/// </summary>
public abstract class ActionBase : RAINAction 
{
	protected Character character;

	/// <summary>
	/// Sets up the reference for the character on create
	/// </summary>
	/// <param name="ai">Ai.</param>
	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);
		character = ai.Body.GetComponent<Character>();
	}

	/// <summary>
	/// Handles turning the "AI" body into a character and
	/// checking for already running tasks.
	/// </summary>
	/// <param name="ai">AI object</param>
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		if (IsRunning()) {
			return ActionResult.RUNNING;
		}
		else if (character.IsRunning) {
			character.IsRunning = false;
			return ActionResult.SUCCESS;
		}
		else {
			ActionResult result = Execute();
			if (result == ActionResult.RUNNING) {
				character.IsRunning = true;
			}
			return result;
		}
	}

	/// <summary>
	/// Method to implement in extending classes
	/// </summary>
	public virtual ActionResult Execute()
	{
		return ActionResult.FAILURE;
	}

	/// <summary>
	/// Checks whether or not the action is currently running
	/// </summary>
	/// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
	protected bool IsRunning() 
	{
		return !character.QueueEmpty && character.IsRunning;
	}
}
