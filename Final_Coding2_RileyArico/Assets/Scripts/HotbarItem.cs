using UnityEngine;

public class HotbarItem : MonoBehaviour
{
    [HideInInspector] bool isHolding = false;

    private void Update()
    {
        if(transform.childCount == 0)
        {
            isHolding = false;
        }
        else
        {
            isHolding = true;
        }
    }

}
