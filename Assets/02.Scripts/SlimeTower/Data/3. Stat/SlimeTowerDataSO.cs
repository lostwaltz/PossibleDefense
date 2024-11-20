using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SlimeTowerData", menuName = "SlimeTower/Data/SlimeTowerData")]
public class SlimeTowerDataSO : ScriptableObject
{ 
    public string towerName; 
   [SerializeField] private SlimeTowerGradeInfo slimeTowerGradeInfo;
   [SerializeField] private SlimeTowerStatUpgradeData slimeTowerUpgradeDataData;
   public SlimeTowerStats SlimeTowerStats;

   
   
    public SlimeTowerGradeInfo SlimeTowerGradeInfo
    {
        get { return slimeTowerGradeInfo; }
    }
    
    public SlimeTowerStatUpgradeData SlimeTowerUpgradeDataData
    {
        get { return slimeTowerUpgradeDataData; }
    }
    
    
}