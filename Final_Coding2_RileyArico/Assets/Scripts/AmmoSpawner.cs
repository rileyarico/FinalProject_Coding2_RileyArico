using System.Collections;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPickupPrefab;
    private bool corouActive = false;
    public float cooldownTime = 15f;
    private Coroutine corou;
    private void Start()
    {

    }

    private void Update()
    {
        if(gameObject.transform.childCount == 0 && corouActive == false)
        {
            corou = StartCoroutine(StartCountdown(cooldownTime));
            corouActive = true;
        }
    }

    private IEnumerator StartCountdown(float time)
    {
        float remainingTime = time;
        
        //until timer reaches 0
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f); //wait 1 sec then subtract
            remainingTime -= 1f;
        }

        //when timer finished
        Debug.Log(ammoPickupPrefab.name + " replenished.");
        OnTimerComplete();
    }
    private void OnTimerComplete()
    {
        Instantiate(ammoPickupPrefab, gameObject.transform);
        //stop coroutine
        StopCoroutine(corou);
        corouActive = false;
    }

}
