using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
   public IntData cash;
   public List<Purchasable> items;
   public UnityEvent canPurchase;
   public UnityEvent purchased;
   
   public void MakePurchase(Purchasable item)
   {
      if (cash.value < item.value) return;
      cash.value -= item.value;
      item.Purchased = true;
      canPurchase.Invoke();
   }

   public void AddItem(Purchasable item)
   {
      items.Add(item);
   }
   
   public void RemoveItem(Purchasable item)
   {
      foreach (var currentItem in items.Where(currentItem => currentItem == item))
      {
         items.Remove(item);
      }
   }

   public void LockCheck()
   {
      foreach (var item in items)
      {
         item.Unlocked = cash.value >= item.value || item.Purchased;
      }
   }

   public void PurchasedCheck()
   {
      foreach (var item in items.Where(item => item.Purchased))
      {
         purchased.Invoke();
      }
   }
}
