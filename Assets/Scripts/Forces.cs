using UnityEngine;
using System.Collections.Generic;

public class Forces: MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    private Vector3 _velocity;

    private void Start()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        _velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _settings.speed;
    }
    
    private void Update()
    {
        DistanceBetweenBoids(Generator.birdPrefabs);
        
        transform.position += _velocity * Time.deltaTime;
    }

    private void DistanceBetweenBoids(List<GameObject> birds)
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        int neighborCount = 0;

        foreach (GameObject other in birds)
        {
            if (other == this.gameObject) continue;
            
            float distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance < _settings.neighborRadius)
            {
                neighborCount++;
                
                // Cohesion
                cohesion += other.transform.position;
                
                // Separation
                if (distance < _settings.separationRadius)
                {
                    separation += (transform.position - other.transform.position).normalized / distance;
                }
                
                // Alignment
                Forces otherBoid = other.GetComponent<Forces>();
                if (otherBoid != null)
                {
                    alignment += otherBoid._velocity;
                }
                
                // Visual debug : green line to each boid
                Debug.DrawLine(transform.position, other.transform.position, Color.cyan);
            }
        }

        if (neighborCount > 0)
        {
            cohesion /= neighborCount;
            cohesion = (cohesion - transform.position).normalized;
            
            separation /= neighborCount;
            separation = separation.normalized;
            
            alignment /= neighborCount;
            alignment = alignment.normalized;
            
            Vector3 direction = Vector3.zero;

            if (_settings.useCohesion)
                direction += cohesion * _settings.cohesionWeight;

            if (_settings.useSeparation)
                direction += separation * _settings.separationWeight;

            if (_settings.useAlignment)
                direction += alignment * _settings.alignmentWeight;
            
            _velocity = Vector3.Lerp(_velocity, direction * _settings.speed, Time.deltaTime);

            if (_velocity.sqrMagnitude > 0.001f)
            {
                float angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}