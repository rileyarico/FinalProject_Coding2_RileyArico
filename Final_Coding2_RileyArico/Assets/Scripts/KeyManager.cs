using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    [Header("UI")]
    public Image keyImagesPrefab;
    public GameObject keyContainerUI;
    [HideInInspector] public int heldKeys = 0;
    private List<Image> keyImgs = new List<Image>();

    //called by KeyPickUp
    //instantiate key image & add to list
    public void PickedUpKey()
    {
        Image addThis = Instantiate(keyImagesPrefab, keyContainerUI.transform); //add as a child of keycontainer
        keyImgs.Add(addThis);
    }

    public void UseKey()
    {
        //remove this from list
        heldKeys--;
        Destroy(keyImgs[0].gameObject);
        keyImgs.RemoveAt(0);
        //destroy also
    }

}
