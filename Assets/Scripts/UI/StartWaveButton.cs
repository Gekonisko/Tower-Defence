using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartWaveButton : MonoBehaviour
    {
        private Image _image;
        private void Start()
        {
            _image = GetComponent<Image>();
            Events.OnEndWave += EndWave;
        }

        public void StartWave()
        {
            if (EnemySpawner.IsWavePlaying) return;
            
            _image.color = Color.black;
            Events.OnStartWave.Invoke();
            
        }

        private void EndWave()
        {
            _image.color = Color.white;
        }
    }
}