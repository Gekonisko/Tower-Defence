using UnityEngine;

public class ExplosionDamage : MonoBehaviour, IDamage
{
    [SerializeField] private float radius = 1;
    
    public void Deal(Transform targetEnemy)
    {
        targetEnemy.GetComponent<Enemy>().Death();
        
        var explosion =  Physics2D.CircleCastAll(
            targetEnemy.position, radius, Vector2.up, 1);
        
        foreach(var col  in explosion)
        {
            if (col.transform.CompareTag("Enemy"))
                col.transform.GetComponent<Enemy>().Death();
        }
    }
    
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }*/
}