using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.hasPath)
        {
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        
    }
}
