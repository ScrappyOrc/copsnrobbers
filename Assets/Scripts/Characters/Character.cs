using UnityEngine;
using System.Collections.Generic;

public enum STEERING_TYPE
{
	FLEE,
	SEEK,
	FOLLOW,
    WANDER
};

/// <summary>
/// The base class for AI characters that are controlled by
/// a NavMeshAgent component along with queued actions.
/// </summary>
public class Character : MonoBehaviour {

	protected readonly Queue<Action> actionQueue = new Queue<Action>();
	protected NavMeshAgent agent;

	public STEERING_TYPE type;
	public GameObject target;

    public int wanderBlocks = 3;

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
	virtual protected void Start ()
	{
		agent = GetComponent<NavMeshAgent>();

		if(type == STEERING_TYPE.SEEK)
			QueueAction(new Seek(target.transform.position));
		//else if(type == STEERING_TYPE.FLEE)
		//	QueueAction(new Flee(target, 50.0f));
        else if(type == STEERING_TYPE.WANDER)
            QueueAction(new Wander(wanderBlocks));
		else if (type == STEERING_TYPE.FOLLOW)
			QueueAction(new Follow(target, 1.0f));
	}

	/// <summary>
	/// Updates the character by applying queued actions
	/// </summary>
	virtual protected void Update () {
		if (actionQueue.Count == 0) return;
		Action next = actionQueue.Peek();

		if(next != null)
		{
		// When one action finishes, change to the next
		if (next.IsDone()) 
		{
			actionQueue.Dequeue();

			// Ran out of actions
			if (actionQueue.Count == 0) 
			{
				agent.Stop();
				return;
			}

			next = actionQueue.Peek();
		}
		next.Apply(this);
		}
		else
		{
			agent.Stop();
			return;
		}
		/*
        if (type == STEERING_TYPE.WANDER) {
            // currently for some reason the path doesn't get completed, I will have to look into this
            // my current theory is because the Y keeps changing on the wanderer the navigation system isn't recognizing that the path is completed
            // first we check to see if the agent has a pending path
            //if (!agent.pathPending) {
                // then we check if the agent is stopping
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    // then we check to see if the agent has a path, or they have no velocity
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // if it passes all the checks we can dequeue the current action
                        // NOTE: if the first check gets fixed I don't think this is necessary
                        actionQueue.Dequeue ();

                        // queue up the wander
                        QueueAction(new Wander(this, wanderRange));
                    }
                }
            //}
        }
        */
	}

	void OnTriggerEnter(Collider col)
	{
		if(type == STEERING_TYPE.FLEE && col.gameObject.GetComponent<NavMeshAgent>() != null)// && col.gameObject.name == target.gameObject.name)
		{
			QueueAction(new Flee(col.gameObject, 50.0f));
			Debug.Log("Flee collided with " + col.gameObject.name + " and is fleeing from it");

		}
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
