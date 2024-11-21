using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    [SerializeField]private List<UIBase> UIBases ;
    private Dictionary<string, UIBase> stageUIContainer = new Dictionary<string, UIBase>();

    private void Awake()
    {
        foreach (UIBase ui in UIBases)
        {
            stageUIContainer.Add(ui.GetType().Name, ui);
        }

    }

    public T GetUI<T>(string name) where T : UIBase
    {
        if (stageUIContainer.ContainsKey(name))
        {
            T uIWindow = stageUIContainer[name] as T; 

            return uIWindow;
        }

        return null;
    }

    private void Update()
    {
        
    }
}
