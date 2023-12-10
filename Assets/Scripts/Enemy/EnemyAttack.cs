using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _distanceAttack;
    [SerializeField] private GameObject _hitBoxAttack;

    private Animator _animator;

    public float DistanceAttack => _distanceAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ActivateHitbox();
        }
    }

    public void ChangeDirection()
    {
        _hitBoxAttack.transform.localPosition = -_hitBoxAttack.transform.localPosition;
    }

    public void Attack()
    {
        _animator.SetTrigger(AnimatorEnemy.Triggers.Attack);
    }

    public void ActivateHitbox()
    {
        _hitBoxAttack.gameObject.SetActive(true);
    }

    public void DeactivateHitbox()
    {
        _hitBoxAttack.gameObject.SetActive(false);
    }
}
