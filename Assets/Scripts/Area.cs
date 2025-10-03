using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 10f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            IRepulse repulsable = hit.GetComponent<IRepulse>();
            if (repulsable != null)
            {
                repulsable.Repulse(transform.position, force, Time.deltaTime);
            }
        }
    }
}