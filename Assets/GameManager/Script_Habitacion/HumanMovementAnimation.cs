using UnityEngine;
using UnityEngine.AI;

public class HumanMovementAnimation : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public GameObject glassDesktop;
    public GameObject glass;

    private NavMeshAgent agent;
    private Animator animator;

    private Transform[] pathPoints;
    private int currentTargetIndex = 0;
    private bool isDrinking = false;

    void Start()
    {
        glass.SetActive(false);//Desactivar el vaso
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        pathPoints = new Transform[] { pointB, pointC };

        // Iniciar en punto A
        transform.position = pointA.position;

        // Ir al primer destino (B)
        MoveToNextPoint();
    }

    void Update()
    {
        if (isDrinking) return;

        // Revisar si llegó al destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentTargetIndex < pathPoints.Length)
            {
                MoveToNextPoint();
            }
            else
            {
                // Llegó a C
                SetWalking(false);
                SetDrinking(true);
                isDrinking = true;
            }
        }

        // Actualizar animación de caminar si se mueve
        SetWalking(agent.velocity.magnitude > 0.1f);
    }

    void MoveToNextPoint()
    {
        if (currentTargetIndex < pathPoints.Length)
        {
            agent.SetDestination(pathPoints[currentTargetIndex].position);
            currentTargetIndex++;
        }
    }

    void SetWalking(bool walking)
    {
        animator.SetBool("isWalking", walking);
    }

    void SetDrinking(bool drinking)
    {
        glassDesktop.SetActive(false);
        glass.SetActive(true);
        animator.SetBool("isDrinking", drinking);
    }

    public void OnFootstep ()
    {

    }
}