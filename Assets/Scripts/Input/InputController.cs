using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [HideInInspector]
    public PlayerController Player;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); 
        if(Player == null) { Debug.LogError("No player found in scene."); }
    }
}
