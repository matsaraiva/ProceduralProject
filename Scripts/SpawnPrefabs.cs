using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    public GameObject[] prefabs; // O array de prefabs que você deseja spawnar
    private float spawnRadius = 122f; // Raio em que os objetos podem spawnar
    public int minObjects = 1; // Número mínimo de objetos para spawnar
    public int maxObjects = 5; // Número máximo de objetos para spawnar
    public Transform player; // O jogador
    //public float minDistance = 244f; // Distância mínima do jogador para spawnar objetos
    private string chunkSeed; // A semente do chunk

    private void Start()
    {
        chunkSeed = this.transform.name + this.transform.position.ToString(); // Defina a semente do chunk como o nome mais a posição deste objeto
        Random.InitState(chunkSeed.GetHashCode()); // Defina a semente aleatória para que os objetos sejam spawnados no mesmo lugar toda vez que você jogar, baseado no chunk

        //player = GameObject.FindGameObjectWithTag("Player").transform; // O transform do jogador

        //Debug.Log("Random test 1:" + Random.Range(0, 100));

        // Chame a função para spawnar um objeto
        Invoke("SpawnObjects", 0.75f);
        //SpawnObjects();
    }

    private void SpawnObjects()
    {
        //Debug.Log("Random test 2:" + Random.Range(0, 100));

        Random.InitState(chunkSeed.GetHashCode());//por algum motivo o unity não usa a seed que eu defini no start, então eu defino ela aqui de novo

        if (true)//(Vector3.Distance(player.position, transform.position) < minDistance)
        {
            int numObjects = Random.Range(minObjects, maxObjects + 1); // Número aleatório de objetos para spawnar

            for (int i = 0; i < numObjects; i++)
            {
                SpawnObject();
            }
        }
    }

    private void SpawnObject()
    {
        // Defina uma posição aleatória dentro dos limites do chunk
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius / 2, spawnRadius / 2), 0, Random.Range(-spawnRadius / 2, spawnRadius / 2));

        //varia dentro do raio
        Vector3 spawnPosition = this.transform.position + randomOffset;

        // Lance um raycast de cima para baixo a partir da altura máxima
        float maxHeight = 100f; // Altura máxima desejada
        Ray ray = new Ray(new Vector3(spawnPosition.x, maxHeight, spawnPosition.z), -Vector3.up);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 10f);
            // Se o raycast atingiu algo, ajuste a altura da posição para a altura do ponto atingido
            spawnPosition.y = hit.point.y;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10f);
            // Se o raycast não atingiu nada, defina a altura da posição para 0
            spawnPosition.y = Random.Range(0f, 1f);
        }

        // Escolha um prefab aleatório para instanciar
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

        // Crie uma rotação aleatória em torno do eixo Y
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // Crie o objeto na posição calculada com a rotação aleatória e faça dele um filho deste objeto
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, randomRotation, transform);
    }
}
