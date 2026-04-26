using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyManager keyManager;

    private void Start()
    {
        keyManager = FindFirstObjectByType<KeyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        keyManager.PickedUpKey();
        Destroy(gameObject);
    }
}
