using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public string ammoForName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Picked up " + ammoForName + "ammo");

    }
}
