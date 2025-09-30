using UnityEngine;

[CreateAssetMenu(fileName = "New BoidSettings", menuName = "BoidSettings")]
public class BoidSettings : ScriptableObject
{
    [Header("Boid Prefab")]
    public GameObject birdPrefab;
    
    [Header("Forces")]
    public float force;
    public float speed;
    public float direction;
    
    [Header("Types of force")]
    public bool cohesion;
    public bool separation;
    public bool alignement;
    
    [Header("Generator")]
    public int birdCount;
    public Transform positionBird;

}
