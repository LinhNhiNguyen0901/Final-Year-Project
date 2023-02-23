using System.Collections;
using UnityEngine;

public class mainCharMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Vector2 gravity;


    public Collider2D standingCollider, crouchingCollider;
    public Transform overheadCheckCollider;
    public Transform wallCheckCollider;
    public LayerMask wallLayer;
    public Transform groundCheckCollider;
    public LayerMask groundLayer;
    public Vector2 wallJumpForce;
    public static Vector2 currCheckpoint = new Vector2();
    bool coyoteJump;
    public bool isSliding;
    bool extraJump = false;
    bool crouchPressed;
    bool facingRight = true;
    bool isDead = false;

    const float overheadCheckRadius = 0.2f;
    const float groundCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;

    float crouchSpeed = 0.5f;
    float horizontalValue;

    [SerializeField] private float speed = 12;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpPower = 40;
    [SerializeField] bool isGrounded = false;

    public int totalJumps = 2;
    int availableJumps = 2;

    // Start is called before the first frame update

    private void Start()
    {
        //Call in start because if call in Awake,
        //the dependencies in UI_Inventory and ItemAssets which both are also called on Awake
        //can cause mistiming and then break 


    }

    void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject.FindGameObjectWithTag("Player").transform.position = currCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            Jump();


        if (Input.GetButtonDown("Crouch"))
            crouchPressed = true;
        else if (Input.GetButtonUp("Crouch"))
            crouchPressed = false;

        if (rb.velocity.y < 0)
        {
            rb.velocity -= gravity * Time.fixedDeltaTime;
        }
        animator.SetFloat("yVelocity", rb.velocity.y);


    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, crouchPressed);
    }

    public void Move(float dir, bool crouchFlag)
    {

        #region move
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;

        if (crouchFlag)
            xVal *= crouchSpeed;

        //Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's velocity
        rb.velocity = targetVelocity;


        //If looking right and clicked left (flip to the left)
        if (facingRight && dir > 0)
        {
            transform.localScale = new Vector3((float)-1.3, (float)1.3, 1);
            facingRight = false;
        }
        //If looking left and clicked right (flip to rhe right)
        else if (!facingRight && dir < 0)
        {
            transform.localScale = new Vector3((float)1.3, (float)1.3, 1);
            facingRight = true;
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion

        #region crouch
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                crouchFlag = true;

            }
        }

        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;
        #endregion


        animator.SetBool("Jump", !isGrounded);
    }

    bool CanMoveOrInteract()
    {
        bool can = true;

        if (isDead)
            can = false;

        return can;
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other
        //2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);

        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                extraJump = false;
            }
            
        }
        else
        {

            if (wasGrounded)
                StartCoroutine(CoyoteJumpDelay());
        }

        //As long as we are grounded the "Jump" bool
        //in the animator is disabled
        animator.SetBool("Jump", !isGrounded);
    }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.1f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            extraJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump)
            {
                extraJump = true;
                availableJumps--;
                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
            if (extraJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

        }

    }

    public void Die()
    {
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
    }

    public void ResetPlayer()
    {
        isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lava")
            GetComponent<LifeCount>().LoseLife();
    } 
}