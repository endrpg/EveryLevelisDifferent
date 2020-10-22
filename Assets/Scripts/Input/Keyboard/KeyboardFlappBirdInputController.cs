using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardFlappBirdInputController : InputController
{
    [SerializeField]
    private float flySpeed = 4f;
    [SerializeField]
    private float jumpForce = 10f;

    void Update()
    {
        Player.MoveHorizontal(1, flySpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.Jump(new Vector2(0, jumpForce));
        }    
    }
}
