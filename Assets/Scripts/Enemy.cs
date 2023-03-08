using System;
using Enums;
using Managers;
using ScriptableObjects;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;
    [SerializeField] private float speed;
    [SerializeField] private float hp;
    [SerializeField] private Direction direction = Direction.None;
    [SerializeField] private bool isDying = false;
    [SerializeField] private bool flipX = false;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _targetPosition;
    void Start()
    {
        speed = enemyData.speed;
        hp = enemyData.hp;
        
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _targetPosition = PathManager.Instance.GetNextPosition(transform.position);
        UpdateDirection();
    }

    void Update()
    {
        if (isDying) return;
        
        transform.position = Vector3.MoveTowards(
            transform.position, _targetPosition, Time.deltaTime * speed);

        if (_targetPosition == transform.position)
        {
            if (PathManager.Instance.IsEndOfPath(transform.position))
            {
                Events.OnEnemyReachEnd.Invoke();
                Death();
            }

            _targetPosition = PathManager.Instance.GetNextPosition(transform.position);
            UpdateDirection();
        }
    }

    public void DestroyThis() => Destroy(gameObject);

    public void DealDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Death();
    }
    
    private void Death()
    {
        isDying = true;
        tag = "Untagged";
        SetDyingAnimation();
    }

    private void UpdateDirection()
    {
        var directionVec = (_targetPosition - transform.position).normalized;
        var cos = Vector3.Dot(Vector3.up, directionVec);
        switch (cos)
        {
            case > 0.7071f: // 45 stopni
                direction = Direction.Up; break;
            case > -0.7071f: // 135 stopni
                direction = (directionVec.x > 0) ? Direction.Right : Direction.Left;
                break;
            default:
                direction = Direction.Down; break;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        switch (direction)
        {
            case Direction.Up:
                _spriteRenderer.flipX = flipX;
                _animator.Play("Up"); 
                break;
            case Direction.Right:
                _spriteRenderer.flipX = !flipX;
                _animator.Play("Side"); 
                break;
            case Direction.Down:
                _spriteRenderer.flipX = flipX;
                _animator.Play("Down"); 
                break;
            case Direction.Left:
                _spriteRenderer.flipX = flipX;
                _animator.Play("Side"); 
                break;
        }
    }
    
    private void SetDyingAnimation()
    {
        switch (direction)
        {
            case Direction.Up:
                _spriteRenderer.flipX = flipX;
                _animator.Play("UpDeath"); 
                break;
            case Direction.Right:
                _spriteRenderer.flipX = !flipX;
                _animator.Play("SideDeath"); 
                break;
            case Direction.Down:
                _spriteRenderer.flipX = flipX;
                _animator.Play("DownDeath"); 
                break;
            case Direction.Left:
                _spriteRenderer.flipX = flipX;
                _animator.Play("SideDeath"); 
                break;
        }
    }

}

