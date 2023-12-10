using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealerSpawner : MonoBehaviour
{
    [SerializeField] private int _quantitySpawn;
    [SerializeField] private Healer _healer;

    [SerializeField] private List<Transform> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<Transform>().ToList();
        _spawnPoints.Remove(transform);
    }

    private void Start()
    {
        if(_quantitySpawn > _spawnPoints.Count) 
        {
            _quantitySpawn = _spawnPoints.Count;
        }

        while(_spawnPoints.Count > _quantitySpawn)
        {
            _spawnPoints.RemoveAt(Random.Range(0, _spawnPoints.Count));
        }

        foreach(Transform point in _spawnPoints)
        {
            Instantiate(_healer, point.position, Quaternion.identity);
        }
    }
}
