using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Pistol : Gun
{

    protected override void PrimaryFire()
    {
        if(shoot_delay_timer <= 0)
        {
            if(primary_fire_is_shooting || primary_fire_hold)
        {
            shoot_delay_timer = gun_data.primary_fire_delay; //delay shooting
            primary_fire_is_shooting = false;
            //Raycast
            Vector3 dir = Quaternion.AngleAxis(Random.Range(-gun_data.spread, gun_data.spread), Vector3.up) * cam.transform.forward;
            dir = Quaternion.AngleAxis(Random.Range(-gun_data.spread, gun_data.spread), Vector3.right) * cam.transform.forward;
            ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, gun_data.range))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green, 0.0f);
                print("hello");
            }
        ammo_in_clip--;
        if(ammo_in_clip <= 0) ammo_in_clip = gun_data.ammo_per_clip;
        }

        }

        
    }

    protected override void SecondaryFire()
    {

    }

}
