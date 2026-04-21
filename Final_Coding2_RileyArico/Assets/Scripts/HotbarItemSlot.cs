using UnityEngine;

public class HotbarItemSlot : MonoBehaviour
{
    [HideInInspector] public bool isHolding;

    private void Start()
    {
        isHolding = false;
    }

}
