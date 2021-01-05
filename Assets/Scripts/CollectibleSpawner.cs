using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _spawnDelaySec;
    [SerializeField] private CollectibleObject[] _objectsToSpawn;
    [SerializeField] private int _maxNumberOfSpawnedObjects = 100;

    private float _timeAfterLastSpawn;
    private bool _isSpawning;

    private void Start()
    {
        _timeAfterLastSpawn = 0;
        _isSpawning = false;
    }

    private void Update()
    {
        if (_isSpawning)
        {
            _timeAfterLastSpawn += Time.deltaTime;

            if (_timeAfterLastSpawn >= _spawnDelaySec && transform.childCount <= _maxNumberOfSpawnedObjects)
            {
                SpawnRandomObject();
                _timeAfterLastSpawn = 0;
            }
        }       
    }

    public void StartSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    public void SetObjectsToSpawn(CollectibleObject[] objectsToSpawn)
    {
        _objectsToSpawn = objectsToSpawn;
    }

    private void SpawnRandomObject()
    {
        CollectibleObject newObject = Instantiate(_objectsToSpawn[Random.Range(0, _objectsToSpawn.Length)],_player.transform.position + RandomPlaceOnSphere(_spawnRadius), Quaternion.identity,gameObject.transform);
        Vector3 lookDirection = _player.transform.position - newObject.transform.position;
        newObject.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void Clear()
    {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private Vector3 RandomPlaceOnSphere(float radius)
    {
        return Random.onUnitSphere * radius;

    }

    public void ConfigureAtLevelStart(Level level)
    {
        foreach(CollectibleObject item in _objectsToSpawn)
        {
            item.SetReward(AssignReward(item,level));
        }

        StartSpawning();
    }

    private int AssignReward(CollectibleObject item, Level currentLevel)
    {
        if (currentLevel.GoodTypes.Contains(item.GetFoodType()))
        {
            return currentLevel.Reward;
        }
        else if (currentLevel.BadTypes.Contains(item.GetFoodType()))
        {
            return currentLevel.Fine;
        }
        else
        {
            return 0;
        }
    }
}
