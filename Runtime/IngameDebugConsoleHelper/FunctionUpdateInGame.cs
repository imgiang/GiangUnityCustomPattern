using System.Collections;
using System.Collections.Generic;
using D2H;
using GiangCustom.DesignPattern;
using IngameDebugConsole;
using UnityEngine;

public class FunctionUpdateInGame : MonoBehaviour
{
    [ConsoleMethod( "lv", "Creates a cube at specified position" )]
    public static void LoadLevel( int lv)
    {
        UIManager.Instance.ShowIngameMenu();
        GamePlay.Instance.LoadAndInstantiateLevel(lv);
        Debug.Log("Load level: " + lv);
    }
}
