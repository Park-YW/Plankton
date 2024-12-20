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
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask[] groundLayer;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite1;
    [SerializeField] private Sprite idleSprite2;
    [SerializeField] private Sprite jumpUpSprite;
    [SerializeField] private Sprite jumpDownSprite;
    [SerializeField] private Sprite itemAdded;
    public float changeDuration = 1f;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;

    private float idleAnimationTimer = 0f; // Idle 애니메이션 타이머
    private float idleAnimationInterval = 0.2f; // Idle 스프라이트 전환 간격
    private bool isSpriteTemporarilyChanged = false; // 스프라이트 임시 변경 플래그

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // ResourceManager의 이벤트 구독
        ResourceManager.Instance.OnItemAdded += ChangeSpriteTemporarily;

        // 원래 스프라이트 저장
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnItemAdded -= ChangeSpriteTemporarily;
        }
    }

    void Update()
    {
        if (!isSpriteTemporarilyChanged)
        {
            UpdateMovement();
            UpdateSprite();
        }
    }

    private void UpdateMovement()
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

    private void UpdateSprite()
    {
        if (!IsGrounded())
        {
            if (rb.velocity.y > 0)
            {
                spriteRenderer.sprite = jumpUpSprite; // 상승 시 스프라이트
            }
            else if (rb.velocity.y < 0)
            {
                spriteRenderer.sprite = jumpDownSprite; // 하강 시 스프라이트
            }
        }
        else if (horizontal == 0f) // 가만히 있을 때만 Idle 애니메이션
        {
            idleAnimationTimer += Time.deltaTime;
            if (idleAnimationTimer >= idleAnimationInterval)
            {
                idleAnimationTimer = 0f;
                spriteRenderer.sprite = spriteRenderer.sprite == idleSprite1 ? idleSprite2 : idleSprite1;
            }
        }
    }

    private void ChangeSpriteTemporarily()
    {
        if (!isSpriteTemporarilyChanged)
        {
            StartCoroutine(ChangeSpriteCoroutine());
        }
    }

    private IEnumerator ChangeSpriteCoroutine()
    {
        isSpriteTemporarilyChanged = true;
        rb.velocity = Vector2.zero; // 이동 멈춤
        Sprite previousSprite = spriteRenderer.sprite; // 기존 스프라이트 저장

        spriteRenderer.sprite = itemAdded; // 임시 스프라이트 설정

        yield return new WaitForSeconds(changeDuration);

        spriteRenderer.sprite = previousSprite; // 기존 스프라이트 복구
        isSpriteTemporarilyChanged = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer[0]) ||
               Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer[1]);
    }
}
