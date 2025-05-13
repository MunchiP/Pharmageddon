using UnityEngine;
using UnityEngine.UI;

public class UploadName : MonoBehaviour
{
    private GameObject nameUser;

    void Start() 
    {
        nameUser = GameObject.FindWithTag("NameUser");
        nameUser.GetComponent<Text>().text = PlayerPrefs.GetString("NombreUsuario");
    }

}
