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

	/// <summary>
	/// Do I have a call? (true when has a robber target)
	/// </summary>
	// <returns><c>true</c>Currently on call<c>false</c>Not currently on call</returns>
	private bool HasCall()
	{	
		return robber != null;
	}

	/// <summary>
	/// Gets the closest robber.
	/// </summary>
	/// <returns><c>true</c>Found robber and he is suspicious or worse<c>false</c>Could not find robber or found robber was not suspicious</returns>
	private bool RobberNearby()
	{
		GameObject robber = GameManager.singleton.GetClosest (CharacterType.ROBBER, this);
		if (robber != null)
			nearbyRobber = robber.GetComponent<Robber> ();
		return robber != null && nearbyRobber.CrimeLevel > 0;
	}

	/// <summary>
	/// Are there already enough police for a specific event?
	/// </summary>
	/// <returns><c>true</c>If a robber is already tailed by the max # of cops<c>false</c>Robber has room for more cop tails</returns>
	private bool ManyPoliceOnCall()
	{
		return robber.CopsOnTail > 3;
	}

	/// <summary>
	/// Stops the target robber.
	/// </summary>
	/// <returns><c>true</c>If the stop-robber action was queued<c>false</c>Could not queue stop-robber action</returns>
	private bool StopRobber()
	{
		// TODO "Stop" robber when you reach him

		if(robber != null)
		{
			QueueAction(new Seek(robber.transform.position));
			//QueueAction(new StopRobberAction(robber));
			return true;
		}

		return false;
	}

	/// <summary>
	/// Check if there is a bigger crime than the one committed by your target
	/// </summary>
	/// <returns><c>true</c>If the nearby robber's crime is worse than the current target's<c>false</c>The current robber has a worse crime</returns>
	private bool BiggerCrime()
	{
		return nearbyRobber.CrimeLevel > robber.CrimeLevel;
	}

	/// <summary>
	/// Head to the location provided by the call (aka location of robber provided)
	/// </summary>
	/// <returns><c>true</c>We have a robber target<c>false</c>We didn't have a robber target</returns>
	private bool HeadToCall()
	{
		if (robber != null)
		{
			QueueAction (new Seek (robber.transform.position));
			return true;
		}

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
