using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Forces: MonoBehaviour
{
    [Header("Boid")]
    [SerializeField] private GameObject _birdPrefab;
    [SerializeField] private float _force;
    [SerializeField] private float _speed;
    [SerializeField] private float _direction;

    [Header("Type of force")]
    [SerializeField] private bool _cohesion;
    [SerializeField] private bool _separation;
    [SerializeField] private bool _alignement;
    
    List<GameObject> _birdPrefabs = new List<GameObject>();

    private void DistanceBetweenBoids()
    {
        for (int i = 0; i < _birdPrefabs.Count; i++)
        {
            GameObject boid1 = _birdPrefabs[i];
            Vector3 position1 = boid1.transform.position;

            for (int j = 0; j < _birdPrefabs.Count; j++)
            {
                if (i == j) continue;

                GameObject boid2 = _birdPrefabs[j];
                Vector3 position2 = boid2.transform.position;

                float distance = Vector3.Distance(position1, position2);
            }
        }
    }
}