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
            Vector3 spawnPos = _settings.positionBird + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            GameObject bird = Instantiate(_settings.birdPrefab, spawnPos, Quaternion.identity);
            
            // Random profile of boids
            Boid boidComponent = bird.GetComponent<Boid>();
            if (boidComponent != null)
            {
                float rand = Random.value;

                if (rand < 0.7f) boidComponent.profile = BoidProfile.Normal;      // 70%
                else if (rand < 0.85f) boidComponent.profile = BoidProfile.Late; // 15%
                else if (rand < 0.95f) boidComponent.profile = BoidProfile.Happy; // 10%
                else boidComponent.profile = BoidProfile.Annoying;                // 5%
            }
            
            birdPrefabs.Add(bird);
        }
    }
}
