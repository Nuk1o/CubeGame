using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;

    private Queue<Cube> _cubesQueue = new Queue<Cube>();
    [SerializeField] private int _cubesQueueCapacity = 20;
    [SerializeField] private bool _autoQueueGrow = true;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber;

    private int maxPower = 12;

    private Vector3 _defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;

        _defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        InitializeCubeQueue();
    }

    private void InitializeCubeQueue()
    {
        for (int i = 0; i < _cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }
    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(_cubePrefab, _defaultSpawnPosition, Quaternion.identity, transform)
            .GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        _cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (_cubesQueue.Count == 0)
        {
            if (_autoQueueGrow)
            {
                _cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("[Cubes Queue] : no more cubes");
                return null;
            }
        }

        Cube cube = _cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);
        
        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), _defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.cubeRigidBody.velocity = Vector3.zero;
        cube.cubeRigidBody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        _cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }

    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number)/Mathf.Log(2))-1];
    }
}
