using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{
    private Player _player;
    private int _damage;

    private void Awake()
    {
        _damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player _player))
        {
            _player.TakeDamage(_damage);
        }
    }
}
