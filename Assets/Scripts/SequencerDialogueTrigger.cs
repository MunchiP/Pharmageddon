using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class SequencerDialogueTrigger : MonoBehaviour
{
    public TMP_Text textoDialogo;

    public void IniciarDialogo(string nombreJugador)
    {
        StartCoroutine(MostrarDialogos(nombreJugador));
    }

    IEnumerator MostrarDialogos(string nombreJugador)
    {
        yield return new WaitForSeconds(2f);
        textoDialogo.text = $"Respira {nombreJugador}, solo es un malestar.";
        yield return new WaitForSeconds(4f);
        textoDialogo.text = "no hay raz�n para perder la calma.";
        yield return new WaitForSeconds(4f);
        textoDialogo.text = "En la mesa, est� sobre la mesa.";
        yield return new WaitForSeconds(8f);
        textoDialogo.text = "Uf... solo una m�s.";
        yield return new WaitForSeconds(4f);
        textoDialogo.text = "ya no hay vuelta atr�s";
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("Game");
    }
}