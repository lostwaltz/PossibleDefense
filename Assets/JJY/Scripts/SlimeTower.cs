using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTower : MonoBehaviour
{
   private void Start()
   {
      StartCoroutine(Attack());
   }
   
   
   
   
   private IEnumerator Attack()
   {
      while (true)
      {
         yield return new WaitForSeconds(1f);


         if (StageManager.Instance.MobSpawner.monsters.Count != 0)
         {
            foreach (var mob in StageManager.Instance.MobSpawner.monsters)
            {
               Debug.Log("공격!!");
               mob.TakeDamage();
               break;
            }
         }
         
      }
   }
}
