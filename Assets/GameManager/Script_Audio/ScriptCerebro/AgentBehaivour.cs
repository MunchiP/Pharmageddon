using UnityEngine;
using UnityEngine.AI;

public class AgentBehaivour : MonoBehaviour
{
    
    //Referencias al agente que se moverá
    NavMeshAgent agent;
    public Transform target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
            agent.SetDestination(target.position);
    }
}
