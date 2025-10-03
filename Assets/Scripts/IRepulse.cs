using UnityEngine;

public interface IRepulse
{
    void Repulse(Vector3 center, float force, float deltaTime);
}