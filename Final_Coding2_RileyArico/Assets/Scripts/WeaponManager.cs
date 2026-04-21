using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //grab weapon socket so we can switch active prefabs
    public Transform weaponSocket;
    //lists of total weapons, we will keep all others .setInactive but keep main active
    public List<GameObject> weaponList = new List<GameObject>();
    //keep track of current index so we can set it false when switching
    private int weaponIndex;
    public HotbarManager hotbarManager;

    private void Update()
    {
        //checks if there is a weapon change to another slot
        CheckWeaponSwitch();
    }

    private void CheckWeaponSwitch()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && weaponList.Count >= 1)
        {
            weaponList[weaponIndex].gameObject.SetActive(false);
            weaponList[0].gameObject.SetActive(true);
            weaponIndex = 0;
            Debug.Log("Switched to weapon " + (weaponIndex + 1));
            hotbarManager.HighlightThis(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && weaponList.Count >= 2)
        {
            weaponList[weaponIndex].gameObject.SetActive(false);
            weaponList[1].gameObject.SetActive(true);
            weaponIndex = 1;
            Debug.Log("Switched to weapon " + (weaponIndex + 1));
            hotbarManager.HighlightThis(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) && weaponList.Count >= 3)
        {
            weaponList[weaponIndex].gameObject.SetActive(false);
            weaponList[2].gameObject.SetActive(true);
            weaponIndex = 2;
            Debug.Log("Switched to weapon " + (weaponIndex + 1));
            hotbarManager.HighlightThis(2);
        }
    }
}
