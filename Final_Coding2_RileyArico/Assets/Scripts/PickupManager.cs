using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickupManager : MonoBehaviour
{
    //list to hold all pickup instances that the player has picked up
    public List<GameObject> pickupList = new List<GameObject>();

    int currentPickupIndex = -1; //start with no weapon, 0 is the first slot of an array

    void Start()
    {
        
    }

    void Update()
    {
            //key to switch weapons
            //weaponslist.count ensures that there is at least one weapon in the list before trying to switch weapons
            if(Input.GetKeyUp(KeyCode.Q) && pickupList.Count > 0)
            {
                int newPickup = (currentPickupIndex + 1) % pickupList.Count; // % this resets the count if you have reached the end of the list
                SwitchPickup(newPickup);
            }
            
    }

    public void AddPickUp(GameObject pickupPrefab)
    {
        //add instantiated pickup to our list
        pickupList.Add(pickupPrefab);
        //prevent multiple active pickups at once
        pickupPrefab.SetActive(false); //starts with it disabled

        //if it is first on the list, activate it
        if(pickupList.Count == 1)
        {
            //call switch pickup to switch our weapon
            //switching it to the first weapon on our list
            SwitchPickup(0);
        }
    }

    void SwitchPickup(int index)
    {
        //deactivate the current active weapon if there is one
        if (currentPickupIndex != -1)
        {
            //ensure when switching weapons, that the previous one is turned off
            pickupList[currentPickupIndex].SetActive(false);
        }

        //set the new pickup as active and update the current index
        currentPickupIndex = index;
        pickupList[currentPickupIndex].SetActive(true);
    }
}
