using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform targetEnemy;
    public float speed;
    public int level = 1; 
    
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private IDamage _damageDealer;

    private bool _isDying = false;

    private void Start()
    {
        _damageDealer = GetComponent<IDamage>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.Play("IdleLevel" + level);
    }
    
    private void Update()
    {
        if (targetEnemy == null)
            Death();
        
        RotateToEnemy();
    }

    private void FixedUpdate()
    {
        if (targetEnemy == null) return;
        
        var direction = (targetEnemy.position - transform.position).normalized;
        _rigidbody2D.velocity = direction * speed;
    }
    
    private void RotateToEnemy()
    {
        if(targetEnemy == null) return;
        
        var dir = (targetEnemy.position - transform.position).normalized;
        var rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void Death()
    {
        _isDying = true;
        _animator.Play("DeathLevel" + level);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isDying && col.transform == targetEnemy)
        {
            Death();
            _damageDealer.Deal(targetEnemy);
        }
    }
}