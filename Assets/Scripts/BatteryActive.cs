using System;
using System.Collections;
using UnityEngine;

public class BatteryActive : MonoBehaviour
{
    [Header("UI Prompt")]
    [Tooltip("Arrastra aquí el panel o texto que muestre 'Presiona E'")]
    public GameObject promptUI;

    [Header("Objetos a activar")]
    [Tooltip("Arrastra aquí los GameObjects que deben activarse al presionar E")]
    public GameObject[] objectsToActivate;

    [Header("Objetos a desactivar")]
    [Tooltip("Arrastra aquí los GameObjects que deben desactivarse al presionar E")]
    public GameObject[] objectsToDeactivate;

    private bool playerInside = false;
    private bool activated = false;

    void Start()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            playerInside = true;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            playerInside = false;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInside && !activated && Input.GetKeyDown(KeyCode.E))
        {
            activated = true;

            // Ocultamos el prompt
            if (promptUI != null)
                promptUI.SetActive(false);

            // Activamos los que van a on
            foreach (var go in objectsToActivate)
                if (go != null)
                    go.SetActive(true);

            // Desactivamos los que van a off
            foreach (var go in objectsToDeactivate)
                if (go != null)
                    go.SetActive(false);

            // Notificar al GameManager
            var gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
                gm.UpdateState();
        }
    }
}