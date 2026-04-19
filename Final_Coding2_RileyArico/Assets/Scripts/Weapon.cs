using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector3 bulletSpawnPos;
    public int instantiateHowMany = 1;

    public float bulletLifeTime;

    public Transform newSpawn;

    public float fireCooldown;
    private float timer = 0;
    private bool canFire;

    public float bulletVelocity = 20f;

    void Update()
    {
        //subtract time elapsed from timer
        timer -= Time.deltaTime;

        //checks if cooldown is over
        if (timer < 0)
        {
            //makes weapon able to fire
            canFire = true;

            //resets the cooldown
            timer = fireCooldown;
        }

        //so it doesnt shoot before we pick it up, it has to be active
        //when we click the left mouse button & we have the weapon & its active
        if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && canFire)
        {
            //perform fire
            Fire2();

        }
    }

    void Fire()
    {
        //the direction we want the bullet to go in
        Vector3 direction = transform.up;
        //we want to take the position of our weapon & add transform.up and then offset it so it is in front.
        bulletSpawnPos = transform.position + transform.up * 0.5f;

        //spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos, Quaternion.identity);
        //shoot bullet
        //bullet.GetComponent<Rigidbody>().AddForce(direction * bulletVelocity, ForceMode.Impulse);
        //destroy bullet after some time (after 3 seconds)
        Destroy(bullet, bulletLifeTime);

    }

    void Fire2()
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
    }
}
