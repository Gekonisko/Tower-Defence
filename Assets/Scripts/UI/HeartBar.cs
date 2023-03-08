using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeartBar : MonoBehaviour
    {
        [SerializeField] private int hp;
        private int _maxHp;

        private Slider _slider;

        private void Start()
        {
            _maxHp = hp;
            _slider = GetComponent<Slider>();
            Events.OnEnemyReachEnd += DecreaseHeart;
        }

        private void DecreaseHeart()
        {
            hp -= 1;
            _slider.value = hp / (float)_maxHp;
        }

        private void OnDestroy()
        {
            Events.OnEnemyReachEnd -= DecreaseHeart;
        }
    }
}