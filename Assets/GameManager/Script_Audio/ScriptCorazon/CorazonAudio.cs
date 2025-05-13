using UnityEngine;

public class CorazonAudio : MonoBehaviour
{
     // Para las zonas y el efecto de sonido, necesito saber dónde está el personaje
    public string sonidoNombre;

    private void Start()
    {
        AudioManager.Instance.PlayPositional(sonidoNombre, transform.position);
    }
}
