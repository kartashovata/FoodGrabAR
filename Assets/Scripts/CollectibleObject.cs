using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleObject : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _collectionEffect;
    [SerializeField] private int _reward = 5;

    public int Reward { get; private set; }

    public event UnityAction<CollectibleObject> Grabbed;

    public void GetCollected()
    {
        Grabbed?.Invoke(this);
        //Instantiate(_collectionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetReward(int newReward)
    {
        _reward = newReward;
    }
}
