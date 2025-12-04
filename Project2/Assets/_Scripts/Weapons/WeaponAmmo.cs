using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class WeaponAmmo : MonoBehaviour
    {
        public int clipSize;
        public int extraAmmo;
        public int currentAmmo;

        public AudioClip magInSound;
        public AudioClip magOutSound;
        public AudioClip releaseSlideSound;
        
        void Awake()
        {
            SaveLoadManager.Instance.ammo = this;
        }
        void Start()
        {
            currentAmmo = clipSize;
            PlayerUIManager.Instance.UpdateAmmo(currentAmmo, extraAmmo);
        }
    
        public void Reload()
        {
            if (extraAmmo >= clipSize)
            {
                int ammoToReload = clipSize - currentAmmo;
                currentAmmo += ammoToReload;
                extraAmmo -= ammoToReload;
            } else if (extraAmmo > 0)
            {
                if (extraAmmo + currentAmmo >= clipSize)
                {
                    int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                    extraAmmo = leftOverAmmo;
                    currentAmmo = clipSize;
                }
                else
                {
                    currentAmmo += extraAmmo;
                    extraAmmo = 0;
                }
            } 
            PlayerUIManager.Instance.UpdateAmmo(currentAmmo, extraAmmo);
        }
        
        public void Save(ref AmmoSaveData data)
        {
            data.currentAmmo = currentAmmo;
            data.extraAmmo = extraAmmo;
        }

        public void Load(AmmoSaveData data)
        {
            currentAmmo = data.currentAmmo;
            extraAmmo = data.extraAmmo;
            PlayerUIManager.Instance.UpdateAmmo(currentAmmo, extraAmmo);
        }
        
    }
}


[System.Serializable]
public struct AmmoSaveData
{
    public int currentAmmo;
    public int extraAmmo;
}