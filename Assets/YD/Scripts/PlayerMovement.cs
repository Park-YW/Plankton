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
    private Vector3 savePoint;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask[] groundLayer;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite1;
    [SerializeField] private Sprite idleSprite2;
    [SerializeField] private Sprite jumpUpSprite;
    [SerializeField] private Sprite jumpDownSprite;

    private SpriteRenderer spriteRenderer;
    private bool isIdleAnimating = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

<<<<<<< HEAD
=======

        
        if(savePoint != null) {
            Debug.Log(savePoint);

        }


>>>>>>> a5916975305cbadf2462a4c2c6c04acd1cb16fb0
        if (Input.GetButtonDown("Jump") && IsGrounded() && !GameManager.Instance.isPlayerLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && !GameManager.Instance.isPlayerLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        UpdateSprite();
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

<<<<<<< HEAD
    private void UpdateSprite()
    {
        if (!IsGrounded())
        {
            if (rb.velocity.y > 0)
            {
                spriteRenderer.sprite = jumpUpSprite; // ÏÉÅÏäπ Ïãú Ïä§ÌîÑÎùºÏù¥Ìä∏
                isIdleAnimating = false;
            }
            else if (rb.velocity.y < 0)
            {
                spriteRenderer.sprite = jumpDownSprite; // ÌïòÍ∞ï Ïãú Ïä§ÌîÑÎùºÏù¥Ìä∏
                isIdleAnimating = false;
            }
        }
        else
        {
            if (!isIdleAnimating)
            {
                StartCoroutine(IdleAnimation());
            }
        }
    }

    private IEnumerator IdleAnimation()
    {
        isIdleAnimating = true;
        while (IsGrounded())
        {
            spriteRenderer.sprite = idleSprite1;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = idleSprite2;
            yield return new WaitForSeconds(0.5f);
        }
        isIdleAnimating = false;
    }

    private void CheckIsGrounded()
    {
        //Debug.Log("IsGrounded: " + IsGrounded());
    }

=======
>>>>>>> a5916975305cbadf2462a4c2c6c04acd1cb16fb0
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            RespawnAtSavePoint();
        }
    }
    public void SetSavePoint(Vector3 position)
    {
        savePoint = position; // ªı∑ŒøÓ ºº¿Ã∫Í ∆˜¿Œ∆Æ∏¶ º≥¡§
        Debug.Log("ªı∑ŒøÓ ºº¿Ã∫Í ∆˜¿Œ∆Æ º≥¡§: " + savePoint);
    }

    private void RespawnAtSavePoint()
    {
        if (savePoint != null)
        {
            transform.position = savePoint; // «√∑π¿ÃæÓ∏¶ ºº¿Ã∫Í ∆˜¿Œ∆Æ∑Œ ¿Ãµø
            Debug.Log("«√∑π¿ÃæÓ∞° ºº¿Ã∫Í ∆˜¿Œ∆Æ∑Œ ¿Ãµø«ﬂΩ¿¥œ¥Ÿ: " + savePoint);
        }
        else
        {
            Debug.LogWarning("ºº¿Ã∫Í ∆˜¿Œ∆Æ∞° º≥¡§µ«¡ˆ æ æ“Ω¿¥œ¥Ÿ!");
        }
    }
}
