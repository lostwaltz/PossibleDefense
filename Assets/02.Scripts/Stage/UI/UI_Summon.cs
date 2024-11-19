using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class UI_StartBtn : MonoBehaviour
{
    public GameObject Slime; 

    public void OnClickSummon()
    {
        List<TowerTile> curTowerTiles =  StageManager.Instance.Stage.TowerTiles;

        for (int i = 0; i < curTowerTiles.Count;i++)
        {
            if (!curTowerTiles[i].IsTower)
            {
                curTowerTiles[i].IsTower = true;
                Instantiate(Slime).transform.position = curTowerTiles[i].transform.position + (Vector3.up * 2);
                break;
            }
           
        }
      
    }

}
