using System;
using ScriptableObjects;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour, IDamage
{
    public float radius = 1;
    public int damage = 1;
    public int explosionDamage = 1;
    [SerializeField] private ExplosionTowerScriptableObject towerData;
    private Projectile _projectile;

    private void Start()
    {
        _projectile = GetComponent<Projectile>();
        Upgrade();
    }
    

    public void Deal(Transform targetEnemy)
    {
        targetEnemy.GetComponent<Enemy>().DealDamage(damage);
        
        var explosion =  Physics2D.CircleCastAll(
            targetEnemy.position, radius, Vector2.up, 10);
        
        foreach(var col  in explosion)
        {
            if (col.transform.CompareTag("Enemy"))
                col.transform.GetComponent<Enemy>().DealDamage(explosionDamage);
        }
    }

    private void Upgrade()
    {
        switch (_projectile.Level)
        {
            case 1:
                radius = towerData.explosionRadius1;
                damage = towerData.damage1;
                explosionDamage = towerData.explosionDamage1;
                break;
            case 2:
                radius = towerData.explosionRadius2;
                damage = towerData.damage2;
                explosionDamage = towerData.explosionDamage2;
                break;
            case 3:
                radius = towerData.explosionRadius3;
                damage = towerData.damage3;
                explosionDamage = towerData.explosionDamage3;
                break;
        }
    }
    
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }*/
}