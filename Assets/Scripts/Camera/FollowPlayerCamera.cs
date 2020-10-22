using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField]
    private bool backgroundFollowPlayer = true;
    [SerializeField]
    private GameObject backGround = null;

    private GameObject Player = null;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player == null) { Debug.LogError("Player missing from scene."); }
    }

    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        if (backgroundFollowPlayer)
        {
            backGround.transform.position = new Vector3(Player.transform.position.x, backGround.transform.position.y, backGround.transform.position.z);
        }
    }
}
