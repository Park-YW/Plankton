using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 2f;
    private float jumpingPower = 6f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.4f; // 감지 반경을 두 배로 설정
    [SerializeField] private LayerMask[] groundLayer; // 레이어 마스크 추가
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        InvokeRepeating("CheckIsGrounded", 0f, 0.1f);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        
        

        if (Input.GetButtonDown("Jump") && IsGrounded() && !GameManager.Instance.isPlayerLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && !GameManager.Instance.isPlayerLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isPlayerLadder)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, vertical * speed);
        }
        else
        {
            rb.gravityScale = 1.5f;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
        if (rb.velocity.x != 0f || rb.velocity.y != 0f)
        {
            GameManager.Instance.isPlayerMoving = true;
        }
        else
        {
            GameManager.Instance.isPlayerMoving = false;
        }
    }

    private bool IsGrounded()
    {
        // 지정된 레이어와의 충돌을 확인하여 땅에 닿아 있는지 확인
        return (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer[0]) || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer[1]));
    }


    
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void CheckIsGrounded()
    {
        //Debug.Log("IsGrounded: " + IsGrounded());
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
