using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInfiniteRunnerInputController : InputController
{
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpForce;

    void Update()
    {
        Player.MoveHorizontal(1, runSpeed);
        Player.sprinting = true;

        if (Input.GetKeyDown(KeyCode.Space)) { Player.Jump(new Vector2(0, jumpForce)); }
    }
}
