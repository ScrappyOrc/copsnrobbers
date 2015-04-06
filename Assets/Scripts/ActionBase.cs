using UnityEngine;
using System.Collections;

public abstract class ActionBase : RAINAction 
{
	protected Character getCharacter(RAIN.Core.ai ai) 
	{
		return ai.Body.GetComponent<Character> ();
	}

	protected bool isRunning(Character character) 
	{
		return !character.QueueEmpty && character.IsRunning;
	}
}
