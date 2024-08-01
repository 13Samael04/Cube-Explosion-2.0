using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private float _minChanceToSplit = 0;
    private float _maxChanceToSplit = 100;
    private Explosion _eplosion;

    public event Action<Cube> Splited;

    public Rigidbody Rigidbody { get; private set; }
    public float ChanceToSplit { get; private set; } = 100;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _eplosion = GetComponent<Explosion>();
    }

    public void Initialization(Vector3 scale, float change)
    {
        transform.localScale = scale;
        ChanceToSplit = change;
    }

    private void OnMouseUpAsButton()
    {
        TryToSplit();
    }

    private void TryToSplit()
    {
        float chance = Random.Range(_minChanceToSplit, _maxChanceToSplit);

        if (chance <= ChanceToSplit)
        {
            Splited?.Invoke(this);
        }

        _eplosion.Explode();
        Destroy(gameObject);
    }
}
