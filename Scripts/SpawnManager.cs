using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn; // O prefab do objeto que você deseja spawnar
    public float spawnRadius = 10f; // Raio em que os objetos podem spawnar em relação ao jogador
    public float spawnInterval = 2f; // Intervalo de tempo entre os spawns

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform; // O transform do jogador

        // Inicialize a função para spawnar objetos repetidamente com um intervalo
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }


    private void SpawnObject()
    {
        // Defina uma posição aleatória dentro do raio definido
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;

        // Certifique-se de que a esfera permaneça dentro do jogador
        Vector3 playerPosition = playerTransform.position; // Posição do jogador
        Vector3 spawnPosition = playerPosition + randomOffset;

        // Lance um raycast de cima para baixo a partir da altura máxima
        float maxHeight = 100f; // Altura máxima desejada
        Ray ray = new Ray(new Vector3(spawnPosition.x, maxHeight, spawnPosition.z), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Se o raycast atingiu algo, ajuste a altura da posição para a altura do ponto atingido
            spawnPosition.y = hit.point.y + 1f;

            // Crie o objeto na posição calculada
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }



}
