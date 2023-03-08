using System;
using ScriptableObjects;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour, IDamage
{
    [SerializeField] private float radius = 1;
    [SerializeField] private ExplosionTowerScriptableObject towerData;

    private Projectile _projectile;

    private void Start()
    {
        _projectile = GetComponent<Projectile>();
        Upgrade();
    }
    

    public void Deal(Transform targetEnemy)
    {
        targetEnemy.GetComponent<Enemy>().Death();
        
        var explosion =  Physics2D.CircleCastAll(
            targetEnemy.position, radius, Vector2.up, 10);
        
        foreach(var col  in explosion)
        {
            if (col.transform.CompareTag("Enemy"))
                col.transform.GetComponent<Enemy>().Death();
        }
    }

    private void Upgrade()
    {
        switch (_projectile.Level)
        {
            case 1:
                radius = towerData.explosionRadius1;
                break;
            case 2:
                radius = towerData.explosionRadius2;
                break;
            case 3:
                radius = towerData.explosionRadius3;
                break;
        }
    }
    
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }*/
}