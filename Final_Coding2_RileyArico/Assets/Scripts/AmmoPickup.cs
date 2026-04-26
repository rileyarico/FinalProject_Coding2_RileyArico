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
        weaponManager.AddAmmoTo(ammoType.forWhichWeapon, ammoType.amount);
        Debug.Log("Picked up " + ammoType.ammoName);
    }
}
