using UnityEngine;

public class CerebroAudio : MonoBehaviour
{
     // Para las zonas y el efecto de sonido, necesito saber dónde está el personaje
    public Transform player; // Agregar en el inspector
    private bool hasPlayed = false; // Controla si ya se reprodujo

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(AudioManager.Instance.Play("Cuarto_Cerebro"));
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

}
