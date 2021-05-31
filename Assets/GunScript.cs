using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 20f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTmeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTmeToFire)
        {
            nextTmeToFire = Time.time + 1f / fireRate;
            shoot();
        }

        void shoot()
        {
            muzzleFlash.Play();
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if(target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }
}
