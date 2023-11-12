using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabsAtPoints : MonoBehaviour
{
    public GameObject[] prefabs; // O array de prefabs que voc� deseja spawnar
    public Transform[] spawnPoints; // Os pontos onde os prefabs ser�o spawnados

    private void Start()
    {
        // Crie uma lista de pontos de spawn dispon�veis
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        // Enquanto houver pontos de spawn dispon�veis...
        while (availablePoints.Count > 0)
        {
            // Escolha um prefab aleat�rio para instanciar
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

            // Escolha um ponto de spawn aleat�rio
            int spawnPointIndex = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[spawnPointIndex];

            // Remova o ponto de spawn da lista de pontos dispon�veis
            availablePoints.RemoveAt(spawnPointIndex);

            // Verifique se o prefab foi coletado
            string key = "CollectedItem" + spawnPoint.position.ToString();

            // debug
            Debug.Log("Checking if " + key + " is 0: " + PlayerPrefs.GetInt(key, 0)); 
            //Debug.Log("PlayerPrefs.GetInt(key, 0) = " + PlayerPrefs.GetInt(key, 0));

            if (PlayerPrefs.GetInt(key, 0) == 0)
            {
                // Crie o objeto no ponto de spawn
                Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
