using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public enum STEERING_TYPE
{
	FLEE,
	SEEK,
	FOLLOW,
    WANDER,
	IDLE,
	SHOP,
	NONE
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
	
	public DecisionTree dTree;

    public int wanderBlocks = 3;

	// Amount of money the character has
	public float money = 0;

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


	/*public virtual void MakeDecision() {
		DecisionTree.Node node = dTree.Root;
		bool inTree = true;
		while (inTree) {
			MethodInfo method = this.GetType().GetMethod(node.Data);
			if ((bool)method.Invoke(this, null)) {
				if (node.IsLeaf()) inTree = false;
				else {
					node = node.YesPtr;
				}
			} else {
				if (node.IsLeaf()) inTree = false;
				else {
					node = node.NoPtr;
				}
			}
		}
	}*/


}
