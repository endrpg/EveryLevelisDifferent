using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputController : InputController
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpForce;

    private float currentSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { currentSpeed = runSpeed; }
        else { currentSpeed = walkSpeed; }

        var horizontal = Input.GetAxis("Horizontal");
        
        Player.MoveHorizontal(horizontal, currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && Player.isGrounded) { Player.Jump(new Vector2(0, jumpForce)); }

        if (Input.GetMouseButtonDown(0)) { Player.attacking = true; }

        SetAnimations(horizontal);
    }

    void SetAnimations(float horizontal)
    {
        if (horizontal > 0)
        { Player.Flip(1); }
        if (horizontal < 0)
        { Player.Flip(-1); }

        if (currentSpeed == runSpeed && horizontal != 0)
        {
            Player.sprinting = true;
        }
        else { Player.sprinting = false; }
        if(currentSpeed == walkSpeed && horizontal != 0)
        {
            Player.walking = true;
        }
        else { Player.walking = false;  }
    }
}
