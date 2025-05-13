using UnityEngine;

public class Dispara : MonoBehaviour
{

    public GameObject proyectilPrefab;

    // -M- Dispara el proyectíl
    public void DisparaProyectil()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Dispara con el espacio
        {
            Vector3 offset = new Vector3(0, -0.5f , 1.5f);
            Instantiate(proyectilPrefab, transform.position + offset, proyectilPrefab.transform.rotation);
        }
    }
}
