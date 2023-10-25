using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // private int currentHealth;
    // public void Heal(int amount)
    // {
        // currentHealth += amount;
        // You can add any other logic here, such as checking for max health limit or updating health UI.
    // }
    public Text PlayerBulletText;
    public int maxBullets;
    // private int bulletsFired = 0;

    void Start()
    {
        PlayerBulletText.text = maxBullets.ToString();
    }

    public bool CanFireBullet()
    {
        // return bulletsFired < maxBullets;
        return maxBullets != 0; 
    }

    public void BulletFired()
    {
        // bulletsFired++;
        maxBullets--;
        PlayerBulletText.text = maxBullets.ToString();
    }

    public void AddBullet(int bullet) 
    {
        maxBullets += bullet;
        PlayerBulletText.text = maxBullets.ToString();
    }
}