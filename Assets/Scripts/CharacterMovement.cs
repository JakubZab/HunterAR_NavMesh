using UnityEngine;
using UnityEngine.AI;
public class CharacterMovement : MonoBehaviour
{
    public float speed = 1f;
    private Joystick joystick;
    private Animator animator;
    private NavMeshAgent agent;
    private bool isMoving = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        joystick = FindObjectOfType<Joystick>();

        agent.enabled = false;
        agent.enabled = true;
    }

    void Update()
    {
        if (isMoving)
        {
            float horizontal = joystick.Horizontal();
            float vertical = joystick.Vertical();

            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

                Vector3 move = direction * speed * Time.deltaTime;
                Vector3 destination = transform.position + move;
                agent.SetDestination(destination);

                animator.SetFloat("Speed", direction.magnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0f);
        }
    }

    public void StopMovement()
    {
        isMoving = false;
        agent.isStopped = true; 
    }

    public void StartMovement()
    {
        isMoving = true;
        agent.isStopped = false; 
    }
}
