using System;
using UnityEngine;

public class Obstacles_Objects : MonoBehaviour
{
    public GameObject[] _gameObject;
    private void OnEnable()
    {
        Instantiate(_gameObject[0], transform.position, Quaternion.identity);
    }
}