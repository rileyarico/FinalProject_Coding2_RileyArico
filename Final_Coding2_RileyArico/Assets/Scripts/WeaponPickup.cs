using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Image inventoryImage;
    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager = FindAnyObjectByType<WeaponManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FinalPlayer>() == null) return;
        //destroy this
        Destroy(this.gameObject);
        //send over prefab & image
        weaponManager.AddWeapon(weaponPrefab, inventoryImage);
        Debug.Log("Picked up " + gameObject.name);
    }
}
