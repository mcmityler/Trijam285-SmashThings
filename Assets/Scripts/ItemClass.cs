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
    public int itemValue = 1; // how much this item is worth when destroying
    public ParticleSystem clickParticles; //particles to spawn when you click on an object
    public Gradient clickParticleColour; //colour of the click particles
    public ParticleSystem breakApartParticles; //particles that spawn when the object is completely broke
    
}