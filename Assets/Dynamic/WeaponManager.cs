using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<Weapon> weapons = new List<Weapon>();
    int ActiveWeapon; // refers to index of weapons
    void AddWeapon(WeaponTypes type)
    {

    }
    void Tick(PlayerInput input,Transform head)
    {
       //tick all weapons that are not active
       for(int i = 0; i < weapons.Count ; i++)
        {
            if(i == ActiveWeapon)
                continue;


            weapons[i].Tick(Time.deltaTime ,reload: false ,shoot: false , head); //cannot be reloaded or shot due to the weapon not being active
                                                                                 //so those args are hard coded to be false


        }
    }
}
