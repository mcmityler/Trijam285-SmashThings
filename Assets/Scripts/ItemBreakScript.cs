using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBreakScript : MonoBehaviour
{
   private ItemClass _thisItem = null;
   private int _itemHealth = 1000;
   public void CreateItem(ItemClass m_item)
   {
      _thisItem = m_item;
      _itemHealth = _thisItem.hitsToBreak;
   }
   private void OnMouseDown()
   {
      _itemHealth--;
      if (_itemHealth <= 0)
      {
         GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>().BrokeItem(_thisItem.itemValue);
         Destroy(this.gameObject);
      }
      else
      {
         if (this.gameObject.GetComponent<Animator>() != null)
         {
            this.gameObject.GetComponent<Animator>().SetTrigger("BreakClick");
         }
      }
      
   }
}
