using UnityEngine;
using System.Collections.Generic;

public enum STEERING_TYPE
{
	FLEE,
	SEEK,
	FOLLOW
};

/// <summary>
/// The base class for AI characters that are controlled by
/// a NavMeshAgent component along with queued actions.
/// </summary>
public class Character : MonoBehaviour {

	private readonly Queue<Action> actionQueue = new Queue<Action>();
	private NavMeshAgent agent;

	public STEERING_TYPE type;
	public GameObject fleeTarget;

	/// <summary>
	/// Retrieves the NavMeshAgent component of the character
	/// to handle controlling movement patterns
	/// </summary>
	/// <value>The agent of the character</value>
	public NavMeshAgent Agent {
		get { return agent; }
	}

	/// <summary>
	/// Grabs the NavMeshAgent component on startup
	/// </summary>
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();

		if(type == STEERING_TYPE.SEEK)
			QueueAction(new Seek(new Vector3(4.38f, 0.61f, -5.72f)));
		else if(type == STEERING_TYPE.FLEE)
			QueueAction(new Flee(fleeTarget, 5.0f));
	}

	/// <summary>
	/// Updates the character by applying queued actions
	/// </summary>
	void Update () {
		actionQueue.Peek().Apply(this);
	}

	/// <summary>
	/// Queues a new action for the character to follow.
	/// </summary>
	/// <param name="action">Action to queue</param>
	public void QueueAction(Action action) {
		actionQueue.Enqueue(action);
	}

	/// <summary>
	/// Forces the character to follow the given action, cancelling
	/// all other actions.
	/// </summary>
	/// <param name="action">Action to force the character to follow</param>
	public void ForceAction(Action action) {
		actionQueue.Clear();
		actionQueue.Enqueue(action);
	}
}
