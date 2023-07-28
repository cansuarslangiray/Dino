using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject Prefab;
        [Range(0f, 1f)] public float SpawnChance;
    }

    [SerializeField] private SpawnableObject[] _objects;

    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChange = Random.value;
        foreach (var obj in _objects)
        {
            if (spawnChange < obj.SpawnChance)
            {
                GameObject obstacle = Instantiate(obj.Prefab);
                if (obstacle.name.Equals("Bird"))
                {
                    obstacle.transform.position += new Vector3(transform.position.x * 0.2f, transform.position.y,
                        transform.position.z);
                }
                else
                {
                    obstacle.transform.position += transform.position;
                }
                break;
            }

            spawnChange -= obj.SpawnChance;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));

    }
}