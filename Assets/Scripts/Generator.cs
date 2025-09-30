using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    
    public static List<GameObject> birdPrefabs = new List<GameObject>();

    private void Start()
    {
        RandomSpawn();
    }

    private void RandomSpawn()
    {
        for (int i = 0; i < _settings.birdCount; i++)
        {
            Vector3 spawnPos = _settings.positionBird.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            GameObject bird = Instantiate(_settings.birdPrefab, spawnPos, Quaternion.identity);
            
            birdPrefabs.Add(bird);
        }
    }
}
