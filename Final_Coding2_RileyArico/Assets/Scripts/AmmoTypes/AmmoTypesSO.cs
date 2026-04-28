using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/AmmoTypes")]
public class AmmoTypesSO : ScriptableObject
{
    public string ammoName;
    public string forWhichWeapon;
    public int amount;
}
