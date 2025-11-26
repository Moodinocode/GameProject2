using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieHand zombieHand;
     public int zombieDamage;
     
   
    void Start()
    {
        zombieHand.damage = zombieDamage;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
