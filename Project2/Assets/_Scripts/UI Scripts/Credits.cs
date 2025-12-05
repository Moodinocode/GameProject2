using UnityEngine;

namespace _Scripts.UI_Scripts
{
    public class Credits : MonoBehaviour
    {
        public float sccrollSpeed = 100f;
        private RectTransform _rectTransform;
        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            _rectTransform.anchoredPosition += Vector2.up * (sccrollSpeed * Time.deltaTime);
        }
    }
}
