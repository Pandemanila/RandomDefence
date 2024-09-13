using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private Vector3 target;
    private NavMeshAgent agent;
    private Weapon weapon;
    private bool isWalk = false;
    public bool isField = true;

    public bool IsWalk => isWalk;
    [SerializeField]
    private float stoppingDistance = 0.1f;
    [SerializeField]
    private float moveThreshold = 0.05f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = stoppingDistance;
        weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        bool isMoving = agent.velocity.magnitude > moveThreshold;
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            isMoving = false;
        }
        if (isMoving != isWalk)
        {
            isWalk = isMoving;
            GetComponent<Animator>().SetBool("IsWalk", isWalk);
            
            if (!isWalk)
            {
                weapon.ChangeState(WeaponState.SearchTarget);
            }
        }
        if(isWalk && agent.remainingDistance <= 0.1)
        {
            StopMoving();
        }
        //Debug.Log($"IsWalk: {isWalk}, Velocity: {agent.velocity.magnitude}, RemainingDistance: {agent.remainingDistance}");



    }

    public void SetDestination(Vector3 targetPosition)
    {
        target = targetPosition;
        if (transform.position.x - target.x > 0)
        {
            Vector2 characterScale = transform.localScale;
            characterScale.x = -Mathf.Abs(characterScale.x);
            transform.localScale = characterScale;
        }
        else if (transform.position.x - target.x < 0)
        {
            Vector2 characterScale = transform.localScale;
            characterScale.x = Mathf.Abs(characterScale.x);
            transform.localScale = characterScale;
        }
        agent.SetDestination(new Vector3(target.x, target.y, 0));
    }

    public void StopMoving()
    {
        agent.ResetPath();
        isWalk = false;
        GetComponent<Animator>().SetBool("IsWalk", isWalk);
        weapon.ChangeState(WeaponState.SearchTarget);
    }
}