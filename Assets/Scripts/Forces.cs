using UnityEngine;
using System.Collections.Generic;

public class Forces: MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _acceleration = Vector3.zero;

    private void Start()
    {
        _settings.ApplyProfile();
    }
    
    private void Update()
    {
        DistanceBetweenBoids(Generator.birdPrefabs);
        WallForBoid();

        _velocity += _acceleration * Time.deltaTime;
        transform.position += _velocity * Time.deltaTime;
        _acceleration = Vector3.zero;
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
            
            //_velocity += direction * _settings.speed * Time.deltaTime;
            _acceleration += direction * _settings.speed;
        }
    }

    private void WallForBoid()
    {
        Vector3 center = _settings.area.transform.position;
        float radius = _settings.area.transform.localScale.x/2f;

        Vector3 offset = transform.position - center;
        float distance = offset.magnitude;

        if (distance > radius)
        {
            Vector3 directionToCenter = (center - transform.position).normalized;
            _acceleration += directionToCenter * _settings.speed;

            _velocity *= 0.9f;
        }
    }
}