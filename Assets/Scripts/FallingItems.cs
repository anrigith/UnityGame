using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItems : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefab;

    [SerializeField] float secondSpawn = 0.5f;

    [SerializeField] float minTras;

    [SerializeField] float maxTras;


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
            GameObject gameObject = Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)],
                position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameObject, 12f);
        }

    }

}