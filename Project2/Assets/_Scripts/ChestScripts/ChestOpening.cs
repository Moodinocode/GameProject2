using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ChestScripts
{
    public class ChestOpening : MonoBehaviour
    {
        private static readonly int Open = Animator.StringToHash("Open");
        public GameObject interactUI;
        public TextMeshProUGUI  text;
        public Image progress;
        public float holdTime = 1.7f;
        public Animator anim;
        public AudioClip chestOpenSound;
        public GameObject key;
        
        private bool _playerNearby;
        private float _holdTimer;
        private bool _opened;
        private AudioSource _audioSource;
        
        
        void Start()
        {
            interactUI.SetActive(false);
            progress.fillAmount = 0;
            _audioSource = GetComponent<AudioSource>();
            
            _audioSource.loop = true;       
        }

    
        void Update()
        {
            if (_opened || !_playerNearby) return;
            
            if (Input.GetKey(KeyCode.E))
            {
                _holdTimer += Time.deltaTime;
                progress.fillAmount = _holdTimer / holdTime;

                if (_holdTimer >= holdTime)
                    OpenChest();
            }
            else
            {
                _holdTimer = 0f;
                progress.fillAmount = 0f;
            }
        }
    
        
        void OnTriggerEnter(Collider other)
        {
            if (_opened) return;
            if (other.CompareTag("Player"))
            {
                _playerNearby = true;
                interactUI.SetActive(true);
                text.text = "Hold E";
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerNearby = false;
                interactUI.SetActive(false);
                _holdTimer = 0f;
                progress.fillAmount = 0f;
            }
        }
        
        void OpenChest()
        {
            _opened = true;
            _audioSource.Stop();
            interactUI.SetActive(false);
            anim.SetTrigger(Open);
            _audioSource.PlayOneShot(chestOpenSound);
            key.SetActive(true);
        }
    }
}
