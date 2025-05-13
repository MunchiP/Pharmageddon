using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // Singleton: accede de manera facil desde otros scripts
    public static AudioManager Instance;
    // La clase interna -sound- es visible en el inspector
    [System.Serializable] public class Sound 
    {
        public string name; // Nombre para identificar el sonido
        public AudioClip clip; //Archivo del audio que se va a reproducir
        [Range(0.5f, 1f)] public float volumen = 1f; // Rango del volumen que se reproduce
    }

    // Lista de los sonidos que se agregan en el inspector
    public List<Sound> sounds = new List<Sound>(); 

    // El audioSourcee que reproducirá los sonidos
    private AudioSource audioSource;

///////////////////////////////////////////////////////////////////////////////////////////////////
/// Relación con los otros script para responder al .Invoque
/// /////////////////////////////////////////////////////////////////////
    public MonoBehaviour playerScript; // Elemento que asigno en el inspecto para relacionar el objeto que tiene el script y uso MonoBehaivour para relacionar es script que tiene ese elemento
    public MonoBehaviour shootScript;
    public MonoBehaviour leavePill;
    public MonoBehaviour enemyMovement;

    private void Awake()
    {
        // Configuración inicial del sistema
        if (Instance == null)
        {
            Instance = this; // Si no hay ninguna otra instancia, esta es la inicial
            // Obtiene el AudioSource adjunto al GameObject
            DontDestroyOnLoad(gameObject); 
            audioSource = GetComponent<AudioSource>();
            Debug.Log("Si se inició AudioManager");
        } 
        else 
        {
            Destroy(gameObject); // en caso de que existra otro, lo destruye
        }
    }


    // -M- Método que reproduce el sonido según el nombre
     public IEnumerator Play(string sonidoNombre)
    {
        Sound sonido = sounds.Find(x => x.name == sonidoNombre); // Lo busca en la lista

        Debug.Log("Reproducción de sonigo: " + sonidoNombre);
        if (sonido != null) // Es difererente de nulo
        {
            audioSource.PlayOneShot(sonido.clip, sonido.volumen); // Lo reproduzco una sola vez "shot"
            yield return new WaitForSeconds(sonido.clip.length);
        } 
        else 
        {
            Debug.LogWarning("Encontró sonido:" + sonidoNombre);
            yield return new WaitForSeconds(1f);
        }
    }

    // Reproducción de cada uno los clipsssssss
    public float GetClipLength(string name)
    {
        AudioClip clip = sounds.Find(s => s.name == name)?.clip;
        return clip != null ? clip.length : 1f;
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

///////////////////////////////////////////////////////////////////////////////////////////////////
/// Llamado de los Invoke para que se reproduzca el sonido
/// /////////////////////////////////////////////////////////////////////

/// Podría crear un método por cada elemento que será llamado pero, usaré un método genérico con la coroutine 
    void PlaySoundInvoke(string name) => StartCoroutine(Play(name));
    
    private void OnEnable() {
        
        //Movimiento del personaje: camina y salta
        ThirdPersonController player = playerScript  as ThirdPersonController;

        if (player != null)
        {
            player.onWalk += () => StartCoroutine(Play("Pasos"));
            player.onJump += () => StartCoroutine(Play("Salto"));
        }

        //Movimiento del personaje: dispara
        WeaponHandler shoot = shootScript as WeaponHandler;

        if (shoot != null)
        {
            shoot.onShoot += () => StartCoroutine(Play("Disparo"));
        }

        //Movimiento deja la pildora
        CapsuleTrigger leavePillSound = leavePill as CapsuleTrigger;
       
       if (leavePillSound != null)
        {
            leavePillSound.onLeavePill += () => StartCoroutine(Play("DejaObjeto"));
        }

        //Sonido del enemigo
        EnemyAI enemy = enemyMovement as EnemyAI;
    
        if (enemy != null)
        {
            enemy.onEnemy  += () => StartCoroutine(Play("Enemigo"));
        }
    }

    ///Método del corazón donde depende de la distancia de el personaje
    
    public void PlayPositional(string nombreSonido, Vector3 position)
        {
            Sound sonido = sounds.Find(s => s.name == nombreSonido);
            if (sonido == null)
            {
                Debug.LogWarning("No se encontró el sonido: " + nombreSonido);
                return;
            }

            GameObject tempGO = new GameObject("PositionalSound_" + nombreSonido);
            tempGO.transform.position = position;

            AudioSource source = tempGO.AddComponent<AudioSource>();
            source.clip = sonido.clip;
            source.volume = sonido.volumen;
            source.spatialBlend = 1f; // sonido 3D
            source.minDistance = 5f;  // ajustable
            source.maxDistance = 20f;
            source.loop = true;
            source.Play();

            // Destruir el objeto cuando el sonido termine (si no es loop)
            if (!source.loop)
            {
                Destroy(tempGO, sonido.clip.length);
            }
        }

    /// Solución para el audi de los enemigos
    
    public AudioSource Create3DAudioSource(string soundName, Transform parent = null)
{
    Sound s = sounds.Find(x => x.name == soundName);
    if (s == null)
    {
        Debug.LogWarning("Sonido no encontrado: " + soundName);
        return null;
    }

    GameObject go = new GameObject("Audio3D_" + soundName);
    if (parent != null)
        go.transform.parent = parent;
    go.transform.localPosition = Vector3.zero;

    AudioSource source = go.AddComponent<AudioSource>();
    source.clip = s.clip;
    source.volume = s.volumen;
    source.spatialBlend = 1f;
    source.rolloffMode = AudioRolloffMode.Linear;
    source.minDistance = 1f;
    source.maxDistance = 20f;
    source.playOnAwake = false;

    return source;
}
}
