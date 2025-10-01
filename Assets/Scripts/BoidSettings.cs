using UnityEngine;

[CreateAssetMenu(fileName = "New BoidSettings", menuName = "BoidSettings")]
public class BoidSettings : ScriptableObject
{
    [Header("Boid Prefab")]
    public GameObject birdPrefab;

    [Header("Boid Parameters")]
    [Range(0f, 20f)] public float speed = 5f;
    [Range(0f, 10f)] public float neighborRadius = 5f; // Ray to detect the closest boids
    [Range(0f, 20f)] public float separationRadius = 20f; // Distance before separation between boids
    
    [Header("Activate/Deactivate rules")]
    public bool useCohesion = true; // Dense cloud
    public bool useSeparation = true; // Scattered cloud
    public bool useAlignment = true; // Long cloud
    
    [Header("Weight of rules")]
    [Range(0f, 5f)] public float cohesionWeight = 1f;
    [Range(0f, 5f)] public float separationWeight = 1f;
    [Range(0f, 5f)] public float alignmentWeight = 1f;
    
    [Header("Generator")]
    public int birdCount = 20;
    public Vector3 positionBird = Vector3.zero;

}
