using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<Weapon> weapons = new List<Weapon>();
    int ActiveWeapon; // refers to index of weapons
    public void AddWeapon(WeaponTypes type)
    {
        Weapon w = new Weapon(type);
        weapons.Add(w);
    }
    public void Tick(PlayerInput input,Transform head) //handels reloading & firing
    {
       //tick all weapons that are not active
       for(int i = 0; i < weapons.Count ; i++)
        {
            if(i == ActiveWeapon)
                continue;


            weapons[i].Tick(Time.deltaTime ,reload: false ,shoot: false , head); //cannot be reloaded or shot due to the weapon not being active
                                                                                 //so those args are hard coded to be false
        }

       var bullets = weapons[ActiveWeapon].Tick(Time.deltaTime , input.reload , input.fire , head);
        if(bullets == null)
            return;


        //only executes if bullet/s exist
        foreach(Bullet b in bullets)
        {
            if(b.ParentWeapon.weaponAttributes.Hitscan)
            {
                handelHitscan(b , head);
            }
            else
            {
                handelProjectile(b , head);
            }
        }
    }

    

    private void handelHitscan(Bullet b,Transform head)
    {
        Debug.Log("rot = " + b.Rot + " vs head " + head.forward);
        RaycastHit hit;
        Debug.Log("bang");
        if(Physics.Raycast(head.position ,b.Rot, out hit , weapons[ActiveWeapon].weaponAttributes.HitscanRange)) //if hit
        {
            Debug.Log("name hit " +  hit.collider.gameObject.name);
        }
    }
    private void handelProjectile(Bullet b, Transform head)
    {

    }
}
