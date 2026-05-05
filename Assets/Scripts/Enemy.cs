using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject modelHolder;
    private GameObject armatureHead;
    private PlayerController PlayerAtRuntime;
    [SerializeField] public GameObject DefenseBarrier = null;
    private Vector3 CoverPositionCalculated;

    [SerializeField] public EnemyState enemyState = EnemyState.Defense;
    [SerializeField] public EnemyType enemyType = EnemyType.Grunt;
    private LayerMask visionMask;

    private float minStandTime = 3f;
    private float maxStandTime = 6f;
    private float minCoverTime = 2f;
    private float maxCoverTime = 3f;

    private float _covertimer;
    private float _currentCoverWaitTime;
    private bool _isTakingCover;

    public enum EnemyState
    {
        MoveToPlayer,
        Defense
    }

    public enum EnemyType
    {
        Grunt,
        Grunt_Infected,
        Grunt_Heavy
    }

    void Awake()
    {
        modelHolder = this.transform.GetChild(0).gameObject;

        visionMask = LayerMask.GetMask(new string[] {"Default", "Viewmodel", "Player", "Objects"});

        //set all model types to be disabled, later enabled using EnemyType
        foreach (Transform child in modelHolder.transform)
        {
            child.gameObject.SetActive(false);
        }

        switch (enemyType)
        {
            case EnemyType.Grunt:
                modelHolder.transform.Find("grunt_idle").gameObject.SetActive(true);
                break;
            case EnemyType.Grunt_Infected:
                modelHolder.transform.Find("grunt_infected_idle").gameObject.SetActive(true);
                break;
            default:
                modelHolder.transform.Find("grunt_idle").gameObject.SetActive(true);
                break;
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        armatureHead = modelHolder.transform.GetChild(0).GetComponentsInChildren<Transform>(true)
        .Where(t => t.name.ToLower().Contains("head"))
        .Select(t => t.gameObject)
        .FirstOrDefault();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAtRuntime = GameObject.FindFirstObjectByType<PlayerController>();

        if (DefenseBarrier == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Objects"));

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Barrier"))
                {
                    DefenseBarrier = hitCollider.gameObject;
                    break;
                }
            }
        }

        if (DefenseBarrier != null)
        {
            CoverPositionCalculated = DefenseBarrier.transform.position;
        }

        SetCoverState(false);
    }

    void FixedUpdate()
    {
        CanSeePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsMoving", navMeshAgent.velocity.magnitude != 0f ? true : false);

        if (PlayerAtRuntime != null)
        {
            this.transform.LookAt(PlayerAtRuntime.transform.position);
        }

        if (enemyState == EnemyState.Defense)
        {
            DefenseMode();
        }
        else
        {
            navMeshAgent.destination = PlayerAtRuntime.transform.position;
        }
    }

    void OnAnimatorMove()
    {
        if (animator.GetBool("IsMoving"))
        {
            navMeshAgent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }

    void DefenseMode()
    {
        if (CoverPositionCalculated != Vector3.zero)
        {
            navMeshAgent.destination = CoverPositionCalculated;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("running"))
            {
                _covertimer += Time.deltaTime;

                if (_covertimer >= _currentCoverWaitTime)
                {
                    SetCoverState(!_isTakingCover);
                }
            }
        }
    }

    void SetCoverState(bool crouch)
    {
        _isTakingCover = crouch;
        _covertimer = 0f;

        animator.SetBool("TakeCover", _isTakingCover);

        if (_isTakingCover)
        {
            _currentCoverWaitTime = UnityEngine.Random.Range(minCoverTime, maxCoverTime);
        }
        else
        {
            _currentCoverWaitTime = UnityEngine.Random.Range(minStandTime, maxStandTime);
        }
    }

    bool CanSeePlayer()
    {
        if (Physics.Linecast(armatureHead.transform.position,
        PlayerAtRuntime.armatureHead.transform.position, out RaycastHit hit, visionMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Color lineColor = Color.green;
                Debug.DrawLine(armatureHead.transform.position, PlayerAtRuntime.armatureHead.transform.position, lineColor);
                return true;
            }
        }

        return false;
    }
}
