using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponTypes
{
    Sniper
}
public class WeaponAttributes
{
    public int MagSize;
    public float FireTime; // secconds per shot
    public float ReloadTime;


    public int SpawnCount; //amount of bullets / projectiles

    public float Inaccuracy; // in degrees

    public float Damage;
    public float HeadshotMulti;
    public bool Hitscan;

    public int ModelIndex; // weapon model
    public int TrailIndex;
    public int HitIndex;

    public float HitscanRange;

    public float ProjectileGravity;
    public float ProjectileImpulse;
    public float ProjectileDrag;
    public float ProjectileLifetime;

    public bool AreaDamage;

    public float AreaRadius;
    public float AreaImpulse;

    public WeaponAttributes(WeaponTypes type)
    {
        switch(type)
        {
            case WeaponTypes.Sniper:
                MagSize = 1;
                FireTime = 0;
                ReloadTime = 1;
                SpawnCount = 1;
                Inaccuracy = 0;
                Damage = 75;
                HeadshotMulti = 1.5f;
                Hitscan = true;
                ModelIndex = 0;
                HitIndex = 0;
                ModelIndex = 0;
                TrailIndex = 0;
                HitIndex = 0;
                HitscanRange = 100;
                AreaDamage = false;
                break;
        }
    }
}
public class Bullet
{
    public Vector3 Pos;
    public Vector3 Rot;
    public Vector3 vel;

    public Weapon ParentWeapon;
}
public class Weapon
{
    public WeaponTypes weaponType;
    public WeaponAttributes weaponAttributes;

    public int mag; //dynamic vars. Attributes are static
    public float remainingFireTime;
    public float remainingReloadTime;

    public List<Bullet> Tick(float deltaTime , bool reload , bool shoot , Transform Head )
    {
       // Debug.Log("reload time " + remainingReloadTime + " firetime " + remainingFireTime + " mag " + mag);
        if(remainingFireTime > 0)
        {
            remainingFireTime -= deltaTime;
        }
        if(remainingReloadTime > 0)
        {
            remainingReloadTime -= deltaTime;

            if(remainingReloadTime < 0)
            {
                mag = weaponAttributes.MagSize;
            }
        }


        if(!shoot || remainingReloadTime > 0 || mag == 0) //if not shooting return null
            return null;

        remainingFireTime = weaponAttributes.FireTime;
        mag--;
        if(mag <= 0)
            remainingReloadTime = weaponAttributes.ReloadTime;


        List<Bullet> bullets = new List<Bullet>();
        for(int i = 0 ; i < weaponAttributes.SpawnCount ; i++) //create bullet objects to be instantiated by the bullet handel as monobehaviours 
        {
            Bullet bullet = new Bullet();
            bullet.Pos = Head.position;
            Vector3 rot = Head.forward;

            rot = new Vector3(
                rot.x + Random.Range(-weaponAttributes.Inaccuracy , weaponAttributes.Inaccuracy) ,
                rot.y + Random.Range(-weaponAttributes.Inaccuracy , weaponAttributes.Inaccuracy) ,
                rot.z
                );

            bullet.Rot = rot;
            bullet.ParentWeapon = this;
            bullet.vel = Head.forward * weaponAttributes.ProjectileImpulse;
            bullets.Add(bullet);
        }

        return bullets;
    }

    public Weapon(WeaponTypes type)
    {
        weaponAttributes = new WeaponAttributes(type);
        mag = weaponAttributes.MagSize;
        remainingFireTime = 0;
        remainingReloadTime = weaponAttributes.ReloadTime;
    }
}

