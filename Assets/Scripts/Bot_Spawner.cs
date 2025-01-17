using UnityEngine;

public class Bot_Spawner : MonoBehaviour
{
    [SerializeField] GameObject botPrefab;
    [SerializeField] Transform spawnZone;
    [SerializeField] Vector3 zoneSize;
    [SerializeField] int count;

    void Start()
    {
        for (int i = 0; i< count; i++)
            SpawnBot();
    }

    void SpawnBot()
    {
        Vector3 randomPosition = new Vector3(Random.Range(spawnZone.position.x - zoneSize.x / 2, spawnZone.position.x + zoneSize.x / 2),
                                                Random.Range(spawnZone.position.y - zoneSize.y / 2, spawnZone.position.y + zoneSize.y / 2),
                                                spawnZone.position.z);
        Instantiate(botPrefab, randomPosition, Quaternion.identity);
    }
}
