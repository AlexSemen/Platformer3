using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeDirection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Samurai>(out Samurai samurai))
        {
           samurai.ChangeDirection();
        }
    }
}
