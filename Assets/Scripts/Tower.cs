using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Level => _level;
    private int _level = 0;
    
    [SerializeField] private BaseTowerScriptableObject towerData;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Animator weaponAnimator;
    
    [SerializeField] private GameObject projectile;
    private float _timeToShotInSeconds;
    private float _timer;

    private Animator _animator;
    private CircleCollider2D _collider2D;
    
    private Transform _targetEnemy = null;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<CircleCollider2D>();
        Upgrade();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        RotateWeaponToEnemy();

        if (CanShoot())
            Shoot();

    }

    [ContextMenu("Upgrade")]
    public void Upgrade()
    {
        _level++;
        switch (_level)
        {
            case 1:
                _timeToShotInSeconds = towerData.timeToShotInSeconds1;
                _collider2D.radius = towerData.zoneRadius1;
                break;
            case 2:
                weaponTransform.localPosition = new Vector3(0, 0.3f, 0);
                weaponAnimator.Play("IdleLevel2");
                _animator.Play("Level2");
                _timeToShotInSeconds = towerData.timeToShotInSeconds2;
                _collider2D.radius = towerData.zoneRadius2;
                break;
            case 3:
                weaponTransform.localPosition = new Vector3(0, 0.4f, 0);
                weaponAnimator.Play("IdleLevel3");
                _animator.Play("Level3");
                _timeToShotInSeconds = towerData.timeToShotInSeconds3;
                _collider2D.radius = towerData.zoneRadius3;
                break;
        }
    }

    private bool CanShoot() => _timer > _timeToShotInSeconds;

    private void Shoot()
    {
        if (_targetEnemy == null) return;
        
        var projectileObject = Instantiate(projectile, weaponTransform.position, weaponTransform.rotation);
        var projectileComponent = projectileObject.GetComponent<Projectile>();
        projectileComponent.targetEnemy = _targetEnemy;
        projectileComponent.Level = Level;
        
        weaponAnimator.Play("ShootLevel" + Level);
        _timer = 0;
    }

    private void RotateWeaponToEnemy()
    {
        if(_targetEnemy == null) return;
        
        var dir = (_targetEnemy.position - transform.position).normalized;
        var rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_targetEnemy == null && other.CompareTag("Enemy"))
            _targetEnemy = other.transform;
    }
}
