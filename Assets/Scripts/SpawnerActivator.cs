using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    [SerializeField] private SpawnerVirus spawnerScript;
    [SerializeField] private GameObject Puerta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnerScript.StartSpawning();
            gameObject.SetActive(false);
            Puerta.SetActive(true);
        }
    }
}
