using UnityEngine;

public class PrefabGenerator : MonoBehaviour
{
    public GameObject prefab;  // O prefab que você quer instanciar
    public int numPrefabs;  // O número de prefabs que você quer instanciar por chunk
    public float chunkSize;  // O tamanho do seu chunk
    public float raycastHeight;  // A altura de onde o raycast é lançado

    void Start()
    {
        for (int i = 0; i < numPrefabs; i++)
        {
            // Gere uma posição aleatória dentro do chunk
            Vector3 position = new Vector3(
                Random.Range(0, chunkSize),
                raycastHeight,
                Random.Range(0, chunkSize)
            );

            // Lance um raycast para baixo a partir dessa posição
            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit))
            {
                // Se o raycast atingir algo, instancie o prefab nessa posição
                Instantiate(prefab, hit.point, Quaternion.identity, transform);
            }
        }
    }
}
