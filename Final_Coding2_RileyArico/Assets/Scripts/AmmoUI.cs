using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    //this will be attatched to a panel,
    //has text for total extra ammo,
    //as well as another nested panel w/ a horizontal layout group,
    //which holds images of bullets, amounts corresponding with the current
    //weapon's currently active ammo.
    public TextMeshProUGUI extraHeldAmmoTxt;
    public GameObject activeAmmoPanel;
    public Image ammoImgPrefab;
    private Weapon activeWeapon;

    private List<GameObject> ammoIcons = new List<GameObject>();

    private void Update()
    {
        if(activeWeapon != null)
        {
            //if active is greater, we need to populate
            if(activeWeapon.activeAmmo > ammoIcons.Count)
            {
                PopulateAmmo();
            }
            //if active is less, we need to remove
            if(activeWeapon.activeAmmo < ammoIcons.Count)
            {
                RemoveAmmo();
            }
            extraHeldAmmoTxt.text = activeWeapon.heldExtraAmmo + "x";
        }
    }

    public void SwitchWeaponAmmo(Weapon weapon)
    {
        activeWeapon = weapon;
    }

    private void RemoveAmmo()
    {
        Destroy(ammoIcons[0]);
        ammoIcons.RemoveAt(0);
    }
    private void PopulateAmmo()
    {
        ammoIcons.Add(Instantiate(ammoImgPrefab.gameObject, activeAmmoPanel.transform));
    }
}
