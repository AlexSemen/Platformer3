using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _castMask;
    [SerializeField] private float _distanceCast;

    private Rigidbody2D _rigidbody2D;
    private RaycastHit2D _hit;
    private Vector2 _endCastPoint;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
    }

    public void Move(float move)
    {
        _endCastPoint = transform.position + Vector3.right * _distanceCast;

        _hit = Physics2D.Linecast(transform.position, _endCastPoint, _castMask);

        if (_hit.collider == null)
        {
            _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
        }
    }
}
