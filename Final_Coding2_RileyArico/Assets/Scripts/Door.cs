using UnityEngine;

public class Door : MonoBehaviour
{
    private KeyManager keyManager;

    private void Start()
    {
        keyManager = FindFirstObjectByType<KeyManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //check player to see if they have a key
        if (other.gameObject.GetComponent<FinalPlayer>() != null)
        {
            Debug.Log("Player entered trigger");
            if(keyManager.heldKeys >= 1)
            {
                keyManager.UseKey();

                //set parent of this trigger false (actual door)
                transform.parent.gameObject.SetActive(false);
                Debug.Log("Used key on " + gameObject.name);
            }
        }
        else return;
    }
}
