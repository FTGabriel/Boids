using UnityEngine;

public enum BoidProfile
{
    Normal,
    Late,
    Happy,
    Annoying
}

[CreateAssetMenu(fileName = "New BoidSettings", menuName = "BoidSettings")]
public class BoidSettings : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject birdPrefab;
    public GameObject area;
    
    [Header("Profile Settings")]
    public BoidProfile profile = BoidProfile.Normal;
    
    [Header("Forces of movement")]
    public float force;
    public float speed;
    [Range(-5f, 20f)] public float cohesionSpeed;
    [Range(-5f, 20f)] public float separationSpeed;
    [Range(-5f, 20f)] public float alignementSpeed;
    public float direction;
    public float perceptionRadius;
    
    [Header("Orbit")]
    public Vector3 center = Vector3.zero;   // Centre point of the cloud
    public float maxRadius = 50f;           // Maximum radius before recall
    public float turnStrength = 2f;         // Force of return to centre
    
    [Header("Generator")]
    public int birdCount = 20;
    public Vector3 positionBird = Vector3.zero;
}
