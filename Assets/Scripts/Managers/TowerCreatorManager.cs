using System;

namespace Managers
{
    public class TowerCreatorManager : Singleton<TowerCreatorManager>
    {
        public static bool IsCreateTowerMode = false;

        private void Start()
        {
            IsCreateTowerMode = false;
            Events.OnTowerCreate += ChangeMode;
        }

        private void ChangeMode()
        {
            IsCreateTowerMode = false;
        }

        private void OnDestroy()
        {
            Events.OnTowerCreate -= ChangeMode;
        }
    }
}