using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject area; // Spawn alan� (Plane objesi)
    public GameObject[] prefabs; // Spawn edilecek prefabler
    public int objectCount = 2; // Her prefab i�in spawn edilecek say�
    public float boundaryOffset = 2.0f; // Kenarlardan uzakl�k (spawn alan�n� daralt�r)

    private Bounds areaBounds;

    private void Start()
    {
        // Alan�n boyutlar�n� al
        areaBounds = area.GetComponent<Collider>().bounds;

        // Her prefab i�in belirtilen say�da spawn et
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
        // Alan�n i�inde rastgele pozisyon se�erken kenarlardan uzakla�
        float x = Random.Range(areaBounds.min.x + boundaryOffset, areaBounds.max.x - boundaryOffset);
        float z = Random.Range(areaBounds.min.z + boundaryOffset, areaBounds.max.z - boundaryOffset);
        float y = areaBounds.center.y; // Zemin �zerinde spawn et

        Vector3 randomPosition = new Vector3(x, y, z);

        // Objeyi spawn et
        Instantiate(prefab, randomPosition, Quaternion.identity);
    }
}
