using UnityEngine;
using System.Collections;

public class Robber : Character 
{
	// Target store to rob
	private Building store;

	// Whether or not the robber finished robbing something
	private bool hasRobbed = false;

	// How many cops are chasing the robber
	private int copsOnTail = 0;

	// The severity of the robbery
	private int crimeLevel = 0;

	// The game object child with the trigger collider
	public GameObject trigger;

    public GameObject targetBank;
    public GameObject targetEscape;
	public Observation robObservation;

	// How many cops are chasing the robber
	public int CopsOnTail {
		get { return copsOnTail; }
		set { copsOnTail = value; }
	}

	// The severity of the robbery
	// This is "0" when hasn't robbed anything
	public int CrimeLevel {
		get { return crimeLevel; }
		set { crimeLevel = value; }
	}

	// Use this for initialization
	override protected void Start () 
	{
		base.Start ();

		trigger.collider.enabled = false;

		// Despite being robbers, they appear
		// as citizens to begin with to avoid
		// immediately being arrested for no reason
		type = CharacterType.CITIZEN;
	}

	/// <summary>
	/// Do I have a store in mind to rob?
	/// </summary>
	/// <returns><c>true</c> if this instance has target; otherwise, <c>false</c>.</returns>
	private bool HasTarget() 
	{
		return store != null;
	}

	/// <summary>
	/// Is there too many people around?
	/// </summary>
	/// <returns>True if crowded, false otherwise</returns>
	private bool IsCrowded()
	{
		return GameManager.singleton.CountNearby (CharacterType.COP, transform.position, 50) > 0
			|| GameManager.singleton.CountNearby (CharacterType.CITIZEN, transform.position, 50) > 10;
	}

	/// <summary>
	/// Have I been waiting too long?
	/// </summary>
	/// <returns><c>true</c> if this instance is impatient; otherwise, <c>false</c>.</returns>
	private bool IsImpatient()
	{
		// TODO implement individual characteristics
		return false;
	}

	/// <summary>
	/// Seems like a bad idea, should I do it anyway?
	/// </summary>
	/// <returns><c>true</c> if this instance is reckless; otherwise, <c>false</c>.</returns>
	private bool IsReckless()
	{
		// TODO implement individual characteristics
		return false;
	}

	/// <summary>
	/// Rob the current target store
	/// </summary>
	private bool Rob()
	{
		// TODO rob the store and set crime level

		hasRobbed = true;
		type = CharacterType.ROBBER;
		trigger.collider.enabled = true;
		return true;
	}

	/// <summary>
	/// Forgets the current target due to waiting for too long
	/// </summary>
	/// <returns>True</returns>
	private bool ForgetTarget()
	{
		this.store = null;
		return true;
	}

	/// <summary>
	/// I want to rob here, but I'll wait around for a little.
	/// </summary>
	/// <returns>False</returns>
	private bool StalkTarget()
	{
		// TODO add stalking behavior (either slow movement nearby or just idle)
		return false;
	}

	/// <summary>
	/// Did I rob something already
	/// </summary>
	/// <returns>True if robbed a store, false otherwise</returns>
	private bool HasRobbed()
	{
		return hasRobbed;
	}

	/// <summary>
	/// I got the goods, I should get away!
	/// </summary>
	private bool Run() 
	{
		// TODO make a getaway target for the robber
		return false;
	}

	/// <summary>
	/// Are there other robberies distacting attention away from here?
	/// </summary>
	/// <returns>True if scene is chaotic, false otherwise</returns>
	private bool IsChaotic()
	{
		// TODO check for cops leaving to go to other robberies
		return false;
	}

	/// <summary>
	/// Gets the nearest store and makes it the robber's target
	/// </summary>
	/// <returns>true</returns>
	private bool GetNearest()
	{
		GameObject closestStore = City.GetNearest (City.shops, gameObject);
		GameObject closestBank = City.GetNearest (City.banks, gameObject);

		// Get the closer of the two for the target
		if ((closestBank.transform.position - transform.position).sqrMagnitude 
		    < (closestStore.transform.position - transform.position).sqrMagnitude) 
		{
			store = closestBank.GetComponent<Building>();
		}
		else 
		{
			store = closestStore.GetComponent<Building>();
		}

		return true;
	}

	/// <summary>
	/// Leave something up to chance
	/// </summary>
	private bool Chance()
	{
		return Random.value > 0.2;
	}

	/// <summary>
	/// GGet a random store to rob from
	/// </summary>
	/// <returns><c>true</c>, if target was gotten, <c>false</c> otherwise.</returns>
	private bool GetTarget()
	{
		// Get a random bank or shop
		if (Random.value > 0.2)
		{
			store = City.GetRandom (City.shops).GetComponent<Building>();
		}
		else 
		{
			store = City.GetRandom (City.banks).GetComponent<Building>();
		}

		return true;
	}

	/// <summary>
	/// Wander about the city
	/// </summary>
	private bool StartWander() 
	{
		QueueAction(new Wander(4));
		QueueAction(new Idle(2));
		return true;
	}

	/// <summary>
	/// Marks the robber as having failed its
	/// robbery attempt after getting caught by the cops
	/// </summary>
	public void Arrest()
	{
		if (crimeLevel == 0)
			return;

		robObservation.rob = false;
		GameManager.singleton.bayes.Add(robObservation);
        //Bayes.reportRobbery (targetBank, targetEscape, false);
		Reset ();
	}

	/// <summary>
	/// Marks the robber as having a successful
	/// robbery after getting away
	/// </summary>
	public void Escape()
	{
		if (crimeLevel == 0)
			return;
        //Debug.Log ("Got away safely!");
        //Bayes.reportRobbery (targetBank, targetEscape, true);
		robObservation.rob = true;
		GameManager.singleton.bayes.Add (robObservation);

		Reset ();
	}

	/// <summary>
	/// Resets the robber's values after finishing a rob attempt
	/// </summary>
	private void Reset()
	{
		crimeLevel = 0;
		hasRobbed = false;
		store = null;
		target = null;
		targetEscape = null;
		trigger.collider.enabled = false;
	}
}
