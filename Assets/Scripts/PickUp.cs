using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    PlayerManager tyler;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        tyler = collision.GetComponent<PlayerManager>();

        if (collision.CompareTag("Player"))
        {
            tyler.getKey();

            Destroy(gameObject);
        }
    }

}

