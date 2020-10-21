using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputController : InputController
{
    void Update()
    {
        Player.horizontal = Input.GetAxis("Horizontal");
        Player.vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)) { Player.sprinting = true; }
        else { Player.sprinting = false; }

        if (Input.GetKeyDown(KeyCode.Space)) { Player.Jump(); }

        if (Input.GetMouseButtonDown(0) && Player.canAttack) { Player.attacking = true; }
    }
}
