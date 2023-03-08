using UnityEngine;

public class PointDamage : MonoBehaviour, IDamage
{
    public void Deal(Transform targetEnemy)
    {
        targetEnemy.GetComponent<Enemy>().Death();
    }
}