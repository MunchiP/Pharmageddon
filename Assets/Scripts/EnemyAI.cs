using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public event Action onEnemy;

    private GameObject playerTarget;
    public GameObject explosion;
    NavMeshAgent navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
            navMeshAgent.SetDestination(playerTarget.transform.position);

        onEnemy?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
         {
             other.GetComponent<GameManager>().GameOver();
             Instantiate(explosion, other.transform.position, other.transform.rotation);
             Destroy(gameObject);
         }

        if (other.CompareTag("Bullet"))
        {
            Instantiate(explosion, transform.position, transform.rotation);            
            Destroy(gameObject);
        }        
    }
}
