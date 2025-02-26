using System;
using UnityEngine;

public class Obstacles_Objects : MonoBehaviour
{
    public GameObject[] _gameObject;
    private void OnEnable()
    {
        Instantiate(_gameObject[1], transform.position, Quaternion.identity);
    }
}