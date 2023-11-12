using UnityEngine;

public class PrefabGenerator : MonoBehaviour
{
    public GameObject prefab;  // O prefab que voc� quer instanciar
    public int numPrefabs;  // O n�mero de prefabs que voc� quer instanciar por chunk
    public float chunkSize;  // O tamanho do seu chunk
    public float raycastHeight;  // A altura de onde o raycast � lan�ado

    void Start()
    {
        for (int i = 0; i < numPrefabs; i++)
        {
            // Gere uma posi��o aleat�ria dentro do chunk
            Vector3 position = new Vector3(
                Random.Range(0, chunkSize),
                raycastHeight,
                Random.Range(0, chunkSize)
            );

            // Lance um raycast para baixo a partir dessa posi��o
            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit))
            {
                // Se o raycast atingir algo, instancie o prefab nessa posi��o
                Instantiate(prefab, hit.point, Quaternion.identity, transform);
            }
        }
    }
}
