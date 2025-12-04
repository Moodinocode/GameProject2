using _Scripts.Managers;
using UnityEngine;

namespace _Scripts
{
    public class AttachToGlobalCanvas : MonoBehaviour
    {
        void Start()
        {
            if (CanvasManager.Instance != null)
            {
                transform.SetParent(CanvasManager.Instance.gameUICanavs.transform, false);
            }
            else
            {
                Debug.LogError("Global canvas not found!");
            }
        }
    }
}
