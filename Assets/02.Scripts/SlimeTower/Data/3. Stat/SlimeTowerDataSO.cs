using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SlimeTowerData", menuName = "SlimeTower/Data/SlimeTowerData")]
public class SlimeTowerDataSO : ScriptableObject
{
   [SerializeField] private SlimeTowerInfo slimeTowerInfo;
   [SerializeField] private SlimeTowerStatUpgradeData slimeTowerUpgradeDataData;
   public SlimeTowerStats SlimeTowerStats;

   
   
    public SlimeTowerInfo SlimeTowerInfo
    {
        get { return slimeTowerInfo; }
    }
    
    public SlimeTowerStatUpgradeData SlimeTowerUpgradeDataData
    {
        get { return slimeTowerUpgradeDataData; }
    }
    
    
}