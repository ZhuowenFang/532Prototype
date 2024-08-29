using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("地面检测")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("缩放参数")]
    public float growScale = 1.5f;
    public float shrinkScale = 0.5f;
    private Vector3 originalScale;

    [Header("摄像机设置")]
    public CinemachineVirtualCamera cinemachineCamera;
    public float growOrthoSize = 7f;
    public float shrinkOrthoSize = 3f;
    private float originalOrthoSize;

    [Header("能力状态")]
    public bool canGrow = false;
    public bool canShrink = false;
    public bool canReturn = false;
    public bool isLarge = false;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // 记录原始大小

        // 获取摄像机的初始正交尺寸
        if (cinemachineCamera != null)
        {
            originalOrthoSize = cinemachineCamera.m_Lens.OrthographicSize;
        }
    }

    void Update()
    {
        Move();
        Jump();
        HandleSizeChange();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 角色翻转
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleSizeChange()
    {
        if (canGrow && Input.GetKeyDown(KeyCode.E))
        {
            AdjustSizeAndPosition(growScale, growOrthoSize);
            isLarge = true;
            canReturn = true;

        }

        if (canShrink && Input.GetKeyDown(KeyCode.Q))
        {
            AdjustSizeAndPosition(shrinkScale, shrinkOrthoSize);
            canReturn = true;
            isLarge = false;
            jumpForce = 5f;
        }

        // 只有当角色的当前缩放比例不是原始比例时，才执行恢复原始大小的操作
        if ((canGrow || canShrink) && Input.GetKeyDown(KeyCode.R) && canReturn)
        {
            AdjustSizeAndPosition(1f, originalOrthoSize);
            canReturn = false;
            isLarge = false;
            jumpForce = 7f;
        }
    }

    void AdjustSizeAndPosition(float scaleMultiplier, float newOrthoSize)
    {
        Vector3 previousScale = transform.localScale;
        transform.localScale = originalScale * scaleMultiplier;

        Vector3 newPosition = transform.position;
        Collider2D overlapCollider = Physics2D.OverlapBox(transform.position, GetComponent<Collider2D>().bounds.size, 0f, groundLayer);

        if (overlapCollider != null)
        {
            // 如果新大小会导致重叠，调整角色位置
            float overlapHeight = GetComponent<Collider2D>().bounds.extents.y * 2;
            newPosition.y += overlapHeight;
        }

        transform.position = newPosition;
        AdjustCameraOrthoSize(newOrthoSize);
    }

    void AdjustCameraOrthoSize(float newOrthoSize)
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.m_Lens.OrthographicSize = newOrthoSize;
        }
    }
}
