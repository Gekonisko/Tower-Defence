using ScriptableObjects;
using UnityEngine;

public class PointDamage : MonoBehaviour, IDamage
{
    public int damage;
    
    [SerializeField] private BaseTowerScriptableObject towerData;
    private Projectile _projectile;
    
    private void Start()
    {
        _projectile = GetComponent<Projectile>();
        Upgrade();
    }
    
    public void Deal(Transform targetEnemy)
    {
        targetEnemy.GetComponent<Enemy>().DealDamage(damage);
    }
    
    private void Upgrade()
    {
        switch (_projectile.Level)
        {
            case 1:
                damage = towerData.damage1;
                break;
            case 2:
                damage = towerData.damage2;
                break;
            case 3:
                damage = towerData.damage3;
                break;
        }
    }
}