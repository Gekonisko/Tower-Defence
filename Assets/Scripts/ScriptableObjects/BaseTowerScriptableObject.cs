using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseTower", menuName = "ScriptableObjects/BaseTower", order = 1)]
    public class BaseTowerScriptableObject : ScriptableObject
    {
        [Header("Level 1")]
        public float timeToShotInSeconds1;
        public float zoneRadius1;
        public int damage1;
        [Header("Level 2")]
        public float timeToShotInSeconds2;
        public float zoneRadius2;
        public int damage2;
        [Header("Level 3")]
        public float timeToShotInSeconds3;
        public float zoneRadius3;
        public int damage3;
    }
}