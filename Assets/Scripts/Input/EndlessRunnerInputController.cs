using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunnerInputController : InputController
{
    void Update()
    {
        Player.horizontal = 1;
        Player.sprinting = true;

        if (Input.GetKeyDown(KeyCode.Space)) { Player.Jump(); }
    }
}
