using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectsSpawn : MonoBehaviour
{
    public GameObject[] prefabs; // O array de prefabs que voc� deseja spawnar
    public float spawnRadius = 122f; // Raio em que os objetos podem spawnar
    //public Transform player; // O jogador
    private string playerSeed;

    private void Start()
    {
        playerSeed = this.transform.position.ToString(); // Defina a semente como a posi��o deste objeto
        Random.InitState(playerSeed.GetHashCode()); // Defina a semente aleat�ria para que os objetos sejam spawnados no mesmo lugar toda vez que voc� jogar

        // Chame a fun��o para spawnar um objeto
        Invoke("SpawnObjects", 0.75f);
        //SpawnObjects();
    }

    private void SpawnObjects()
    {
        Random.InitState(playerSeed.GetHashCode());//por algum motivo o unity n�o usa a seed que eu defini no start, ent�o eu defino ela aqui de novo
        foreach (GameObject prefab in prefabs)
        {
            SpawnObject(prefab);
        }
    }

    private void SpawnObject(GameObject prefabToSpawn)
    {
        // Defina uma posi��o aleat�ria dentro dos limites do chunk
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius / 2, spawnRadius / 2), 0, Random.Range(-spawnRadius / 2, spawnRadius / 2));

        //varia dentro do raio
        Vector3 spawnPosition = this.transform.position + randomOffset;

        // Lance um raycast de cima para baixo a partir da altura m�xima
        float maxHeight = 100f; // Altura m�xima desejada
        Ray ray = new Ray(new Vector3(spawnPosition.x, maxHeight, spawnPosition.z), -Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 10f);
            // Se o raycast atingiu algo, ajuste a altura da posi��o para a altura do ponto atingido
            spawnPosition.y = hit.point.y;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10f);
            // Se o raycast n�o atingiu nada, defina a altura da posi��o para 0
            spawnPosition.y = Random.Range(0f, 1f);
        }

        // Crie o objeto na posi��o calculada
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

}
