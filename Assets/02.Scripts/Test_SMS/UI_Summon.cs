using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartBtn : MonoBehaviour
{
    public GameObject Slime; 

    public void OnClickSummon()
    {
        Instantiate(Slime).transform.position += Vector3.up *3;
        //int index = UnityEngine.Random.Range(0,Board.curMap.PlayerTilePoint.Count);

        //Instantiate(Slime).transform.position = Board.curMap.PlayerTilePoint[index]; 
    }

}
