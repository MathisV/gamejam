using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesDestroyer : MonoBehaviour
{
    public Transform player;

    public float distanceThreshold = 10.0f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = player.position.x - transform.position.x;
            if (distance > distanceThreshold)
            {
                Destroy (gameObject);
            }
        }
    }
}
