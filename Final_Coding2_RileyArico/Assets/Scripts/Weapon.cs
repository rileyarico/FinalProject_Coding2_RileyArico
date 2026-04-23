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
    public int ammoTotal = 5;
    private int heldAmmo;

    //cooldown stuff
    public float fireCooldown;
    private float timer = 0;
    private bool canFire = true;


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
        if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && canFire /*&& heldAmmo > 0*/)
        {
            Debug.Log("Fire Performed");
            //perform fire
            Fire();
            timer = fireCooldown;
            heldAmmo -= 1;
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
