using _Scripts.Managers;
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
        
        void Awake()
        {
            SaveLoadManager.Instance.chestState = this;
        }
        
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
        public void Save(ref ChestSaveData data)
        {
            data.opened = _opened;
        }

        public void Load(ChestSaveData data,KeySaveData keyData)
        {
            _opened = data.opened;

            if (_opened)
            {
                // Force chest open visually
                anim.SetTrigger(Open);
                if (keyData.hasKey)
                {
                    Debug.Log("Key Found");
                    key.SetActive(false); 
                }
                else
                {
                    key.SetActive(true); 
                    Debug.Log("Key Not Found");
                }
                interactUI.SetActive(false);
            }
        }
    }
}


[System.Serializable]
public struct ChestSaveData {
    public bool opened;
}