using UnityEngine;
using System.IO;

/// <summary>
/// Main class for setting up the game and accessing various aspects
/// of the game's data.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager singleton;

	public GameObject CITIZEN;

	public int CITIZENS = 10;
	public float SPAWN_RATE = 0.1f;
	public static float HALT_DISTANCE = 10.0f;

	private const string FILE_LOCATION = "Assets/Scripts/DecisionTrees/";
	private string citizenTreePath;

	private DecisionTree[] dTrees;

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
		dTrees = new DecisionTree[3];

		FileInfo file = new FileInfo("Assets/Scripts/DecisionTrees/citizenTree.txt");
		Debug.Log(file.FullName);
		Initialize();

		City.Initialize();
	}

	void Initialize()
	{
		citizenTreePath = FILE_LOCATION + "citizenTree.txt";

		dTrees[0] = new DecisionTree(citizenTreePath);
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
				citizen.GetComponent<Citizen>().dTree = dTrees[0];
				citizenList[citizenList.Length - CITIZENS] = citizen;

				CITIZENS--;
			}
		}
	}
}
