using UnityEngine;
using System.Collections;

public class Cop : Character 
{
	private Robber robber;
	private Robber nearbyRobber;

	// Use this for initialization
	override protected void Start () 
	{
		base.Start ();
		type = CharacterType.COP;
	}
	
	// Update is called once per frame
	override protected void Update () 
	{
	
	}
	
	public void MakeDecision()
	{
		DecisionTree.Node node = dTree.Root;
		bool inTree = true;
		while(inTree)
		{
			bool result = true;
			switch (node.Data)
			{
			case "HasCall":
				result = HasCall();
				break;
			case "RobberNearby":
				result = RobberNearby();
				break;
			case "ManyPoliceOnCall":
				result = ManyPoliceOnCall();
				break;
			case "StopRobber":
				result = StopRobber();
				break;
			case "BiggerCrime":
				result = BiggerCrime();
				break;
			case "HeadToCall":
				result = HeadToCall();
				break;
			case "PanicNearby":
				result = PanicNearby();
				break;
			case "LookForCause":
				result = LookForCause();
				break;
			case "Patrol":
				result = Patrol();
				break;
			}
		}
	}

	private bool HasCall()
	{	
		return robber != null;
	}
	
	private bool RobberNearby()
	{
		GameObject robber = GameManager.singleton.GetClosest (CharacterType.ROBBER, this);
		if (robber != null)
			nearbyRobber = robber.GetComponent<Robber> ();
		return robber != null && nearbyRobber.CrimeLevel > 0;
	}
	
	private bool ManyPoliceOnCall()
	{
		return robber.CopsOnTail > 3;
	}
	
	private bool StopRobber()
	{
		// TODO Seek target robber and "stop" him
		return false;
	}
	
	private bool BiggerCrime()
	{
		return nearbyRobber.CrimeLevel > robber.CrimeLevel;
	}
	
	private bool HeadToCall()
	{
		// TODO seek location provided by call
		return false;
	}
	
	private bool PanicNearby()
	{
		// TODO detect if a hubbub has started nearby
		return false;
	}
	
	private bool LookForCause()
	{
		// TODO if hubbub, seek towards it
		return false;
	}
	
	private bool Patrol()
	{
		// TODO wander within beat
		QueueAction(new Wander(4));
		return true;
	}
}
