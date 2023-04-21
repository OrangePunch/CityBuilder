using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectPrefab;
    [SerializeField] private float _minSpawnX;
    [SerializeField] private float _maxSpawnX;
    [SerializeField] private float _minSpawnY;
    [SerializeField] private float _maxSpawnY;
    [SerializeField] private float _minSpawnZ;
    [SerializeField] private float _maxSpawnZ;
    [SerializeField] private float _startDelay = 2;
    [SerializeField] private float _repeatRate;
    [SerializeField] private float _repeatRangeOne;
    [SerializeField] private float _repeatRangeTwo;

    private void Start()
    {
        InvokeRepeating("SpawnObjects", _startDelay, _repeatRate);
    }

    private void SpawnObjects()
    {
        int objectPrefabIndex = Random.Range(0, _objectPrefab.Length);
        var spawnPos = new Vector3(Random.Range(_minSpawnX, _maxSpawnX), Random.Range(_minSpawnY, _maxSpawnY), Random.Range(_minSpawnZ, _maxSpawnZ));
        _repeatRate = Random.Range(_repeatRangeOne, _repeatRangeTwo);
        Instantiate(_objectPrefab[objectPrefabIndex], spawnPos, _objectPrefab[objectPrefabIndex].transform.rotation);
        Invoke("SpawnObjects", _repeatRate);
    }
}
