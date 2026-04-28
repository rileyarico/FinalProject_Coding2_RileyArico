using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Data")]
    public GameObject bulletPrefab;
    public float bulletLifeTime = 5f;
    public float bulletVelocity = 20f;

    //ammo management
    public string weaponName;
    public int maxAmmo = 5;
    [HideInInspector] public int activeAmmo;
    [HideInInspector] public int heldExtraAmmo = 0;

    //cooldown stuff
    public float fireCooldown;
    private float timer = 0;
    private bool canFire = true;

    private void Start()
    {
        activeAmmo = maxAmmo;
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
        //the direction we want the bullet to go in
        Vector3 direction = -transform.right; //Oh my god idk why this works but doing "-" makes it rotate -90*
        //we want to take the position of our weapon & add transform.up and then offset it so it is in front.
        Vector3 bulletSpawnPos = transform.position + transform.up * 0.5f;

        //Quaternion rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        //spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, this.transform.rotation);
        //shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletVelocity, ForceMode.Impulse);
        Debug.Log("Spawned Bullet");
        //destroy bullet after some time (after 3 seconds)
        Destroy(bullet, bulletLifeTime);

    }

    private void Reload()
    {
        //idk if we will use this
        if (heldExtraAmmo > 0)
        { 
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
