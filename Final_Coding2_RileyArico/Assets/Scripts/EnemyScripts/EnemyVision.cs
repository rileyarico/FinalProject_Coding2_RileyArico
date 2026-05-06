using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float detectionRange = 10f;
    public float detectionAngle = 45f;
    bool isInAngle, isInRange, isNotHidden;

    private GameObject player;

    private void Start()
    {
        player = FindFirstObjectByType<FinalPlayer>().gameObject;

    }

    private void Update()
    {
        isInAngle = false;
        isInRange = false;
        isNotHidden = false;
        if(Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            isInRange = true;
        }


        RaycastHit hit;
        if(Physics.Raycast(transform.position,(player.transform.position - transform.position), out hit, Mathf.Infinity))
        {
            if(hit.transform == player.transform)
            {
                isNotHidden = true;
            }
        }


        Vector3 side1 = player.transform.position - transform.position;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);

        if(angle < detectionAngle && angle > -1 * detectionAngle)
        {
            isInAngle = true;
        }

    }

    public bool IsDetected()
    {
        bool allTrue = false;
        if(isInAngle && isInRange && isNotHidden)
        {
            allTrue = true;
        }
        return allTrue;
    }

}
