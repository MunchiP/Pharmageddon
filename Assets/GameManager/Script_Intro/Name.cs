using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // ¡Importante para usar TMP_InputField y TMP_Text!
using System.Collections;

public class Name : MonoBehaviour
{
    public TMP_InputField inputText;
    public TMP_Text textoNombre;

    public GameObject continuarBtn;
    [SerializeField] private GameObject mensajeError;

    private void Awake()
    {
        mensajeError.SetActive(true); // Visible al inicio si así lo quieres
    }

    private void Start()
    {
        //StartCoroutine(FocusAlInicio());
        continuarBtn.SetActive(false);

        // Asignar el callback si textoNombre es el mismo campo de entrada
        inputText.onValueChanged.AddListener(OnInputChanged);
    }
    /*
    private IEnumerator FocusAlInicio()
    {
        yield return new WaitForEndOfFrame();
        inputText.ActivateInputField();
    }
    */
    private void OnInputChanged(string texto)
    {
        textoNombre.text = texto; // Opcional: mostrar el nombre mientras se escribe
    }

    private void Update()
    {
        if (inputText.text.Length < 4)
        {
            mensajeError.SetActive(true);
            continuarBtn.SetActive(false);

           
        }
        else
        {
            mensajeError.SetActive(false);
            continuarBtn.SetActive(true);
        }
    }
    public void ContinuarFuncion()
    {
        string nombre = inputText.text;

        // Llama al script que muestra los diálogos y le pasa el nombre

        FindFirstObjectByType<SequencerDialogueTrigger>().IniciarDialogo(nombre);
        Debug.Log("ContinuarFuncion llamada");
    }
}