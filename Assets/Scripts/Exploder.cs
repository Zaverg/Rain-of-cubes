using System.Linq;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _forceExplosion;

    private Collider[] _buffer = new Collider[20];

    public void Explode()
    {
        int countHits = Physics.OverlapSphereNonAlloc(transform.position, _radius, _buffer);

        foreach (Collider collider in _buffer.Take(countHits))
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_forceExplosion, transform.position, _radius);
        }
    }
}
