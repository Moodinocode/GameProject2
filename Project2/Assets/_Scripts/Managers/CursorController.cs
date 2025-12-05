using UnityEngine;

namespace _Scripts.Managers
{
    public class CursorController : MonoBehaviour
    {
        void Start()
        {
            HideCursor();
        }

        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}