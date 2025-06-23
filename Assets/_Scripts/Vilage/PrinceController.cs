using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PrinceController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;  
    private Animator animator;
    [SerializeField] private float speed;

    public bool isTalk;

    public enum State
    {
        Idle, Run
    };
    public State currentState;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }
        if (isTalk)
        {
            rb.velocity = Vector3.zero;
        }
        else
            Move();

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Run:
                Run();
                break;
            default:
                break;
        }
    }

    public void Run()
    {
        if (rb.velocity == Vector2.zero)
        {
            currentState = State.Idle;
        }

        animator.Play("Prince Run");
        float direction = rb.velocity.x;

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void Idle()
    {
        animator.Play("Prince Idle");
        if (rb.velocity != Vector2.zero)
        {
            currentState = State.Run;
        }
    }
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(horizontalInput, 0);
        rb.velocity = dir * speed;
    }
}
