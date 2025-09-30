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

    /*[Header("Orbit")]
    public Vector3 center = Vector3.zero;
    public float maxRadius;
    public float turnStrength;*/
    
    [Header("Types of cloud")]
    public bool denseCloud;
    public bool scatteredCloud;
    public bool longCloud;
    
    [Header("Generator")]
    public int birdCount;
    public Vector3 positionBird;

}
