using UnityEngine;

/// <summary>
/// Main class for setting up the game and accessing various aspects
/// of the game's data.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager singleton;

	public GameObject CITIZEN;
	public int CITIZENS = 100;
	public float SPAWN_RATE = 0.1f;

	private GameObject[] citizenList;
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
		citizenList = new GameObject[CITIZENS];

		City.Initialize();
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
	}
}
