using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimatorScript : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        if (animator.GetBool("IsMoving"))
        {
            navMeshAgent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }
}
