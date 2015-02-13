using UnityEngine;
using System.Collections;

public class Flee : Action
{
	GameObject target;
	float distance;
	NavMeshHit hit;

	/// <summary>
	/// Creates a Flee action that causes the target
	/// to flee the given GameObject
	/// </summary>
	/// <param name="target">GameObject to flee</param>
	/// <param name="distance">Distance to keep away from "target" </param>
	public Flee(GameObject target, float distance)
	{
		this.target = target;
		this.distance = distance;

		hit = new NavMeshHit();
	}
	
	/// <summary>
	/// Updates the target position of the character
	/// to follow the target GameObject.
	/// </summary>
	/// <param name="character">The character controlled by the action</param>
	public void Apply(Character character)
	{
		if ((character.transform.position - target.transform.position).sqrMagnitude < distance * distance)
		{
			int rand = Random.Range(0, 4);
			Vector3 newPosition = character.transform.position;

			if(rand == 0)
			{
				newPosition.z += distance;
			}
			else if(rand == 1)
			{
				newPosition.z -= distance;
			}
			else if(rand == 2)
			{
				newPosition.x += distance;
			}
			else
			{
				newPosition.x -= distance;
			}

			NavMesh.SamplePosition(newPosition, out hit, 0.5f, 0);
			character.Agent.SetDestination(hit.position); 
			character.Agent.Resume();
		}
		else
		{
			character.Agent.Stop();
		}
	}
}
