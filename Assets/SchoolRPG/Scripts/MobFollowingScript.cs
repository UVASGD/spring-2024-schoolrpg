using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobFollowingScript : MonoBehaviour
{
    public Transform target; // The GameObject to follow
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on follower GameObject.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
