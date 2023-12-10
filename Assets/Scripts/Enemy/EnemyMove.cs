using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Samurai))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _castMask;
    [SerializeField] private float _distanceCast;

    private Rigidbody2D _rigidbody2D;
    private RaycastHit2D _hit;
    private Vector2 _endCastPoint;
    private Samurai _samurai;
    private Transform _castPoint;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _samurai = GetComponent<Samurai>();

        _castPoint = _samurai.CastPoint;
    }

    public void Move()
    {
        _endCastPoint = _castPoint.position + Vector3.right * _distanceCast;
        
        _hit = Physics2D.Linecast(_castPoint.position, _endCastPoint, _castMask);
        
        if (_hit.collider == null)
        {
          _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        }
    }

    public void ChangeDirection()
    {
        _distanceCast = -_distanceCast;
        _speed = -_speed;
    }
}
