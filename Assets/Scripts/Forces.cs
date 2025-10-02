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
            // Initial direction
            /*float initialAngle = _settings.direction * Mathf.Deg2Rad;
            _velocity = new Vector3(Mathf.Cos(initialAngle), Mathf.Sin(initialAngle), 0) * _settings.speed;*/
        }
    }
    private void Update()
    {
        // for (int k = 0; k < 100; k++)
        //{
            BoidsEvolution2();
         //   Timer t = new Timer(100);
        // }
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

    private void BoidsEvolution()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            GameObject boid1 = birds[i];
            Vector3 position1 = boid1.transform.position;
            Vector3 force = Vector3.zero;
            Vector3 AverageVelocity = Vector3.zero;
            Vector3 Velocity = Vector3.zero;
            int p = 0;
            for (int j = 0; j < birds.Count; j++)
            {
                if (i == j) continue;

                GameObject boid2 = birds[j];
                Vector3 position2 = boid2.transform.position;

                float distance = Vector3.Distance(position1, position2);

                if (distance < _settings.perceptionRadius)
                {
                    p++;
                    // calcul  (alignment, cohesion, separation)
                    force += position2; 
                    AverageVelocity += boid2.GetComponent<Forces>()._velocity;
                }
                
            }
            if (p > 0)
            {
                force /= p; // Average position of nearby boids
                Vector3 cohesion = (force - position1).normalized * _settings.cohesionSpeed;
                Vector3 separation = - (force - position1) * _settings.separationSpeed;
                if (separation.x > 1f) separation.x = 1f;
                if (separation.x < -1f) separation.x = -1f;
                if (separation.y > 1f) separation.y = 1f;
                if (separation.y < -1f) separation.y = -1f;
                if (separation.z > 1f) separation.z = 1f;
                if (separation.z < -1f) separation.z = -1f;
                AverageVelocity /= p; // Average velocity of nearby boids
                AverageVelocity *= _settings.alignementSpeed;
                Velocity = cohesion + separation + AverageVelocity;
                position1 += Velocity * Time.deltaTime;
                if (position1.x > 100f) position1.x = - 100f;
                if (position1.x < -100f) position1.x = 100f;
                if (position1.y > 100f) position1.y = - 100f;
                if (position1.y < -100f) position1.y = 100f;
                if (position1.z > 100f) position1.z = -100f;
                if (position1.z < -100f) position1.z = 100f;
                boid1.transform.position = position1;
                if (Velocity.x > 1f) Velocity.x = 1f;
                if (Velocity.x < -1f) Velocity.x = -1f;
                if (Velocity.y > 1f) Velocity.y = 1f;
                if (Velocity.y < -1f) Velocity.y = -1f;
                if (Velocity.z > 1f) Velocity.z = 1f;
                if (Velocity.z < -1f) Velocity.z = -1f;
                boid1.GetComponent<Forces>()._velocity = Velocity;
                birds[i] = boid1;

            }
            else 
            {
                Velocity = boid1.GetComponent<Forces>()._velocity;
                position1 += Velocity * Time.deltaTime;
                if (position1.x > 100f) position1.x = -100f;
                if (position1.x < -100f) position1.x = 100f;
                if (position1.y > 100f) position1.y = -100f;
                if (position1.y < -100f) position1.y = 100f;
                if (position1.z > 100f) position1.z = -100f;
                if (position1.z < -100f) position1.z = 100f;   
                boid1.transform.position = position1; 
                birds[i] = boid1;
            }
        }
    }

    private void BoidsEvolution1()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            GameObject boid1 = birds[i];
            Vector3 position1 = boid1.transform.position;
            Vector3 force = Vector3.one;
            position1 += force;
            if (position1.x > 10f) position1.x = -10f;
            if (position1.x < -10f) position1.x = 10f;
            if (position1.y > 10f) position1.y = -10f;
            if (position1.y < -10f) position1.y = 10f;
            if (position1.z > 10f) position1.z = -10f;
            if (position1.z < -10f) position1.z = 10f;
            boid1.transform.position = position1;
            birds[i] = boid1;
        }
    }

    private void BoidsEvolution2()
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