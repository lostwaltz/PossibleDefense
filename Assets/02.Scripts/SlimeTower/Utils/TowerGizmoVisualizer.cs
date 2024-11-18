
    using System;
    using UnityEngine;

    public class TowerGizmoVisualizer: MonoBehaviour
    {
        
        private BaseSlimeTower _slimeTower;
        private SlimeTowerStatSo data;

         private void Awake()
         {
             _slimeTower = GetComponent<BaseSlimeTower>();
         }
         
         void OnDrawGizmos()
         {
             if(_slimeTower ==null)
                return;

             DrawAttackRange();
         }

        private void DrawAttackRange()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,  _slimeTower.slimeTowerData.SlimeTowerStats.AttackRange);
        }
    }
