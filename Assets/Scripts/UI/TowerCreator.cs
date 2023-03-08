using System;
using UnityEngine;

namespace UI
{
    public class TowerCreator : MonoBehaviour
    {
        public GameObject tower;
        
        private Camera _camera;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _spriteRenderer.color == Color.white)
            {
                Instantiate(tower, transform.position + new Vector3(0, 0.5f), Quaternion.identity);
                Events.OnTowerCreate.Invoke();
                Destroy(gameObject);
            }
            
            UpdatePosition();
        }

        void UpdatePosition()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var correctPosition = GetCorrectMousePosition(mousePos);
            if (correctPosition != transform.position)
            {
                transform.position = correctPosition;
                _spriteRenderer.color = Color.white;
            }
        }

        public Vector3 GetCorrectMousePosition(Vector3 worldPos) 
            => new Vector3((int)Math.Floor(worldPos.x) + 0.5f, (int)Math.Floor(worldPos.y) + 0.5f);

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Tower") || other.CompareTag("Obstacle"))
                _spriteRenderer.color = Color.red;
        }
    }
}