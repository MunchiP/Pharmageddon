using UnityEngine;

public class MovimientoProyectil : MonoBehaviour
{

    public float speed = 30f;
    
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
