using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.UI_Scripts
{
    public class AttachToGlobalCanvas : MonoBehaviour
    {
        void Start()
        {
            if (CanvasManager.Instance != null)
            {
                transform.SetParent(CanvasManager.Instance.gameUICanavs.transform, false);
                
                CanvasManager.Instance.RegisterDynamicUI(gameObject);
            }
            else
            {
                Debug.LogError("Global canvas not found!");
            }
        }
        
    }
}
