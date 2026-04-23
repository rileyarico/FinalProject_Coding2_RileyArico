using UnityEngine;

public class Bullet : MonoBehaviour  
{
    public float damage = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {

        //destroy the parent b/c this obj is held by one
        //Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
