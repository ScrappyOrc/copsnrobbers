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

	private GameObject[] citizenList;

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

		// Populate the city with citizens
		citizenList = new GameObject[CITIZENS];
		for (int i = 0; i < CITIZENS; i++) 
		{
			GameObject citizen = (GameObject)GameObject.Instantiate(CITIZEN);
			citizen.transform.position = City.getRandomPoint(4, true);
			citizenList[i] = citizen;
		}
	}
}
