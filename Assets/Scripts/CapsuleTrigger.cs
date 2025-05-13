using System;
using System.Collections;
using UnityEngine;

public class CapsuleTrigger : MonoBehaviour
{
    public event Action onLeavePill;

    MeshRenderer meshRenderer;
    Material[] mats;
    public Material material;
    public GameObject fxExplosion;
    private bool activate = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mats = meshRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activate)
        {   
            mats[0] = material;
            meshRenderer.materials = mats;
            activate = true;
            other.GetComponent<GameManager>().UpdateState();

            onLeavePill?.Invoke();
        }
    }

    public IEnumerator ActivateExplosion()
    {
        Instantiate(fxExplosion, transform.position, transform.rotation);//Mostrar las explosión en la posición de este transform
        Destroy(gameObject);                                                              
        yield break;
    }

}
