using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;        // �̵� �ӵ�
    public float jumpForce = 0.005f;       // ���� ��
    public int maxJumps = 2;            // �ִ� ���� Ƚ�� (���� ���� ����)

    private Rigidbody2D rb;
    Floorcheck floorcheck;
    private bool isGrounded = false;
    private int jumpCount = 0;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public GameObject buildBlock;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        floorcheck = GetComponentInChildren<Floorcheck>();
    }

    void Update()
    {
        Move();
        //CheckGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !floorcheck.isFloorTouch)
        {
            GroundBuild();
        }
    }

    void GroundBuild()
    {
        Instantiate(buildBlock, floorcheck.transform);
    }
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ĳ���� ���� ��ȯ
        if (moveInput > 0 && transform.localScale.x < 0 || moveInput < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
