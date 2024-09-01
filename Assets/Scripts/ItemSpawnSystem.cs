using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawnSystem : MonoBehaviour
{
    [SerializeField] private List<ItemClass> _breakableItems = new List<ItemClass>();

    [SerializeField] private GameObject _spawnArea;

    [SerializeField] private float _spawnTime = 10f;
    private float _ctr = 0f;

    private void FixedUpdate()
    {
        _ctr += Time.deltaTime;
        if (_spawnTime < _ctr)
        {
            _ctr = 0;
            SpawnRandomBreakableItem();
        }
    }

    public void SpawnRandomBreakableItem()
    {
        Vector3 m_randomSpawnPoint = new Vector3(Random.Range(_spawnArea.transform.position.x - _spawnArea.transform.localScale.x/2, _spawnArea.transform.position.x + _spawnArea.transform.localScale.x/2),
                                                Random.Range(_spawnArea.transform.position.y - _spawnArea.transform.localScale.y/2, _spawnArea.transform.position.y + _spawnArea.transform.localScale.y/2), 0); //random spawn position
        
        int m_randomItemNum = Random.Range(0, _breakableItems.Count); //random item from item list
        GameObject m_newItem = Instantiate(_breakableItems[m_randomItemNum].itemPrefab, m_randomSpawnPoint, Quaternion.identity); //spawn item in random position
        m_newItem.transform.SetParent(_spawnArea.transform); //make item a child of the spawn area.
    }
}
