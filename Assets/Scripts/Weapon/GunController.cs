using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunController : MonoBehaviour
{
    public float range = 100f;
    public float fireRate = 0.2f;
    public int damage = 10;
    public int magSize = 6;
    public int magReserve = 48;
    private int magCurrentCount;
    private bool canFire;
    private float nextFireTime;
    
    public TMP_Text hudAmmo;
    public GameObject bloodEffect;
    private GameObject cloneBloodEffect;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public Transform muzzle;
    
    void Start()
    {
        canFire = true;
        magCurrentCount = magSize;

        shootSound = GetComponent<AudioSource>();
       
    }

    void Update()
    {
        hudAmmo.text = "Ammo: " + magCurrentCount + "/" + magReserve;
        
        Reload();
        
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out RaycastHit hit, 5.0f))
        {
            AmmoCrate ammoCrate = hit.transform.GetComponent<AmmoCrate>();

            if (ammoCrate != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && PlayerController.Instance.playerMoney >= 1000)
                {
                    magReserve += magSize * 4;
                    PlayerController.Instance.playerMoney -= 1000;
                }
            }
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            
            if(canFire)
            {
                Shoot();
            }

        }

        if (magReserve < 0)
        {
            magReserve = 0;
        }
    }

    void Reload()
    {
        if (magCurrentCount <= 0)
        {
            canFire = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && magReserve > 0)
        {
            magReserve += magCurrentCount;
            magCurrentCount = magSize;
            magReserve -= magSize;
            canFire = true;
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        muzzleFlash.Play();
        shootSound.Play();
        magCurrentCount -= 1;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, range))
        {
            // Check if the raycast hit an enemy
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                cloneBloodEffect = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(cloneBloodEffect, 1f);   
            }
            
            //Destroy(bloodEffect, 2.0f);

            // Create a bullet impact effect at the hit point
            //Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
           // Destroy(impactEffect, 2f);
        }
    }
}






