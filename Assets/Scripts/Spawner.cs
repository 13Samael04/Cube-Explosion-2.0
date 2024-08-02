using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;

    private int _initialCubeCount = 6;
    private int _minSpawnCube = 2;
    private int _maxSpawnCube = 6;
    private int _indexToDecreaseScale = 2;
    private int _IndexForDerciseChanceSpleet = 2;

    private void Awake()
    {
        for (int i = 0; i < _initialCubeCount; i++)
        {
            CreateCube(_cube, transform.position);
        }
    }

    private Cube CreateCube(Cube cube, Vector3 startPosition)
    {
        Vector3 position = startPosition + Random.onUnitSphere * cube.transform.localScale.y;

        if(Physics.Linecast(startPosition, position, out RaycastHit hit))
        {
            position = hit.point;
        }

        Cube newCube = Instantiate(cube, position, Quaternion.identity);
        newCube.Splited += CreateReducedCube;

        return newCube;
    }

    private void CreateReducedCube(Cube cube)
    {
        cube.Splited -= CreateReducedCube;
        int countCubes = Random.Range(_minSpawnCube, _maxSpawnCube);
        List<Cube> cubes = new();

        Vector3 scale = cube.transform.localScale / _indexToDecreaseScale;
        float chanceToSplite = cube.ChanceToSplit / _IndexForDerciseChanceSpleet;

        for (int i = 0; i < countCubes; i++)
        {
            Cube newCube = CreateCube(cube, cube.transform.position);
            newCube.Init(scale, chanceToSplite);
            cubes.Add(newCube);
        }

        foreach (Cube spawnedCube in cubes)
        {
            spawnedCube.Rigidbody.AddExplosionForce(1000, cube.transform.position, 20);
        }
    }
}
