using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject area; // Spawn alaný (Plane objesi)
    public GameObject[] prefabs; // Spawn edilecek prefabler
    public int objectCount = 2; // Her prefab için spawn edilecek sayý
    public float boundaryOffset = 2.0f; // Kenarlardan uzaklýk (spawn alanýný daraltýr)

    private Bounds areaBounds;

    private void Start()
    {
        // Alanýn boyutlarýný al
        areaBounds = area.GetComponent<Collider>().bounds;

        // Her prefab için belirtilen sayýda spawn et
        foreach (var prefab in prefabs)
        {
            for (int i = 0; i < objectCount; i++)
            {
                SpawnRandomObject(prefab);
            }
        }
    }

    private void SpawnRandomObject(GameObject prefab)
    {
        // Alanýn içinde rastgele pozisyon seçerken kenarlardan uzaklaþ
        float x = Random.Range(areaBounds.min.x + boundaryOffset, areaBounds.max.x - boundaryOffset);
        float z = Random.Range(areaBounds.min.z + boundaryOffset, areaBounds.max.z - boundaryOffset);
        float y = areaBounds.center.y; // Zemin üzerinde spawn et

        Vector3 randomPosition = new Vector3(x, y, z);

        // Objeyi spawn et
        Instantiate(prefab, randomPosition, Quaternion.identity);
    }
}
