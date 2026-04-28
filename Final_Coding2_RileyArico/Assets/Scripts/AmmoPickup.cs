using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AmmoTypesSO ammoType;
    private WeaponManager weaponManager;
    private void Start()
    {
        weaponManager = FindFirstObjectByType<WeaponManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FinalPlayer>() == null) return;

        if (weaponManager.DoesWeaponExist(ammoType.forWhichWeapon))
        {
            Debug.Log("Recieved true for weapon existing.");
            weaponManager.AddAmmoTo(ammoType.forWhichWeapon, ammoType.amount);
            Debug.Log("Picked up " + ammoType.ammoName);
            Destroy(gameObject);
            return;
        }
        Debug.Log("Couldn't find which ammo this belongs to. Ammo is for " + ammoType.forWhichWeapon);
    }
}
