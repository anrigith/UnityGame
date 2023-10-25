using UnityEngine;

public class BulletIcon : MonoBehaviour
{
    // Adjust the amount of bullets to add when collected.
    public int bulletsToAdd = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("firstPlayer") || collision.CompareTag("secondPlayer"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // playerController.maxBullets += bulletsToAdd;
                playerController.AddBullet(bulletsToAdd);
                Destroy(gameObject); // Destroy the bullet icon when collected.
            }
        }
    }
}