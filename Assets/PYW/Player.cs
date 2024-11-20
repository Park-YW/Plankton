using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 3f;        // 이동 속도
    public float jumpForce = 0.005f;       // 점프 힘
    public int maxJumps = 2;            // 최대 점프 횟수 (이중 점프 구현)

    private Rigidbody2D rb;
    Floorcheck floorcheck;
    private bool isGrounded = false;
    private int jumpCount = 0;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public GameObject buildBlock;

    private float abilityTimer = 0;
    public int maxBlockstep = 5;
    private int Blockstep = 0;

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
        if (Input.GetKey(KeyCode.LeftShift) && !floorcheck.isFloorTouch)
        {
            GroundBuild();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Blockstep = 0;
        }
    }

    void GroundBuild()
    {
        if ( abilityTimer == 0 )
        {
            if(Blockstep < maxBlockstep)
            {
                Instantiate(buildBlock, floorcheck.transform.position, Quaternion.identity);
                Blockstep++;
                abilityTimer += Time.deltaTime;
            }
        }
        else
        {
            abilityTimer += Time.deltaTime;
        }
        if ( abilityTimer > 0.03f )
        {
            abilityTimer = 0;
        }
        
        
    }
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 캐릭터 방향 전환
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
