using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBullet : MonoBehaviour
{
    public int bulletsToAdd = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "firstPlayer" || collision.gameObject.tag == "secondPlayer")
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