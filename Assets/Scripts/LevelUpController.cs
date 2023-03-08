using System;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPopUp;
    
    private Camera _camera;
    private BoxCollider2D _collider2D;
    private Tower _tower;
    private void Start()
    {
        _camera = Camera.main;
        _collider2D = GetComponent<BoxCollider2D>();
        _tower = transform.parent.GetComponent<Tower>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _tower.Level < 3)
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var pos = transform.position;
            var size = _collider2D.size / 2;

            if ((mousePos.x < pos.x + size.x && mousePos.x > pos.x - size.x) &&
                (mousePos.y < pos.y + size.y && mousePos.y > pos.y - size.y))
            {
                if (levelUpPopUp.activeInHierarchy)
                {
                    _tower.Upgrade();
                    levelUpPopUp.SetActive(false);
                }
                else
                    levelUpPopUp.SetActive(true);
            }
            else
                levelUpPopUp.SetActive(false);
        }
    }
}