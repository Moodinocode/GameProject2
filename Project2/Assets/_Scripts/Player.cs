using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public int HP = 100;
   
   public void TakeDamage(int damageAmount)
   {
       HP -= damageAmount;

       if (HP <= 0)
       {
           print("Player Dead");
       }
       else
       {
           print("Player hit");
       }
   }

   private void onTriggerEnter(Collider other)
   {
       if (other.CompareTag("ZombieHand"))
       {
           TakeDamage(25);
       }
   }
   
}
