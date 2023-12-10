using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

[RequireComponent(typeof(Samurai))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _distanceVision;
    [SerializeField] private LayerMask _castMask;

    private RaycastHit2D _hit;
    private Samurai _samurai;
    private Vector2 _endCastPoint;
    private Transform _castPoint;

    private void Awake()
    {
        _samurai = GetComponent<Samurai>();

        _castPoint = _samurai.CastPoint;
    }

    private void FixedUpdate()
    {
        _endCastPoint = _castPoint.position + Vector3.right * _distanceVision;

        _hit = Physics2D.Linecast(_castPoint.position, _endCastPoint, _castMask);

        if(_hit.collider != null)
        {
            Debug.DrawLine(_castPoint.position, _hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(_castPoint.position, _endCastPoint, Color.red);
        }

        if (_hit.collider != null && _hit.collider.GetComponent<Player>())
        {
            _samurai.SetPlayer(_hit.collider.GetComponent<Player>());
        }
    }
       
    public void ChangeDirection()
    {
        _distanceVision = -_distanceVision;
    }
}
