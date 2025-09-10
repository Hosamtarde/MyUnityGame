using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TouchingDirections))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Knight : MonoBehaviour
{
    public float walkSpeed = 10f;
    public DetectionZone attackZoon;
    public float walkStopRate = 0.05f;
    private Vector2 walkDirectionVector = Vector2.left;
    public DetectionZone cliffDetectionZone;

    TouchingDirections touchingDirections;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Damageable damageable;

    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection = WalkableDirection.Left;

    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection == value) return;

            _walkDirection = value;
            walkDirectionVector = (_walkDirection == WalkableDirection.Right) ? Vector2.right : Vector2.left;

            
            if (_walkDirection == WalkableDirection.Left)
                spriteRenderer.flipX = false; 
            else
                spriteRenderer.flipX = true;  
        }
    }
    public bool _hasTarget = false;

    public bool HasTarget { 
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget , value);
        } 
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
        

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
             animator.SetFloat(AnimationStrings.attackCooldown , Mathf.Max(value , 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        walkDirection = WalkableDirection.Left;
    }

    void Update()
    {
        HasTarget = attackZoon.detectedColliders.Count > 0;
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        

    }


    private void FixedUpdate()
    {
        if (touchingDirections.isGrounded && touchingDirections.IsOnWall )
        {
            FlipDirection();
        }
       


        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.isGrounded)
            {
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        walkDirection = (walkDirection == WalkableDirection.Right) ? WalkableDirection.Left : WalkableDirection.Right;
    }

    public void OnHit(int damage , Vector2 knockback)
    {
        
        rb.velocity = new Vector2(knockback.x, rb.velocity.y * knockback.y);
    }

    public void OnCliffDetected()
    {
        if(touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }

    //public void OnNoGroundDetected()
   // {

   // }


}
