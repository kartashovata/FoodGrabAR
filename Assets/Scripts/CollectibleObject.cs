using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _collectionEffect;
    [SerializeField] private int _reward = 5;

    public int Reward { get => _reward; }
    [SerializeField] private Types _type;

    

    public void GetCollected()
    {
        //Instantiate(_collectionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetReward(int newReward)
    {
        _reward = newReward;
    }

    public Types GetFoodType()
    {
        return _type;
    }
}

public enum Types
{
    NonEatable,
    NeutralEatable,
    Sweet,
    Healthy,
    Fruit,
    Fastfood
}
