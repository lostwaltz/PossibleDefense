
    using System;
    using UnityEngine;

    public class TowerGizmoVisualizer: MonoBehaviour
    {
        
        private BaseSlimeTower _slimeTower;
        private SlimeTowerDataSO _dataSo;

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
            Gizmos.DrawWireSphere(transform.position,  _slimeTower.StatHandler.AttackRange);
        }
    }
