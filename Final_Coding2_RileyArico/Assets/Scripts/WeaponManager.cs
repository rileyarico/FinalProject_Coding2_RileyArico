using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void AddWeapon(GameObject weapon, Image inventoryImage)
    {
        //finds a open slot
        //add weapon to list
        //give image to open inventory slot
        for(int i = 0; i <= hotbarManager.inventorySlots.Count; i++)
        {
            //if this hot bar item's slot is false
            HotbarItemSlot thisItemSlot = hotbarManager.inventorySlots[i].GetComponent<HotbarItemSlot>();
            if (thisItemSlot.isHolding == false)
            {
                //add weapon to this list
                weaponList.Add(Instantiate(weapon, weaponSocket.transform));
                //instantiate new Image as a child
                Instantiate(inventoryImage, hotbarManager.inventorySlots[i].transform);
                //set isHolding true in HotbarItem script
                hotbarManager.inventorySlots[i].GetComponent<HotbarItemSlot>().isHolding = true;
                if(weaponList.Count == 1)
                {
                    hotbarManager.HighlightThis(0);
                }
                else if (weaponList.Count >= 2)
                {
                    //grab last item of WeaponList
                    //set inactive
                    weaponList[weaponList.Count - 1].gameObject.SetActive(false);
                }

                    //instant

                    i = 10;
                return;

                //adding on both these lists should be okay, we aren't removing
                //so indexes of images & gameobjects should be the same
            }
        }
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
