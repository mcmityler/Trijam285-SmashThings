using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ItemBreakScript : MonoBehaviour
{
   private ItemClass _thisItem = null;
   private int _itemHealth = 1000;
   private GameObject _explosionObj;
   public void CreateItem(ItemClass m_item, GameObject m_explosion)
   {
      _thisItem = m_item;
      _itemHealth = _thisItem.hitsToBreak;
      _explosionObj = m_explosion;
   }
   private void OnMouseDown()
   {
      _itemHealth--;
      GameObject.FindGameObjectWithTag("Manager").GetComponent<ItemSpawnSystem>().ItemClickParticles(_thisItem);


      if (_itemHealth <= 0)
      {
         this.gameObject.SetActive(false); //set false before checking if there are any left, (fixes bug where it would set a spawned item inactive and then break the game loop)

         GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>().BrokeItem(_thisItem.itemValue); //broke item so increase score..
         
         if (_thisItem.itemName == "Bomb") //if it is a bomb do an explosion
         {
            GameObject m_explosion = Instantiate(_explosionObj, transform.position, Quaternion.identity); //spawn explosion
            Destroy(m_explosion, 2f); //destroy explosion
            CameraShaker.Instance.ShakeOnce(6f, 6f, 0.1f, 0.5f); //shake the screen when you hit a bomb
         }
         else
         {
            CameraShaker.Instance.ShakeOnce(2f, 2f, 0.1f, 0.5f); 
            GameObject.FindGameObjectWithTag("Manager").GetComponent<ItemSpawnSystem>().BrakeableItemKilled(); //not a bomb

         }

         _itemHealth = _thisItem.hitsToBreak; //reset health 
      }
      else
      {
         if (this.gameObject.GetComponent<Animator>() != null)
         {
            this.gameObject.GetComponent<Animator>().SetTrigger("BreakClick");
         }
      }
      
   }


   public string GetItemName()
   {
      return _thisItem.itemName;
   }
}
