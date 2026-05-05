using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    public float lifeDuration = 1.0f;

    private void Update()
    {
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0.0f )
        {
            Destroy(gameObject);
        }
    }
}
