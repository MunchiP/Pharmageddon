using UnityEngine;



// [RequireComponent(typeof(AudioSource))]
public class AnusAudio : MonoBehaviour
{

     // Para las zonas y el efecto de sonido, necesito saber dónde está el personaje
    public Transform player; // Agregar en el inspector
    private bool hasPlayed = false; // Controla si ya se reprodujo

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro al script de  AnusAudio");
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(AudioManager.Instance.Play("Cuarto_Ano"));
            hasPlayed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Stop();
            hasPlayed = false;
        }
    }

    


    //     void Update()
    //     {

    // Debug.Log("Entro a script de AnusAudio");

    //         Debug.Log("Entro a script de AnusAudio");
    //         if (player == null || AudioManager.Instance == null) return;

    //         float distance = Vector3.Distance(player.position, transform.position);
    //         // float distanceZone = Mathf.Clamp01(1 - (distance/ maxDistance));
    //         // audioSource.volume = distanceZone * maxVolume;

    //         if (distance <= maxDistance && !hasPlayed)
    //         {
    //             Debug.Log("Verificó la distancia con el jugador");
    //             StartCoroutine(AudioManager.Instance.Play("Cuarto_Ano"));
    //             hasPlayed = true;
    //              Debug.Log("imprimo la distancia en la que está: " + distance);
    //         }
    //         else if (distance> maxDistance)
    //         {
    //             hasPlayed = false;
    //         }
    //     }

}
