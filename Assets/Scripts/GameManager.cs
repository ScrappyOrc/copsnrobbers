using UnityEngine;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Main class for setting up the game and accessing various aspects
/// of the game's data.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager singleton;

	public GameObject CITIZEN;
	public GameObject ROBBER;

	public int CITIZENS = 100;
	public int ROBBERS = 1;
	public int COPS = 5;
	public float SPAWN_RATE = 0.1f;
	public static float HALT_DISTANCE = 10.0f;

	private string citizenTreePath;

	private GameObject[] citizenList;
	private GameObject[] robberList;
	private GameObject[] copList;
	private float spawnTimer;

	/// <summary>
	/// Retrieves the list of citizens in the game
	/// </summary>
	/// <value>The list of citizens in the game</value>
	public GameObject[] CitizenList {
		get { return singleton.citizenList; }
	}

	/// <summary>
	/// Initializes the game, spawning needed characters
	/// </summary>
	void Start() 
	{
		singleton = this;
		spawnTimer = SPAWN_RATE;

		// Initialize the lists
		citizenList = new GameObject[CITIZENS];
		robberList = new GameObject[ROBBERS];
		copList = new GameObject[COPS];

		City.Initialize();
	}

	/// <summary>
	/// Gets the list of all characters of a given type
	/// </summary>
	/// <returns>The list of characters</returns>
	/// <param name="type">Type of character to get the list of</param>
	public GameObject[] GetList(CharacterType type)
	{
		switch (type) 
		{
		case CharacterType.CITIZEN:
			return citizenList;
		case CharacterType.COP:
			return copList;
		case CharacterType.ROBBER:
			return robberList;
		default:
			return null;
		}
	}

	/// <summary>
	/// Gets the closest character matching the given type
	/// to the original character
	/// </summary>
	/// <returns>The closest character</returns>
	/// <param name="type">Type of character to look for</param>
	/// <param name="source">Character looking for the nearest</param>
	public GameObject GetClosest(CharacterType type, Character source)
	{
		GameObject[] list = GetList (type);
		float dSq = float.MaxValue;

		GameObject sgo = source.gameObject;
		GameObject result = null;
		Vector3 pos = sgo.transform.position;
		foreach (GameObject go in list) 
		{
			if (go == sgo || go.GetComponent<Character>().Type != type) continue;
			float d = (go.transform.position - pos).sqrMagnitude;
			if (d < dSq) 
			{
				dSq = d;
				result = go;
			}
		}
		return result;
	}

	/// <summary>
	/// Counts the nearby characters matching the given type
	/// </summary>
	/// <returns>The nearby characters</returns>
	/// <param name="location">Location to check around</param>
	/// <param name="radius">Radius to consider close</param>
	public int CountNearby(CharacterType type, Vector3 location, float radius)
	{
		GameObject[] list = GetList (type);

		float rSq = radius * radius;
		int count = 0;
		foreach (GameObject go in list)
		{
			if (go != null && (go.transform.position - location).sqrMagnitude < rSq)
			{
				count++;
			}
		}
		return count;
	}

	public List<GameObject> GetNearby(CharacterType type, Vector3 location, float radius)
	{
		GameObject[] list = GetList(type);

		float rSq = radius * radius;
		List<GameObject> result = new List<GameObject>();
		foreach (GameObject go in list) 
		{
			if (go != null && (go.transform.position - location).sqrMagnitude < rSq)
			{
				result.Add(go);
			}
		}
		return result;
	}

	void Update() 
	{
		if (CITIZENS > 0) 
		{
			spawnTimer -= Time.deltaTime;
			while (spawnTimer <= 0) 
			{
				spawnTimer += SPAWN_RATE;

				GameObject citizen = (GameObject)GameObject.Instantiate(CITIZEN);
				GameObject spawn = City.GetRandom(City.houses);
				citizen.transform.position = spawn.transform.position;
				citizenList[citizenList.Length - CITIZENS] = citizen;

				CITIZENS--;
			}
		}
		if (ROBBERS > 0)
		{
			spawnTimer -= Time.deltaTime;
			while (spawnTimer <= 0)
			{
				spawnTimer += SPAWN_RATE;

				GameObject robber = (GameObject)GameObject.Instantiate(ROBBER);
				GameObject robberSpawn = City.GetRandom(City.houses);
				robber.transform.position = robberSpawn.transform.position;
				robberList[robberList.Length - ROBBERS] = robber;

				ROBBERS--;
			}
		}
	}
}
