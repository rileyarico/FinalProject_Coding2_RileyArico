using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NPCData start;
    void Start()
    {
        //FinalPlayer player = FindAnyObjectByType<FinalPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FinalPlayer>() != null)
        {
            Debug.Log("Player entered trigger");
            other.GetComponent<FinalPlayer>().RequestDialogue(start);
        }
    }
}
