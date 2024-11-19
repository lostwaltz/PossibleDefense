using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SlimeTowerData", menuName = "SlimeTower/SlimeTowerData")]
public class SlimeTowerDataSO : ScriptableObject
{
   [SerializeField] private SlimeTowerInfo slimeTowerInfo;
    
    public SlimeTowerInfo SlimeTowerInfo
    {
        get { return slimeTowerInfo; }
    }
    
    public SlimeTowerStats SlimeTowerStats;
}