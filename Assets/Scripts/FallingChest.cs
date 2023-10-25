using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingChest : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefab;

    [SerializeField] float secondSpawn;

    [SerializeField] float minTras;

    [SerializeField] float maxTras;

    [SerializeField] ChestBullet chestBullet;

    GameObject gameObject;

    private int hitCount = 0;


    void Start()
    {
        StartCoroutine(ItemSpawn());
    }
    IEnumerator ItemSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            gameObject = Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
        }

    }
    public void BulletHit()
    {
        hitCount++;
        if (hitCount >= 2)
        {
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;
            Quaternion savedRotation = gameObject.transform.rotation;
            Destroy(gameObject);
            hitCount = 0;
            ChestBullets(x, y, savedRotation);
        }
        Debug.Log(hitCount);
    }
    private void ChestBullets(float x, float y, Quaternion savedRotation)
    {
        ChestBullet bullet = Instantiate(chestBullet, new Vector2(x, y), savedRotation);
        Debug.Log("ChestBullet Creaeted");
    }
    public void UpdateHitCount()
    {
        hitCount = 0;
    }
}