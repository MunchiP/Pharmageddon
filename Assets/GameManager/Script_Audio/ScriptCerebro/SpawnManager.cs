using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
 // Array para los elementos
    public GameObject[] objetosPrefabs;
    public int poolSize; // Referncia específica al número de objetos existentes en el juego - 10 según requisito
    [SerializeField] List<GameObject> pooledObjets = new List<GameObject>(); // Almaceno los objetos instanciados usados o no

    // Espeficaciones de tiempos e intervalos
    public float spawnTimer = 0f; 
    public float spawnInteral = 0.2f;  // Aparecen los objetos cada 2 segundos según requisito

    public BoxCollider spawnArea;    // Cambiando de método para optimizar el área de creación de elementos

    void Start()
    {

        AddToPool(poolSize);       // Inicializo la función de agregar a la piscina como elemento de entrada - el tamaño de la piscina
    }

    void Update()
    { 
        SpawnRandomEnemy();
    }

        // -M- método para agregar el enemigo creado en el área específica determinada
        void SpawnRandomEnemy()
    {
        spawnTimer += Time.deltaTime;

        // Si la lista de objetos es menor que el tamaño de la piscina, entonces use el método de agregar un objeto
            // Comentado para que no se vuelvan a crear los enemigos sino que solamente queden los 15
       
        if (pooledObjets.Count < poolSize)
        {
            AddToPool(poolSize - pooledObjets.Count);
        }

        if (spawnTimer >= spawnInteral)
        {
            spawnTimer = 0f;
            GameObject temporal = FistDesative();            // Obtiene un objeto de la piscina

            if (temporal != null)
            {
                 // El prefab no va a guardar la etiqueta de jugador que se le ha asignado al elemento en el inspector
                 // porque los prefabs no pueden guadar instancias de la scena
                 // entonces es mejor dejarlo asignado por código
                 // así a cada molde que se crea se le crea con la asignación de etiqueta de una vez
                 
                 Vector3 posicionRandom = GetRandomPointSpawnArea();
                 NavMeshHit hit; // En Unity: almacena el resultado de la búsqueda en el NavMesh, el anterior

                // out hit: una forma que tiene C# para decir que la función se llenará con el resultado de la busqueda
                // 2f: Unity busca un punto en el NavMesh de sario de 2 unidades para la creación
                if (NavMesh.SamplePosition(posicionRandom, out hit, 2f, NavMesh.AllAreas))
                    {
                        temporal.transform.position = hit.position;
                        AgentBehaivour behaivour = temporal.GetComponent<AgentBehaivour>();
                        if (behaivour != null)
                            {
                                behaivour.target = GameObject.FindWithTag("Player").transform;
                            }
                    }
                 temporal.SetActive(true);
            }
        }
    }

        // -M- método del área de creación de los elementos
        Vector3 GetRandomPointSpawnArea()
        {
            Bounds bounds = spawnArea.bounds;
            float x = Random.Range(bounds.min.x , bounds.max.x);
            float z = Random.Range(bounds.min.z , bounds.max.z);
            float y = bounds.center.y;

            return new Vector3(x , y , z);
        }

        // -M- método para la verificación y creación de un objeto en la Pool
        GameObject FistDesative()
        {
            // Recorre cada uno de los objetos  y verifica 
            foreach (GameObject item in pooledObjets)
            {
                // sí el item no está activado en la Herencia - Inspector y si ya no existen
                if (item != null && !item.activeInHierarchy) 
                {
                    //Entonre regrese el item
                    return item; 
                }
            }
                 // En caso de que todos los elementos estén creados, entonces crea uno nuevo
            return null;
        }

        // -M- método de la Pool -- agrego como parámetro de entrada el tamaño de la Pool declararo arriva, intervenido en el inspector
        void AddToPool( int poolSize)
        {
            for (int i = 0; i < poolSize; i++) // Recorro mi array hasta el tamaño de la Pool
            {
                // Selecciona el prefab aleatorio del arreglo de objetosPrefab
                // (0, objetosPrefabs.Lenght) aquí es: "0", porque el arreglo asume donde empieza pero no donde termina
                int randomPrefab = Random.Range(0, objetosPrefabs.Length);
                // Toma ese prefab de la lista
                GameObject prefabs = objetosPrefabs[randomPrefab];
                // Lo instancia en la posición 0 y sin rotación
                GameObject objetoCreado = Instantiate(prefabs, Vector3.zero, Quaternion.identity);
                // Los crea pero los deja en falso papra después usarlos
                objetoCreado.SetActive(false);
                // Luego de crear el objeto, de manera random, de la lista e instanciarlo en la posición 
                // Los agrego a la piscina
                pooledObjets.Add(objetoCreado);



                    // Bonus de colores en los objetos
                    Renderer rend = objetoCreado.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        rend.material.color = new Color(Random.value, Random.value, Random.value);
                    }

            }
        }



}
