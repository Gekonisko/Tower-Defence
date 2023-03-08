using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerSelect : MonoBehaviour
    {
        [SerializeField] private GameObject tower;
        [SerializeField] private GameObject towerCreatorCursor;

        private Image _image;
        private void Start()
        {
            _image = GetComponent<Image>();
            Events.OnTowerCreate += ChangeColor;
        }

        private void ChangeColor()
        {
            _image.color = Color.white;
        }

        public void CreateTowerCreatorCursor()
        {
            if (TowerCreatorManager.IsCreateTowerMode) return;
            
            var cursor = Instantiate(towerCreatorCursor, transform.position, Quaternion.identity);
            cursor.GetComponent<TowerCreator>().tower = tower;
            _image.color = Color.black;
            TowerCreatorManager.IsCreateTowerMode = true;
        }

        private void OnDestroy()
        {
            Events.OnTowerCreate -= ChangeColor;
        }
    }
}