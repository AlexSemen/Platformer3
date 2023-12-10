using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnerBullets;
    [SerializeField] private Bullet _bullet;

    private Bullet _newBullet;

    public void ChangeDirection()
    {
        _spawnerBullets.localPosition = -_spawnerBullets.localPosition;
        _spawnerBullets.LookAt(transform.position);
        _spawnerBullets.DOLocalRotate(new Vector3(_spawnerBullets.localRotation.eulerAngles.x, 0, _spawnerBullets.rotation.eulerAngles.y), 0);
    }

    public void Attack()
    {
        _newBullet = Instantiate(_bullet, _spawnerBullets.position, Quaternion.identity);
        _newBullet.transform.localRotation = _spawnerBullets.localRotation;
    }
}
