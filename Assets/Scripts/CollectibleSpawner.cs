using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private CollectibleObject[] _objects;

    private void Start()
    {
        StartCoroutine(SpawnRandomObject());
    }

    private IEnumerator SpawnRandomObject()
    {
        while (true)
        {
            CollectibleObject newObject = Instantiate(_objects[Random.Range(0, _objects.Length)], RandomPlaceInSphere(_spawnRadius), Quaternion.identity);
            Vector3 lookDirection = _player.transform.position - newObject.transform.position;
            newObject.transform.rotation = Quaternion.LookRotation(lookDirection);
            newObject.Grabbed += OnObjectGrabbed;

            yield return new WaitForSeconds(_secondsBetweenSpawn);
        }
    }

    private Vector3 RandomPlaceInSphere(float radius)
    {
        return Random.insideUnitSphere * radius;
    }

    private void OnObjectGrabbed(CollectibleObject grabbedObject)
    {
        grabbedObject.Grabbed -= OnObjectGrabbed;

        _player.AddScore(grabbedObject.Reward);
    }
}
