using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool canAttack = false;

    //movement speeds
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 2f;
    [SerializeField]
    private float jumpForce = 10f;

    //animation
    private AnimatorOverrideController animOController = null;
    [SerializeField]
    private AnimationClip idleClip = null;
    [SerializeField]
    private AnimationClip walkClip = null;
    [SerializeField]
    private AnimationClip runClip = null;
    [SerializeField]
    private AnimationClip fallingClip = null;
    [SerializeField]
    private AnimationClip attackClip = null;

    //input 
    [HideInInspector]
    public float horizontal = 0f;
    [HideInInspector]
    public bool sprinting = false;
    [HideInInspector]
    public float vertical = 0f;
    [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public bool walking = false;

    //components
    private Rigidbody2D rb = null;
    private CircleCollider2D collider = null;
    private Animator animController = null;
    private int currentDirection = 1; //1 facing right -1 facing left

    private float currentSpeed = 0;
    private bool isGrounded = false;

    //do on awake so that it gets called before the attack controller
    void Awake()
    {
        GetAllComponents();

        SetUpAnimator();

        if (canAttack)
        {
            SetUpAttacking();
        }

        animController.runtimeAnimatorController = animOController;
    }

    private void GetAllComponents()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        animController = GetComponentInChildren<Animator>();
    }

    private void SetUpAnimator()
    {
        animOController = new AnimatorOverrideController(animController.runtimeAnimatorController);

        SetAnimationClip("Idle", idleClip);
        SetAnimationClip("Run", runClip);
        SetAnimationClip("Walk", walkClip);
        SetAnimationClip("Falling", fallingClip);
    }

    private void SetUpAttacking()
    {
        SetAnimationClip("Attack", attackClip);
    }

    public void SetAnimationClip(string targetName, AnimationClip clip)
    {
        animOController[targetName] = clip;
    }

    void Update()
    {
        CheckIfGrounded();
        Move();
        HandleAnimations();
        //attacking = false;
    }

    private void Move()
    {
        if (sprinting) { currentSpeed = runSpeed; }
        else { currentSpeed = walkSpeed; }
        var xSpeed = horizontal * currentSpeed;

        rb.velocity = new Vector2(xSpeed, rb.velocity.y);

        //handles flipping the player
        if (horizontal > 0)
        {
            currentDirection = 1;
        }
        else if (horizontal < 0)
        {
            currentDirection = -1;
        }
        transform.localScale = new Vector3(currentDirection, 1, 1);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void HandleAnimations()
    {
        if (horizontal > 0 || horizontal < 0) { walking = true; }
        else { walking = false; }

        animController.SetBool("walking", walking);
        animController.SetBool("sprinting", sprinting);

        if (attacking)
        {
            animController.SetBool("attacking", true);
            StartCoroutine(ResetAttack());
        }

        animController.SetBool("grounded", isGrounded);
    }

    private void CheckIfGrounded()
    {
        var offset = new Vector2(transform.position.x + collider.radius, transform.position.y) + collider.offset;
        var start = new Vector2(offset.x - collider.radius, (offset.y - collider.radius));
        //var startR = new Vector2(start.x + collider.radius, start.y);
        //var startL = new Vector2(start.x - collider.radius, start.y);

        //var distance = -0.1f;

        //var hitR = Physics2D.Raycast(startR, Vector2.down, distance, 8);
        //var hitL = Physics2D.Raycast(startL, Vector2.down, distance, 8);

        isGrounded = Physics2D.OverlapCircle(start, collider.radius, 512);

        ////var col = Physics2D.OverlapCircle(collider.offset, collider.radius);

        //Debug.DrawRay(startR, new Vector2(0, distance), Color.green);
        //Debug.DrawRay(startL, new Vector2(0, distance), Color.green);

        //if (hitL.collider != null || hitR.collider != null) { isGrounded = true; }
        //else { isGrounded = false; }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackClip.length);
        animController.SetBool("attacking", false);
        attacking = false;
    }
}
