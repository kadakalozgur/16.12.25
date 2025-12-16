using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : MonoBehaviour
{
    private CapsuleCollider2D m_CapsulleCollider;
    private Animator m_Anim;
    private Rigidbody2D m_rigidbody;

    public float MoveSpeed = 5f;
    public float DashSpeed = 25f;
    public float DashDuration = 0.3f;
    public static bool canMove = true;

    private float m_MoveX;
    private float m_MoveY;
    private bool isDead = false;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector3 dashDirection;
    private TrailRenderer tr;

    private void Start()
    {
        m_CapsulleCollider = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
        tr = GetComponentInChildren<TrailRenderer>();

        canMove = true;

        if (tr != null)
            tr.emitting = false;

        // Disable gravity for top-down game
        m_rigidbody.gravityScale = 0;
        // Clear rigidbody constraints
        m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {

        if (!canMove)
        {

            m_rigidbody.velocity = Vector2.zero;
            return;

        }

        // Dash timer control
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                m_rigidbody.velocity = Vector2.zero;

                if (tr != null)
                    tr.emitting = false;
            }
            else
            {
                // Dash movement
                transform.position += dashDirection * DashSpeed * Time.deltaTime;
                return; // Don't process other inputs during dash
            }
        }

        checkInput();

        // Speed limiter
        if (m_rigidbody.velocity.magnitude > 30)
        {
            m_rigidbody.velocity = m_rigidbody.velocity.normalized * 30f;
        }
    }

    public void checkInput()
    {
        // If dead, don't process any input
        if (isDead)
            return;

        // Sit control
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_Anim.Play("Sit");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_Anim.Play("Idle");
        }

        // Block other movements during Sit animation
        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sit"))
        {
            return;
        }

        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            m_rigidbody.velocity = Vector2.zero;
            return;
        }

        // Movement inputs
        m_MoveX = Input.GetAxis("Horizontal");
        m_MoveY = Input.GetAxis("Vertical");

        // Dash control (Space key)
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            // Dash in the last movement direction
            if (m_MoveX != 0 || m_MoveY != 0)
            {
                dashDirection = new Vector3(m_MoveX, m_MoveY, 0).normalized;
                isDashing = true;
                dashTimer = DashDuration;

                if (tr != null)
                    tr.emitting = true;

                return;
            }
        }

        bool isMoving = false;
        bool isAttacking = m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        // A/D keys - Horizontal movement
        if (Input.GetKey(KeyCode.D))
        {
            // Only change facing if not attacking
            if (!isAttacking)
                SetFacing(true); // Face right
            transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Only change facing if not attacking
            if (!isAttacking)
                SetFacing(false); // Face left
            transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
            isMoving = true;
        }

        // W/S keys - Vertical movement only (face direction doesn't change)
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * MoveSpeed * Time.deltaTime;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * MoveSpeed * Time.deltaTime;
            isMoving = true;
        }

        // Reset velocity to zero if not moving (instant stop)
        if (!isMoving)
        {
            m_rigidbody.velocity = Vector2.zero;
        }

        // Attack animation control
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_Anim.Play("Attack");
            }
            else
            {
                // Animation based on movement state
                if (isMoving)
                {
                    // Switch to Run if not already in Run animation
                    if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    {
                        m_Anim.Play("Run");
                    }
                }
                else
                {
                    // Switch to Idle if not already in Idle animation
                    if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        m_Anim.Play("Idle");
                    }
                }
            }
        }
    }

    private void SetFacing(bool right)
    {
        Transform model = transform.Find("model");
        if (model != null)
        {
            // Use scale for 2D characters
            Vector3 scale = model.localScale;

            // If model initially faces left, use this logic
            if (right)
            {
                scale.x = -Mathf.Abs(scale.x); // Go right (reverse scale)
            }
            else
            {
                scale.x = Mathf.Abs(scale.x); // Go left (normal scale)
            }

            model.localScale = scale;
        }
    }
}