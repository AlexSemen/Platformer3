using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAttack))]

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool _isAttack;
    [SerializeField] private int _hitPoint;
    [SerializeField] private int _hitPointMax;

    public event UnityAction<int, int> HealthChanged;
    private bool isFacingRight = true;
    public bool _isGrounded;
    private float _groundRadius = 0.2f;
    private float _move;
    private float _timeImmortality;
    private WaitForSeconds _waitForImmortality;
    private float _timeBlinkColorDamage;
    private int _quantityBlinkColorDamage;
    private bool _immortality;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    
    public bool IsWounded 
    { 
        get 
        {
            if (_hitPoint < _hitPointMax)
            {
                return true; 
            }
            else
            {
                return false;
            }
        }
    }

    private void Awake()
    {
        _hitPoint = _hitPointMax;
        _timeBlinkColorDamage = 0.5f;
        _quantityBlinkColorDamage = 6;
        _immortality = false;
        _timeImmortality = 3f;
        _waitForImmortality = new WaitForSeconds(_timeImmortality);
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _playerMove.Jump();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetTrigger(AnimatorPlayer.Triggers.Attack);
            _playerAttack.Attack();
        }

        HealthChanged.Invoke(_hitPoint, _hitPointMax);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundMask);

        if (_isAttack == false)
        {
            _move = Input.GetAxis("Horizontal");

            _playerMove.Move(_move);

            _animator.SetFloat(AnimatorPlayer.Params.Speed, Math.Abs(_move));

            if (_move > 0 && !isFacingRight)
            {
                isFacingRight = true;
                _spriteRenderer.flipX = false;
                _playerAttack.ChangeDirection();
            }
            else if (_move < 0 && isFacingRight)
            {
                isFacingRight = false;
                _spriteRenderer.flipX = true;
                _playerAttack.ChangeDirection();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (_immortality == false)
        {
            _hitPoint -= damage;
            HealthChanged.Invoke(_hitPoint, _hitPointMax);

            _spriteRenderer.DOColor(Color.red, _timeBlinkColorDamage).SetLoops(_quantityBlinkColorDamage, LoopType.Yoyo);

            StartCoroutine(Immortality());
        }
    }

    public void Heal()
    {
        if (_hitPoint < _hitPointMax)
        {
            _hitPoint += 1;
            HealthChanged.Invoke(_hitPoint, _hitPointMax);
        }
    }

    private IEnumerator Immortality()
    {
        _immortality = true;

        yield return _waitForImmortality;

        _immortality = false;
    }
}
