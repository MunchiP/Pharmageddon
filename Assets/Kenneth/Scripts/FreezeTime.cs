using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;
        
    }

public void IniciarEscena()
    {
        Time.timeScale = 1.0f;
    }
}
