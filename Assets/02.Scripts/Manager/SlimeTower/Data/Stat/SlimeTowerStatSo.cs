using UnityEngine;

[CreateAssetMenu(fileName = "SlimeTowerStat", menuName = "SlimeTower/SlimeTowerStat")]
public class SlimeTowerStatSo : ScriptableObject
{
    [SerializeField] private SlimeTowerData slimeTowerData;
    
    public SlimeTowerData SlimeTowerData
    {
        get { return slimeTowerData; }
    }
    
    public SlimeTowerStats SlimeTowerStats;
}