using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBreakScript : MonoBehaviour
{
   private ItemClass _thisItem = null;
   private int _itemHealth = 1000;
   [SerializeField] private bool _isBackground = false;
   public void CreateItem(ItemClass m_item)
   {
      _thisItem = m_item;
      _itemHealth = _thisItem.hitsToBreak;
   }
   private void OnMouseDown()
   {
      if (_isBackground)
      {
         GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>().ClickedCounter();
         return;
      }
      _itemHealth--;
      if (_itemHealth <= 0)
      {
         GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>().BrokeItem(_thisItem.itemValue);
         Destroy(this.gameObject);
      }
      else
      {
         //change objects picture if its not broken
         GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>().ClickedCounter();
         if (this.gameObject.GetComponent<Animator>() != null)
         {
            this.gameObject.GetComponent<Animator>().SetTrigger("BreakClick");
         }
      }
      
   }
}
