using System.Collections;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts
{
    public class Player : MonoBehaviour
    {
        private static readonly int Death = Animator.StringToHash("Death");
        public float hp = 100;
        public GameObject bloodyScreen;
        public Image fadeImage;
        
        [HideInInspector] public Animator anim;

        void Awake()
        {
            SaveLoadManager.Instance.player = this;
        }
        void Start()
        {
            anim = GetComponent<Animator>();
        }
        public void TakeDamage(int damageAmount)
        {
            
            hp -= damageAmount;
            hp = Mathf.Clamp(hp, 0, 100);
            PlayerUIManager.Instance.UpdateHealth(hp, 100);

            if (hp == 0)
            {
                PlayerDead();
                return;
            }
            
            StartCoroutine(BloodyScreenEffect());
            
        }

        private void PlayerDead()
        {
            /*GetComponent<MouseMovement>().enabled = false;
       GetComponent<PlayerMovement>().enabled = false;*/
       
            // dying animation
            anim.SetTrigger(Death);
        }

        private IEnumerator BloodyScreenEffect()
        {
            if (bloodyScreen.activeInHierarchy == false)
            {
                bloodyScreen.SetActive(true);
            }
       
            var image = bloodyScreen.GetComponentInChildren<Image>();

            Color startColor = image.color;
            startColor.a = 1f;
            image.color = startColor;

            float duration = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

                Color newColor = image.color;
                newColor.a = alpha;
                image.color = newColor;
                elapsedTime += Time.deltaTime;
           
                yield return null;
            }
       
       
            if (bloodyScreen.activeInHierarchy)
            {
                bloodyScreen.SetActive(false);
            }
        }
        
        public void Save(ref PlayerSaveData data)
        {
            data.position = transform.position; 
            data.health = hp;
        }

        public void Load(PlayerSaveData data)
        {
            var cc = GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            transform.position = data.position;
            hp = data.health;

            PlayerUIManager.Instance.UpdateHealth(hp, 100);

            if (cc != null) cc.enabled = true;
        }

   
    }
}

[System.Serializable]
public struct PlayerSaveData
{
    public Vector3 position;
    public float health;
}
