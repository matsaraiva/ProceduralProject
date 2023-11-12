using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn; // O prefab do objeto que voc� deseja spawnar
    public float spawnRadius = 10f; // Raio em que os objetos podem spawnar em rela��o ao jogador
    public float spawnInterval = 2f; // Intervalo de tempo entre os spawns

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform; // O transform do jogador

        // Inicialize a fun��o para spawnar objetos repetidamente com um intervalo
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }


    private void SpawnObject()
    {
        // Defina uma posi��o aleat�ria dentro do raio definido
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;

        // Certifique-se de que a esfera permane�a dentro do jogador
        Vector3 playerPosition = playerTransform.position; // Posi��o do jogador
        Vector3 spawnPosition = playerPosition + randomOffset;

        // Lance um raycast de cima para baixo a partir da altura m�xima
        float maxHeight = 100f; // Altura m�xima desejada
        Ray ray = new Ray(new Vector3(spawnPosition.x, maxHeight, spawnPosition.z), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Se o raycast atingiu algo, ajuste a altura da posi��o para a altura do ponto atingido
            spawnPosition.y = hit.point.y + 1f;

            // Crie o objeto na posi��o calculada
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }



}
