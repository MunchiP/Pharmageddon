using System.Collections;
using TMPro;
using UnityEngine;

public class DialogoManager : MonoBehaviour
{

    [SerializeField] private GameObject dialogoMarca; // Elemento de  ... visual cerca del objeto
    [SerializeField] private string[] dialogoLineas; // Guardo en un array las lineas de dialogo que quiero poner
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TMP_Text dialogoTexto;

    private string nombreJugador; // Traigo la variable de nombre del jugador creado en la scena anterior
 
    private bool didDialogoComenzo; // Condicional si el dialogo comenzó
    private int lineIndex; // Mostrar la linea de dialogo que está recorriendo
    private float tiempoLetra = 0.05f;

    private bool isJugadorCerca;    // Verifica si el jugador está cerca para comenzar el dialogo
                                    // Como no hay otro elemento con qué colisionar no verifico el tag del usuario y este scritp lo tienen las pastillas
    void OnTriggerEnter(Collider other)    // Si lo está se activa el dialogo
    {
        isJugadorCerca = true;
        dialogoMarca.SetActive(true);
    }

    void OnTriggerExit(Collider other)      // Se desactiva si ya no está
    {
        isJugadorCerca = false;
        dialogoMarca.SetActive(false);
    }

    void Start()
    {
            nombreJugador = PlayerPrefs.GetString("NombreUsuario");
    }

    void Update()
    {
        if (isJugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (!didDialogoComenzo) // verificar que el dialogo no se ha iniciado
            {
                ComienzaDialogo();                
            } 
            else if (dialogoTexto.text == dialogoLineas[lineIndex])
            {
                SiguienteLineaDialogo();
            }  
        }
    }

    void ComienzaDialogo()
    {
        didDialogoComenzo = true;
        dialogoPanel.SetActive(true); // Activo panel donde va el texto
        dialogoMarca.SetActive(false); // Desactivo la marca de inicio
        lineIndex = 0; // Re inicia el dialogo cada que comienza y empieza en 0
        StartCoroutine(MuestraLinea()); // llamo -desde una corrutina- al método creado
        Time.timeScale =  0f; // Para que el jugador permaneza en el lugar cuando empieza la linea de dialogo - congelado -
    }

    private IEnumerator MuestraLinea() // Como usaré un efecto de tipeo, usaré una corrutina que: me permitirá parusar la ejecución y reanudarle después de cierto tiempo
    {
     
        ///////////////////////////////////////////////////////////////////////////
        /// recibe el nombre del usuario /////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        
        string lineaActual = dialogoLineas[lineIndex];

        if (lineaActual.Contains("{nombre}")) /// {nombre} lo he declarado en el inspector
        {
            lineaActual = lineaActual.Replace("{nombre}", nombreJugador); //nombreJugador es la variable con la que lo he asignado y guardado esde el script anterior
        }

        dialogoTexto.text = string.Empty; // Primero comienza vacío

        foreach (char item in lineaActual) // Concatenar cada uno de los caracteres que se va mostrando  --  dialogoLineas[lineIndex] -- antes de reemplazar por el nombre
        {
            dialogoTexto.text += item;
            yield return new WaitForSecondsRealtime(tiempoLetra); // con la corrutina espero 0.05f segundos para dicha concatenación --- debe ser un real time porque, como alteré el tiempo con la posición del jugador, con el real time esto no afecta al tipeo de la información

        }
    }

        ///////////////////////////////////////////////////////////////////////////
        /// /////////////////////////// /////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////

    void SiguienteLineaDialogo() // Me permite avanzar en las lineas de texto ingresadas en el array
    {
        lineIndex++; // Se va aumentando one per one
        if (lineIndex < dialogoLineas.Length) // Si las lienas de dialogo on mnors que las lineas de dialogo Lenght
        {
            StartCoroutine(MuestraLinea()); // comience a ejecturarse las lineas de código
        }
        else
        {
            didDialogoComenzo = false; // de lo contrario se desactiva el dialogo
            dialogoPanel.SetActive(false); // se desactiva el panel
            dialogoMarca.SetActive(true); // se activan los puntitos de inicio
            Time.timeScale = 1f;
        }
    }
}
