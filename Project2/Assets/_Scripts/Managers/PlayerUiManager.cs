using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;

        [Header("Health")]
        public Image healthBarFill;   // Only the fill image
        public float healthLerpSpeed = 5f;
        private float _targetHealthFill = 1f;

        [Header("Ammo")]
        public TextMeshProUGUI ammoText;
        public Image ammoImage;

        [Header("Key")]
        public Image keyIcon;

        [Header("Portal Message")]
        public GameObject portalMessage;   // The whole panel (BG + text)

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            keyIcon.gameObject.SetActive(false);
            portalMessage.SetActive(false);
        }

        // ---------------- UI UPDATE METHODS ----------------

        public void UpdateHealth(float current, float max)
        {
            _targetHealthFill = current / max;
        }

        public void UpdateAmmo(int clip, int total)
        {
            ammoText.text = clip + " / " + total;
        }
    
        private void Update()
        {
            if (!healthBarFill)
            {
                healthBarFill.fillAmount = Mathf.Lerp(
                    healthBarFill.fillAmount,
                    _targetHealthFill,
                    Time.deltaTime * healthLerpSpeed
                );
            }
        }

        public void ShowKeyIcon()
        {
            keyIcon.gameObject.SetActive(true);
        }

        public void ShowPortalMessage()
        {
            portalMessage.SetActive(true);

            CancelInvoke(nameof(HidePortalMessage));
            Invoke(nameof(HidePortalMessage), 2.0f);
        }

        void HidePortalMessage()
        {
            portalMessage.SetActive(false);
        }
    }
}