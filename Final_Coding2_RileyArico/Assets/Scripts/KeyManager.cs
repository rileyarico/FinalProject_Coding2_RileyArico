using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public Image keyImagesPrefab;
    private int heldKeys = 0;
    private List<Image> keyImgs = new List<Image>();

    //called by KeyPickUp
    //instantiate key image & add to list
    public void PickedUpKey()
    {
        Image addThis = Instantiate(keyImagesPrefab); //add child
        keyImgs.Add(addThis);
    }

    public void UseKey()
    {
        //remove this from list

        //destroy also
    }

}
