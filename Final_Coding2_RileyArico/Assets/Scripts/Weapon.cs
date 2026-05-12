using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Data")]
    public GameObject bulletPrefab;
    public float bulletLifeTime = 5f;
    public float bulletVelocity = 20f;
    public Camera playerCam;

    [Header("New Bullet System")]
    public float damage = 1f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 60f;

    //ammo management
    public string weaponName;
    public int maxAmmo = 5;
    [HideInInspector] public int activeAmmo;
    [HideInInspector] public int heldExtraAmmo = 0;

    //cooldown stuff
    public float fireCooldown;
    private float timer = 0;
    private bool canFire = true;

    //sfx
    private SoundManager soundManager;
    public SoundEvent reload;
    public SoundEvent shoot;

    private void Start()
    {
        activeAmmo = maxAmmo;
        playerCam = Camera.main;
        soundManager = FindFirstObjectByType<SoundManager>();
    }

    void Update()
    {
        //subtract time elapsed from timer
        if (timer > 0)
        { 
            timer -= Time.deltaTime;
            canFire = false;
        }

        //checks if cooldown is over
        if (timer <= 0)
        {
            //makes weapon able to fire
            canFire = true;

            //resets the cooldown
            //timer = fireCooldown;
        }

        //so it doesnt shoot before we pick it up, it has to be active
        //when we click the left mouse button & we have the weapon & its active
        if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && canFire && activeAmmo > 0)
        {
            Debug.Log("Fire Performed");
            //perform fire
            Fire();
            soundManager.PlaySound(shoot);
            timer = fireCooldown;
            activeAmmo -= 1;
        }
        else if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && canFire && activeAmmo <= 0)
        {
            Debug.Log("Out of Active Ammo! Reload with R or pickup more!");
        }

        if (this.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Fire()
    {
        //new function
        muzzleFlash.Play();

        RaycastHit hit;
        if( Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit)) //range could go here at the end, but I want to leave it out
        {
            Debug.Log(hit.transform.name);
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if(enemy != null)
            {
                enemy.currentHealth -= damage;
            }

            Target targ = hit.transform.GetComponent<Target>();
            if(targ != null)
            {
                targ.currentHealth -= damage;
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
                Debug.Log("Did " + (-hit.normal * impactForce) + " force");
            }

            GameObject impactGameobject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameobject, 1f);

        }

        /*
        //the direction we want the bullet to go in
         Vector3 direction = -transform.right; //Oh my god idk why this works but doing "-" makes it rotate -90*
        //we want to take the position of our weapon & add transform.up and then offset it so it is in front.
        Vector3 bulletSpawnPos = transform.position + transform.up * 0.5f;

        //Quaternion rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 100f))
        {
            //spawn bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position, this.transform.rotation);
            //shoot bullet
            bullet.GetComponent<Rigidbody>().AddForce(direction * bulletVelocity, ForceMode.Impulse);
            Debug.Log("Spawned Bullet");
            //destroy bullet after some time (after 3 seconds)
            Destroy(bullet, bulletLifeTime);
        }
        */

    }

    private void Reload()
    {
        //idk if we will use this
        if (heldExtraAmmo > 0)
        {
            soundManager.PlaySound(reload);
            //check the amount of ammo missing from active
            int emptyammo = maxAmmo - activeAmmo;

            //check if we can add all of EmptyAmmo, or as much as we have
            //so if the blank space is more than held,
            if (emptyammo >= heldExtraAmmo)
            {
                //add only what is held
                activeAmmo += heldExtraAmmo;
                heldExtraAmmo = 0;
            }
            else
            {
                //if we have more extraAmmo than empty,
                //subtract empty from our extra total,
                heldExtraAmmo -= emptyammo;
                //set active ammo to max capacity
                activeAmmo = maxAmmo;
            }
            Debug.Log("Reloaded " + gameObject.name);
        }
        else
        {
            Debug.Log("No extra ammo! go pick some up!");
        }
    }
    /*void Fire2()
    {
        //runs this code for how many times the weapon is supposed to fire
        for (int i = 0; i < instantiateHowMany; i++)
        {
            //spawn bullet
            //GameObject bullet = Instantiate(bulletPrefab, newSpawn.position, Quaternion.identity, player);
            GameObject newBullet = Instantiate(bulletPrefab, newSpawn.position, this.transform.rotation);
            //shoot bullet
            newBullet.GetComponent<Rigidbody>().AddForce(newSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
            //destroy bullet after some time (after 3 seconds)
            canFire = false;
            Destroy(newBullet, 3f);
            //bullet cooldown
        }
    }*/
}
