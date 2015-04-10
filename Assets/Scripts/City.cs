using UnityEngine;
using System.Collections;

/// <summary>
/// Contains data and methods for getting data about the city
/// such as random locations on the streets
/// </summary>
public class City {

	// This is the distance between each intersection
	// in a single direction. Can be used to move blocks
	// away while keeping a position thats on a street
	public static readonly int BLOCK_OFFSET = 75;

	// Streets are actually 17/18 large, this just is
	// nicely divisible by 2 and leaves some breathing
	// room from the buildings
	public static readonly int STREET_WIDTH = 16;

	// The center of an intersection near the center of
	// the city. This can be used along with the other
	// numbers to get random points throughout the city
	public static readonly Vector3 CENTER = new Vector3(-2.5f, 0.5f, 11.0f);

	// The max number of blocks away from the center you can get
	// before reaching the edge of the city in a single direction
	public static readonly int MAX_OFFSET = 5;

	public static GameObject[] houses;
	public static GameObject[] shops;
	public static GameObject[] banks;

	/// <summary>
	/// Initializes data such as grabbing the locations of shops,
	/// banks and houses for quick access later on
	/// </summary>
	public static void Initialize() 
	{
		houses = GameObject.FindGameObjectsWithTag("House");
		shops = GameObject.FindGameObjectsWithTag("Shop");
		banks = GameObject.FindGameObjectsWithTag("Bank");
	}

	/// <summary>
	/// Gets a random key point from the list
	/// </summary>
	/// <param name="list">The list to use</param>
	/// <returns>The random key point from the list</returns>
	public static GameObject GetRandom(GameObject[] list) 
	{
		//Debug.Log ("R:" + Random.value);
		//Debug.Log (list.Length);
		return list [(int)(UnityEngine.Random.value * list.Length)];
	}

	/// <summary>
	/// Gets the nearest key point to the source character from
	/// the given list of key points
	/// </summary>
	/// <returns>The nearest key point in the list to the source character</returns>
	/// <param name="list">List of key points to use</param>
	/// <param name="source">Source character</param>
	public static GameObject GetNearest(GameObject[] list, GameObject source) 
	{
		float dSq = float.MaxValue;
		GameObject closest = null;
		foreach (GameObject point in list) 
		{
			var d = (point.transform.position - source.transform.position).sqrMagnitude;
			if (d < dSq)
			{
				closest = point;
				dSq = d;
			}
		}
		return closest;
	}

	/// <summary>
	/// Gets a random point in the city that is on a
	/// street within the given number of blocks from
	/// the center
	/// </summary> 
	/// <returns>The random point</returns>
	/// <param name="blocks">Number of blocks to allow from the center of the city (default MAX_OFFSET)</param> 
	/// <param name="extra">Whether or not to offset across the street along with down blocks (default true)</param>
	public static Vector3 GetRandomPoint(int blocks = 999, bool extra = true) 
	{
		blocks = Mathf.Min (MAX_OFFSET, Mathf.Max (0, blocks));
		Vector3 pos = new Vector3 (CENTER.x, CENTER.y, CENTER.z);

		// Get random number of blocks and extra offset
		float xBlocks;
		float zBlocks;
		float xOffset = 0;
		float zOffset = 0;

		// Must choose X or Z to remain an integer amount to avoid
		// landing in the middle of a building
		if (Random.value > 0.5) 
		{
			xBlocks = Random.value * (blocks * 2) - blocks;
			zBlocks = Mathf.Floor (Random.value * (blocks * 2 + 1)) - blocks;
			if (extra) 
			{
				zOffset = Random.value * STREET_WIDTH - STREET_WIDTH / 2;
			}
		}
		else 
		{
			xBlocks = Mathf.Floor (Random.value * (blocks * 2 + 1)) - blocks;
			zBlocks = Random.value * (blocks * 2) - blocks;
			if (extra)
			{
				xOffset = Random.value * STREET_WIDTH - STREET_WIDTH / 2;
			}
		}

		// Apply the offsets
		pos.x += xBlocks * BLOCK_OFFSET + xOffset;
		pos.z += zBlocks * BLOCK_OFFSET + zOffset;

		// Return the position
		return pos;
	}

	/// <summary>
	/// Gets the center of a random intersection
	/// </summary>
	/// <returns>The random intersection's center</returns>
	/// <param name="blocks">Number of blocks to allow from the center of the city (default MAX_OFFSET)</param>
	public static Vector3 GetRandomIntersection(int blocks = 999)
	{
		blocks = Mathf.Min (MAX_OFFSET, Mathf.Max (0, blocks));

		// Get the random block
		float xBlocks = Mathf.Floor (Random.value * (blocks * 2 + 1)) - blocks;
		float zBlocks = Mathf.Floor (Random.value * (blocks * 2 + 1)) - blocks;

		// Make the vector
		Vector3 center = new Vector3 (CENTER.x, CENTER.y, CENTER.z);
		center.x += xBlocks * BLOCK_OFFSET;
		center.z += zBlocks * BLOCK_OFFSET;
		return center;
	}
}
