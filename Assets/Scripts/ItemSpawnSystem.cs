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

    [SerializeField] private GameObject _explosionObj;
    [SerializeField] private List<GameObject> _breakableItemPool = new List<GameObject>();
    [SerializeField] private GameObject _clickParticles;


    private int _breakableItemsAlive = 0;
    private void Awake()
    {
        //make item pool at the start when launched
        CreateItemPool();
        SpawnRandomItemsFromPool();
    }

    private void CreateItemPool()
    {
        foreach (var m_item in _breakableItems)
        {
            for (int i = 0; i < 3; i++)
            {
                //create 3 of each item to add to pool
                GameObject m_newPoolItem = Instantiate(m_item.itemPrefab, Vector3.one, Quaternion.identity); //spawn item at (1,1,1)
                m_newPoolItem.transform.SetParent(_spawnArea.transform); //make item a child of the spawn area.
                m_newPoolItem.GetComponent<ItemBreakScript>().CreateItem(m_item, _explosionObj); //set items values in its script
                m_newPoolItem.SetActive(false); // make it so the items are all invisible
                _breakableItemPool.Add(m_newPoolItem); //add each item to a list of all breakable gameobjects to "spawn" and reuse
            }
        }
    }
    public void ItemClickParticles(ItemClass m_clickParticleInfo)
    {
        //var m_main = _clickParticles.main; 
        //m_main.startColor = m_clickParticleInfo.clickParticleColour;
        ParticleSystem.MainModule m_main = _clickParticles.GetComponent<ParticleSystem>().main; 
        var randomColors = new ParticleSystem.MinMaxGradient(m_clickParticleInfo.clickParticleColour);
        randomColors.mode = ParticleSystemGradientMode.RandomColor; //ensure it is trying to use a random colour not a gradient.. so that it spawns multiple colour
        m_main.startColor = randomColors;
        
        
        if (Camera.main != null)
            _clickParticles.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _clickParticles.GetComponent<ParticleSystem>().Play();
    }

    public void BrakeableItemKilled()
    {
        _breakableItemsAlive--;
        Debug.Log(_breakableItemsAlive);
        if (_breakableItemsAlive <= 0)
        {
            DeactivateAllBreakableItems();
            SpawnRandomItemsFromPool();
        }
    }

    void DeactivateAllBreakableItems()
    {
        foreach (var m_breakPoolItem in _breakableItemPool)
        {
            m_breakPoolItem.SetActive(false);
        }
    }
    
    public void SpawnRandomItemsFromPool()
    {
        _breakableItemsAlive = 0; //amount of items alive
        int _randomItemAmount = Random.Range(2, 6); //randomly spawn 2-6 items
        List<GameObject> m_tempItemPool = new List<GameObject>(_breakableItemPool); //temporary item pool so that you dont try spawning the same item twice (aka remove items after "spawning")
        
        
        for (int i = 0; i < _randomItemAmount; i++)
        {
            Vector3 m_randomSpawnPoint = new Vector3(
                Random.Range(_spawnArea.transform.position.x - _spawnArea.transform.localScale.x / 2,
                    _spawnArea.transform.position.x + _spawnArea.transform.localScale.x / 2),
                Random.Range(_spawnArea.transform.position.y - _spawnArea.transform.localScale.y / 2,
                    _spawnArea.transform.position.y + _spawnArea.transform.localScale.y / 2), 
                0); //random spawn position

            int m_randomItemNum = Random.Range(0, m_tempItemPool.Count); //random item from item list

            m_tempItemPool[m_randomItemNum].transform.position = m_randomSpawnPoint;
            m_tempItemPool[m_randomItemNum].SetActive(true);
            Debug.Log(m_tempItemPool[m_randomItemNum].name);
            if (m_tempItemPool[m_randomItemNum].GetComponent<ItemBreakScript>().GetItemName() != "Bomb")
            {
                _breakableItemsAlive++;
            }

            m_tempItemPool.RemoveAt(m_randomItemNum);
        }
        if (_breakableItemsAlive <= 0) //only bombs spawned ... redo
        {
            DeactivateAllBreakableItems();
            SpawnRandomItemsFromPool();
        }

    }
}
