using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickPlayerInputController : InputController
{
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private Button jumpButton;
    [SerializeField]
    private Button attackButton;

    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpForce;

    private float currentSpeed;

    private void Start()
    {
        attackButton.onClick.AddListener(AttackButtonPressed);
        jumpButton.onClick.AddListener(JumpButtonPressed);
    }

    void Update()
    {
        currentSpeed = runSpeed;

        var horizontal = joystick.Horizontal;/*Input.GetAxis("Horizontal");*/

        Player.MoveHorizontal(horizontal, currentSpeed);

        SetAnimations(horizontal);
    }

    private void AttackButtonPressed()
    {
        Player.attacking = true;
    }

    private void JumpButtonPressed()
    {
        if (Player.isGrounded)
        {
            Player.Jump(new Vector2(0, jumpForce));
        }
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
        if (currentSpeed == walkSpeed && horizontal != 0)
        {
            Player.walking = true;
        }
        else { Player.walking = false; }
    }
}
