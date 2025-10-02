using UnityEngine;

public class Area : MonoBehaviour
{
    /*[SerializeField] private BoidSettings _settings;
    [SerializeField] private GameObject _area;

    public Vector3 KeepInsideAreaSphere(GameObject boidObj)
    {
        Vector3 acceleration = Vector3.zero;

        Vector3 center = _area.transform.position;
        float radius = _settings.maxRadius;
        
        Vector3 dirToCenter = center - boidObj.transform.position;
        float distance = dirToCenter.magnitude;

        if (distance > radius)
        {
            acceleration = dirToCenter.normalized * _settings.turnStrength;
        }

        return acceleration;
    }*/
}