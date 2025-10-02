using UnityEngine;
using System.Collections.Generic;
using System.Timers;

public class Forces: MonoBehaviour
{
    [SerializeField] private BoidSettings _settings;
    private Vector3 _velocity;
    
    List<GameObject> birds = Generator.birdPrefabs;

    public Forces(List<GameObject> birds)
    {
        this.birds = birds;
    }

    private void Start()
    {
        birds = Generator.birdPrefabs;
        for (int i = 0; i < birds.Count; i++)
        {
            GameObject boid1 = birds[i];
            Vector3 initVelocity = Random.insideUnitSphere.normalized * _settings.speed;
            boid1.GetComponent<Forces>()._velocity = initVelocity;
            birds[i] = boid1;
        }
    }
    private void Update()
    {
        BoidsEvolution();
    }
    
    private void BoidsEvolution()
    {
        // Temporary lists are prepared to store the new positions and speeds
        Vector3[] newPositions = new Vector3[birds.Count];
        Vector3[] newVelocities = new Vector3[birds.Count];

        for (int i = 0; i < birds.Count; i++)
        {
            GameObject boid1 = birds[i];
            Vector3 position1 = boid1.transform.position;
            Vector3 velocity1 = boid1.GetComponent<Forces>()._velocity;

            Vector3 avgPosition = Vector3.zero;
            Vector3 avgVelocity = Vector3.zero;
            Vector3 separation = Vector3.zero;

            int neighborCount = 0;

            // --- Search for neighbours ---
            for (int j = 0; j < birds.Count; j++)
            {
                if (i == j) continue;

                GameObject boid2 = birds[j];
                Vector3 position2 = boid2.transform.position;

                float distance = Vector3.Distance(position1, position2);

                if (distance < _settings.perceptionRadius)
                {
                    neighborCount++;

                    // Cohesion (centre of neighbours)
                    avgPosition += position2;

                    // Alignement (average of velocities)
                    avgVelocity += boid2.GetComponent<Forces>()._velocity;

                    // Separation (avoid collision)
                    separation += (position1 - position2) / (distance * distance);
                }
            }

            Vector3 acceleration = Vector3.zero;

            if (neighborCount > 0)
            {
                avgPosition /= neighborCount;
                avgVelocity /= neighborCount;

                // Cohesion
                Vector3 cohesion = (avgPosition - position1).normalized * _settings.cohesionSpeed;

                // Alignement
                Vector3 alignment = avgVelocity.normalized * _settings.alignementSpeed;

                // Separation
                separation *= _settings.separationSpeed;

                acceleration += ApplyProfile(cohesion, alignment, separation, boid1);
            }

            // --- Force pulling towards the global centre ---
            Vector3 directionToCenter = (_settings.center - position1);
            float distanceFromCenter = directionToCenter.magnitude;

            if (distanceFromCenter > _settings.maxRadius)
            {
                Vector3 centeringForce = directionToCenter.normalized * _settings.turnStrength;
                acceleration += centeringForce;
            }

            // ---  Update speed ---
            velocity1 += acceleration * Time.deltaTime;
            velocity1 = Vector3.ClampMagnitude(velocity1, _settings.speed);

            // --- Update position ---
            position1 += velocity1 * Time.deltaTime;

            // Temporary storage
            newPositions[i] = position1;
            newVelocities[i] = velocity1;
        }

        // After the loop, apply the updates
        for (int i = 0; i < birds.Count; i++)
        {
            GameObject boid = birds[i];
            boid.transform.position = newPositions[i];
            boid.GetComponent<Forces>()._velocity = newVelocities[i];
        }
    }
    
    private Vector3 ApplyProfile(Vector3 cohesion, Vector3 alignment, Vector3 separation, GameObject boidObj)
    {
        Boid boid = boidObj.GetComponent<Boid>();
        
        switch (boid.profile)
        {
            case BoidProfile.Late:
                cohesion *= 0.3f;   // weak cohesion
                SetBoidColor(boidObj, Color.blue);
                break;

            case BoidProfile.Happy:
                cohesion *= Random.Range(0.5f, 1.5f); 
                alignment *= Random.Range(0.5f, 1.5f);
                separation *= Random.Range(0.5f, 1.5f);
                SetBoidColor(boidObj, Color.yellow);
                break;

            case BoidProfile.Annoying:
                separation *= 0.2f; // don't avoid a lot collisions
                SetBoidColor(boidObj, Color.green);
                break;

            default: // Normal
                SetBoidColor(boidObj, Color.white);
                break;
        }

        return cohesion + alignment + separation;
    }
    
    private void SetBoidColor(GameObject boidObj, Color color)
    {
        Renderer rend = boidObj.GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = color;
    }
}