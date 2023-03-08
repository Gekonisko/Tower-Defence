using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int level = 1;
    
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Animator weaponAnimator;
    
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeToShotInSeconds;
    private float _timer;

    private Animator _animator;
    private Transform _targetEnemy = null;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        RotateWeaponToEnemy();

        if (CanShoot())
            Shoot();

    }
    
    [ContextMenu("UpgradeToLevel2")]
    public void UpgradeToLevel2() => UpgradeToLevel(2);
    
    [ContextMenu("UpgradeToLevel3")]
    public void UpgradeToLevel3() => UpgradeToLevel(3);

    private void UpgradeToLevel(int l)
    {
        level = l;
        switch (level)
        {
            case 2:
                weaponTransform.localPosition = new Vector3(0, 0.3f, 0);
                weaponAnimator.Play("IdleLevel2");
                _animator.Play("Level2");
                break;
            case 3:
                weaponTransform.localPosition = new Vector3(0, 0.4f, 0);
                weaponAnimator.Play("IdleLevel3");
                _animator.Play("Level3");
                break;
        }
    }

    private bool CanShoot() => _timer > timeToShotInSeconds;

    private void Shoot()
    {
        if (_targetEnemy == null) return;
        
        var p = Instantiate(projectile, weaponTransform.position, weaponTransform.rotation);
        p.GetComponent<Projectile>().targetEnemy = _targetEnemy;
        p.GetComponent<Projectile>().level = level;
        
        weaponAnimator.Play("ShootLevel" + level);
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
