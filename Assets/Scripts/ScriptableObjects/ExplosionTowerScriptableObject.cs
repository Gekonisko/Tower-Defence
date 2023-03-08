using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ExplosionTower", menuName = "ScriptableObjects/ExplosionTower", order = 1)]
    public class ExplosionTowerScriptableObject : BaseTowerScriptableObject
    {
        [Header("Level 1")]
        public int explosionDamage1;
        public float explosionRadius1;
        [Header("Level 2")]
        public int explosionDamage2;
        public float explosionRadius2;
        [Header("Level 3")]
        public int explosionDamage3;
        public float explosionRadius3;
    }
}