using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;
    public Bullet bulletPrefab;
    public string axis;
    public string vert;
    private float horizontal;
    private float speed = 4f; // 8f
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    AudioManager audioManager;

    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float groundCheckRadius;
    public bool isGrounded;
    public Vector3 respawnPoint;
    public GameObject fallDetector;

    bool shouldActivateColliderStand;
    bool shouldActivateColliderSquat;

    //testing
    public BoxCollider2D colliderStand;
    public BoxCollider2D colliderSquat;
    [SerializeField] private KeyCode controlKeyCode;


    // Testing
    private Transform objectTransform;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
        objectTransform = GetComponent<Transform>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        // testing
        shouldActivateColliderStand = !Input.GetKey(controlKeyCode);
        shouldActivateColliderSquat = Input.GetKey(controlKeyCode);

        colliderStand.enabled = shouldActivateColliderStand;
        colliderSquat.enabled = shouldActivateColliderSquat;
        //

        if (shouldActivateColliderSquat)
        {
            Vector3 newScale = objectTransform.localScale;
            newScale.y = 0.13f;
            objectTransform.localScale = newScale;
        }
        else if (shouldActivateColliderStand)
        {
            Vector3 newScale = objectTransform.localScale;
            newScale.y = 0.16f;
            objectTransform.localScale = newScale;
        }

        horizontal = Input.GetAxisRaw(axis);

        if (Input.GetButtonDown(vert) && IsGrounded() && shouldActivateColliderStand)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp(vert) && rb.velocity.y > 0f && shouldActivateColliderStand)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (Input.GetKeyDown(KeyCode.Space) && this.gameObject.tag == "firstPlayer")
        {
            if (Shoot())
            {
                audioManager.PlaySFX(audioManager.shot1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && this.gameObject.tag == "secondPlayer")
        {
            if (Shoot())
            {
                audioManager.PlaySFX(audioManager.shot2);
            }
        }

        if (shouldActivateColliderSquat && this.gameObject.tag == "firstPlayer")
        {
            anim.SetTrigger("Squat");
        }
        else if (shouldActivateColliderSquat && this.gameObject.tag == "secondPlayer")
        {
            anim.SetTrigger("Squat");
        }
        // Debug.Log(Input.GetKey(KeyCode.LeftControl));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        // Animation
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", IsGrounded());
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (this.gameObject.tag == "firstPlayer")
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        else if (this.gameObject.tag == "secondPlayer")
        {
            if (isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }

    private bool Shoot()
    {
        if (playerController.CanFireBullet())
        {
            playerController.BulletFired();

            Bullet bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x + CheckFacing(), transform.position.y + checkSquat()), transform.rotation);
            bullet.Shoot(transform.right, isFacingRight, this.gameObject.tag);

            anim.SetTrigger("Shot");
            return true;
        }
        else
        {
            // Player has exceeded the bullet limit, so they cannot shoot.
            // You can add any feedback or sound effect to indicate this.
            audioManager.PlaySFX(audioManager.dryShot);
            Debug.Log("Cannot fire more bullets.");
            return false;
        }
    }

    private float CheckFacing()
    {
        if (this.gameObject.tag != "firstPlayer")
        {
            return isFacingRight ? -0.35f : 0.35f;
        }
        return isFacingRight ? 0.35f : -0.35f;
    }

    private float checkSquat()
    {
        return shouldActivateColliderStand ? 0.20f : 0.10f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bottom")
        {
            if (gameObject.tag == "firstPlayer")
            {
                FindObjectOfType<GameManager>().HurtP1();
                transform.position = respawnPoint;
            }
            else
            {
                FindObjectOfType<GameManager>().HurtP2();
                transform.position = respawnPoint;
            }
            audioManager.PlaySFX(audioManager.scream);
        }

    }

}