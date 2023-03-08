using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
    public class EnemyScriptableObject : ScriptableObject
    {
        public int hp;
        public int speed;
    }
}