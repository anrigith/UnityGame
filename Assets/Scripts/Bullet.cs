using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 300f;
    public float maxLifetime = 10f;
    private FallingChest chest;

    AudioManager audioManager;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        chest = FindObjectOfType<FallingChest>();
    }

    public void Shoot(Vector2 direction, bool isFacingRight, string tagName)
    {
        if ( tagName == "firstPlayer")
        {
            if (isFacingRight)
            {
                rigidbody.AddForce(direction * speed);
            }
            else 
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                
                rigidbody.AddForce(-direction * speed);
            }
        }
        else
        {
            if (!isFacingRight)
            {
                rigidbody.AddForce(direction * speed);
            }
            else 
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                
                rigidbody.AddForce(-direction * speed);
            }
        }
        // The bullet only needs a force to be added once since they have no
        // drag to make them stop moving

        // Destroy the bullet after it reaches it max lifetime
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "firstPlayer")
        {
            FindObjectOfType<GameManager>().HurtP1();
            audioManager.PlaySFX(audioManager.knockDown);
        }
        if (collision.gameObject.tag == "secondPlayer")
        {
            FindObjectOfType<GameManager>().HurtP2();
            audioManager.PlaySFX(audioManager.knockDown);
        }
        if (collision.gameObject.CompareTag("Chest"))
        {
            if (chest != null)
            {
                chest.BulletHit();
            }
        }
        // Destroy the bullet as soon as it collides with anything
        Destroy(gameObject);
    }

}