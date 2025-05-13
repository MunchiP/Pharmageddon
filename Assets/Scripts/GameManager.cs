using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI de Vida")]
    public Image imgHearts;

    [Header("Mensaje de Estado")]
    public TMP_Text textMessage;  // ← Aquí debe ir tu TMP_Text

    private int activations = 0;  // Contador de activaciones

    void Start()
    {
        activations = 0;
        if (textMessage != null)
            textMessage.text = "";  // Limpiamos el texto al inicio
    }

    // Llamado desde cada ActivationTrigger al presionar E
    public void UpdateState()
    {
        activations++;

        if (activations == 2)
        {
            if (textMessage != null)
                textMessage.text = "¡Ganaste!";
        }
    }

    public void GameOver()
    {
        imgHearts.rectTransform.sizeDelta = new Vector2(
            imgHearts.rectTransform.sizeDelta.x - 32,
            imgHearts.rectTransform.sizeDelta.y);

        if (imgHearts.rectTransform.sizeDelta.x <= 0)
            RestartScene();
    }

    public void RestartScene()
    {
        Destroy(GameObject.Find("AudioManager"));
        Scene curr = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curr.name);
    }
}
