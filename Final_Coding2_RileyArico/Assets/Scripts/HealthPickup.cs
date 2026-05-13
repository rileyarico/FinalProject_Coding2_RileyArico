using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float restoreAmt;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Health pickup entered");
        if (other.gameObject.GetComponent<FinalPlayer>() != null)
        {
            FinalPlayer player = other.gameObject.GetComponent<FinalPlayer>();
            Debug.Log("Ran into player");
            float maxhealth = player.maxHealth;
            float currenthealth = player.currentHealth;

            player.currentHealth += restoreAmt;

            if (player.currentHealth > maxhealth)
            {
                player.currentHealth = maxhealth;
            }
            Destroy(gameObject);
        }
        return;
    }

}
