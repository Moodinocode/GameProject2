using _Scripts.ChestScripts;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class Portal : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                KeyScript keyScript = other.GetComponent<KeyScript>();
                if (keyScript != null && keyScript.hasKey)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    PlayerUIManager.Instance.ShowPortalMessage();
                }
            }
        }
    }
}
