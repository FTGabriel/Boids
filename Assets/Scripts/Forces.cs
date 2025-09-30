using UnityEngine;
using System.Collections.Generic;

public class Forces: MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    private Vector3 _velocity;
    
    List<GameObject> birds = Generator.birdPrefabs;

    private void Start()
    {
        birds = Generator.birdPrefabs;

        // Initial direction
        /*float initialAngle = _settings.direction * Mathf.Deg2Rad;
        _velocity = new Vector3(Mathf.Cos(initialAngle), Mathf.Sin(initialAngle), 0) * _settings.speed;*/
    }

    private void Update()
    {
        /*Vector3 directionToCenter = (_settings.center - transform.position).normalized;
        float distance = directionToCenter.magnitude;

        if (distance > _settings.maxRadius)
        {
            Vector3 correction = directionToCenter.normalized * _settings.turnStrength * Time.deltaTime;
            _velocity = (_velocity + correction).normalized * _settings.speed;
        }

        // Movement
        transform.position += _velocity * Time.deltaTime;

        // Rotation
        float angleDegre = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleDegre);*/
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