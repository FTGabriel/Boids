using UnityEngine;
using System.Collections.Generic;

public class Forces: MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    
    List<GameObject> birds = Generator.birdPrefabs;

    private void Start()
    {
        birds = Generator.birdPrefabs;
    }

    private void DistanceBetweenBoids()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            GameObject boid1 = birds[i];
            Vector3 position1 = boid1.transform.position;

            for (int j = 0; j < birds.Count; j++)
            {
                if (i == j) continue;

                GameObject boid2 = birds[j];
                Vector3 position2 = boid2.transform.position;

                float distance = Vector3.Distance(position1, position2);
            }
        }
    }
}