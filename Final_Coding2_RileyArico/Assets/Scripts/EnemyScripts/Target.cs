using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector] public float currentHealth;
    public Image healthBar;
    public GameObject setActiveOnFirstDeath;
    private bool diedOnce = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        if(currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (diedOnce == false)
        {
            setActiveOnFirstDeath.SetActive(true);
        }
        diedOnce = true;
        currentHealth = maxHealth;
    }
}
