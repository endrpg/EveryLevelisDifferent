using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTopDownInputController : InputController
{
    [SerializeField]
    private float moveSpeed;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Player.MoveHorizontal(horizontal, moveSpeed);
        Player.MoveVertical(vertical, moveSpeed);
    }
}
