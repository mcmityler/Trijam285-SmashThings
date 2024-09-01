using System.Collections.Generic;
using UnityEngine;

//Item class for Item Spawn system

[System.Serializable]
public class ItemClass
{
    public string itemName; //name of the item 
    public GameObject itemPrefab; //prefab of the item to spawn
    public int hitsToBreak = 1; //how many hits this item has to break
    public List<Sprite> itemSprites; //sprites to use to update item if it takes more than 1 hit and want to show damage somehow.
}