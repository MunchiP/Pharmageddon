using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoAudio : MonoBehaviour
{
  public string sonidoNombre = "Enemigo";
    private AudioSource audioSource;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Crear AudioSource 3D con el AudioManager
        audioSource = AudioManager.Instance.Create3DAudioSource(sonidoNombre, transform);
        audioSource.loop = true;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 10f;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.2f;
    }

    void Update()
    {
        if (agent == null || audioSource == null) return;

        bool isMoving = agent.velocity.magnitude > 0.1f;

        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
