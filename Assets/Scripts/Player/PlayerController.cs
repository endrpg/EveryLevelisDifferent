using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    public bool sprinting = false;
    [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public bool walking = false;
    [HideInInspector]
    public bool isGrounded = false;

    //components
    private Rigidbody2D rb = null;
    private CircleCollider2D collider = null;
    private Animator animController = null;

    private bool canAttack = false;

    //do on awake so that it gets called before the attack controller
    void Awake()
    {
        GetAllComponents();

        SetUpAnimator();

        if (attackClip != null)
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
        canAttack = true;
        SetAnimationClip("Attack", attackClip);
    }

    public void SetAnimationClip(string targetName, AnimationClip clip)
    {
        animOController[targetName] = clip;
    }

    void Update()
    {
        CheckIfGrounded();
        HandleAnimations();
        //attacking = false;
    }

    public void MoveHorizontal(float horizontal, float speed)
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void Flip(int direction) //-1 left //1 right
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    public void MoveVertical(float vertical, float speed)
    {
        rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
    }

    public void Jump(Vector2 jumpForce)
    {
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    private void HandleAnimations()
    {
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
