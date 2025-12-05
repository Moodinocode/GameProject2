using UnityEngine;

namespace _Scripts.Weapons
{
    public class CrosshairUI : MonoBehaviour
    {
        public RectTransform top;
        public RectTransform bottom;
        public RectTransform left;
        public RectTransform right;

        public float baseGap = 15f;
        public float bloomMultiplier = 4f;

        private WeaponBloom _bloom;

        void Start()
        {
            _bloom = FindObjectOfType<WeaponBloom>();
        }

        void Update()
        {
            if (_bloom == null) return;

            float spread = _bloom.CurrentBloom * bloomMultiplier;

            top.anchoredPosition    = new Vector2(0,  baseGap + spread);
            bottom.anchoredPosition = new Vector2(0, -baseGap - spread);
            left.anchoredPosition   = new Vector2(-(baseGap + spread), 0);
            right.anchoredPosition  = new Vector2(baseGap + spread, 0);
        }
    }
}