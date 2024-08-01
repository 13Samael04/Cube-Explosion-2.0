using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radiusExplosion = 40;
    [SerializeField] private float _forceExplosion = 200;

    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    public void Explode()
    {
        float radius = _radiusExplosion / _cube.transform.localScale.x;
        float force = _forceExplosion / _cube.transform.localScale.x;

        foreach (Rigidbody cube in GetCubesRigitbody())
        {
            cube.AddExplosionForce(force, transform.position, radius);
        }
    }

    private List<Rigidbody> GetCubesRigitbody()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radiusExplosion);
        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if(hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}