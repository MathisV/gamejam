using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameObject[] obstacles;

    public Transform player;

    public float minSpawnTime = 2;

    public float maxSpawnTime = 15;

    private float y = 7;

    void Start()
    {
        Invoke("RandomSpawn", 1);
    }

    void RandomSpawn()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        int index = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[index],
        new Vector3(Mathf.FloorToInt(player.position.x) + 50, y, 0),
        Quaternion.identity);

        Invoke("RandomSpawn", spawnTime);
    }
}
