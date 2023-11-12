using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // O objeto (árvore) que você deseja spawnar
    public int gridSize = 10; // Tamanho da grade para espaçamento das árvores
    public float treeDensity = 0.5f; // Densidade das árvores
    public float maxHeight = 100f; // Altura máxima para lançar o raycast
    public LayerMask groundLayer; // Camada que representa o terreno

    private void Start()
    {
        GenerateTrees();
    }

    void GenerateTrees()
    {
        for (int x = -gridSize / 2; x < gridSize / 2; x++)
        {
            for (int z = -gridSize / 2; z < gridSize / 2; z++)
            {
                if (Random.value < treeDensity)
                {
                    Vector3 spawnPosition = new Vector3(transform.position.x + x, maxHeight, transform.position.z + z);
                    RaycastHit hit;

                    // Lançar um raycast de cima para baixo para encontrar a altura do terreno
                    if (Physics.Raycast(spawnPosition, Vector3.down, out hit, Mathf.Infinity, groundLayer))
                    {
                        // Se o raycast atingir o terreno, gere a árvore nessa posição
                        Instantiate(treePrefab, hit.point, Quaternion.identity);
                    }
                }
            }
        }
    }
}
