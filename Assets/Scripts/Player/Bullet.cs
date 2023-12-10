using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3.0f);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * _speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Samurai>(out Samurai samurai))
        {
            samurai.TakeDamage();
        }

        Destroy(gameObject);
    }
}
