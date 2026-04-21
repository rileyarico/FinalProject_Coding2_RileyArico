using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    //grab inventory slots
    public List<Image> inventorySlots = new List<Image>();
    //public Color highlightColor;
    private Image currentlyHighlighted;

    public void HighlightThis(int index)
    {
        if(currentlyHighlighted != null)
        {
            currentlyHighlighted.color = Color.white;
        }
        currentlyHighlighted = inventorySlots[index];
        currentlyHighlighted.color = Color.red;

    }
}
