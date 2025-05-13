using UnityEngine;

public class HallAudio : MonoBehaviour
{
    public Transform player; // Arrastrar el Player desde el inspector
    private AudioSource currentSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentSource == null)
        {
            // Crear un AudioSource 3D que se repite
            currentSource = AudioManager.Instance.Create3DAudioSource("Pasillos", transform);
            if (currentSource != null)
            {
                currentSource.loop = true;
                currentSource.spatialBlend = 1f;
                currentSource.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentSource != null)
        {
            currentSource.Stop();
            Destroy(currentSource.gameObject);
            currentSource = null;
        }
    }
}

