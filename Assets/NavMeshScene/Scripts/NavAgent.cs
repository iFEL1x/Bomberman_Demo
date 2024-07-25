using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    private NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_agent.updateRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.updateRotation = true;
    }
}
