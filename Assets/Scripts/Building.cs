using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a building that citizens can visit and line up at
/// in order to spend or receive money.
/// </summary>
public class Building : MonoBehaviour {

	// Amount of money it gives/takes from the citizens each visit
	// Positive gives money (banks)
	// Negative takes money (shops)
	public float MONEY_AMOUNT = 200.0f;
	
	// The time it takes to process a character's transaction
	// They will wait this amount of time while first in line before
	// spending/getting money and leaving the line
	public float PROCESS_TIME = 5.0f;

	// The time it takes for a robber to rob from this store
	public float ROB_TIME = 10.0f;

	// At higher security levels, the stores will be more likely to respond to the robbers
	// this could mean having a security guard on duty
	// or having a silent alarm, or maybe a gun behind the desk?
	public int SECURITY_LEVEL = 1;

	// Space between two waiting characters in line
	public float LINE_SPACE = 0.5f;

	// money in the cash register
	public float cashRegister = 0;

	// Characters currently in line
	private readonly List<GameObject> line = new List<GameObject>();

	void Start()
	{
		cashRegister = Random.value * 5000;
	}

	/// <summary>
	/// Checks whether or not the line for the building is empty
	/// </summary>
	/// <returns>true if the line is empty, false otherwise</returns>
	public bool IsLineEmpty()
	{
		return line.Count == 0;
	}

	/// <summary>
	/// Retrieves the last person in line for the building
	/// </summary>
	/// <returns>The last in line or null if none are in line</returns>
	public GameObject GetLastInLine() 
	{
		if (IsLineEmpty ())
			return null;
		return line[line.Count - 1];
	}

	/// <summary>
	/// Queues a character into the line for the building
	/// </summary>
	/// <param name="character">character to queue</param>
	public void Queue(GameObject character) 
	{
		line.Add (character);
	}

	/// <summary>
	/// Removes a character from the line
	/// </summary>
	public void Dequeue() 
	{
		line.RemoveAt (0);
	}

	public void Buy()
	{
		cashRegister += Mathf.Abs(MONEY_AMOUNT);
	}

	public float Rob()
	{
		float temp = cashRegister;
		cashRegister = 0;
		return temp;
	}

	/// <summary>
	/// Gets the position the character should be waiting at while in line
	/// </summary>
	/// <returns>The waiting position for the character</returns>
	/// <param name="character">character that is in line</param>
	public Vector3 GetDestination(GameObject character) 
	{
		int index = line.IndexOf (character);

		switch (index) 
		{

		// When not in line (which shouldn't happen), return
		// their position because they shouldn't be waiting
		case -1:
			return character.transform.position;

		// When first in line, wait at the building's key point
		// while the transaction time is handled
		case 0:
			return gameObject.transform.position;

		// Otherwise, wait in line according to their position
		default:
			return transform.position + index * LINE_SPACE * transform.forward;
		}
	}

	/// <summary>
	/// Gets the rob position.
	/// </summary>
	/// <returns>The position that the robber will stand to rob the building</returns>
	/// <param name="character">the robber</param>
	public Vector3 GetRobPosition(GameObject character)
	{
		return transform.position - LINE_SPACE * transform.forward;
	}

	/// <summary>
	/// Checks whether or not the character is first in line
	/// </summary>
	/// <returns>true if first in line, false otherwise</returns>
	/// <param name="character">Character to check for</param>
	public bool IsFirstInLine(GameObject character)
	{
		return line [0] == character;
	}
}
