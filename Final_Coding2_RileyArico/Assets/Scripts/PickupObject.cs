using UnityEngine;

public class Pick : MonoBehaviour
{
    //prefab that will be instantantiated when picked up
    public GameObject pickupObject;

    //the transform of the socket to which the item will be parented to the player
    public Transform pickupSocket;

    //making a reference to our manager so we can later on call a function from it
    PickupManager pickupManager;

    private Vector3 pointA;
    private Vector3 pointB;

    private float spin;
    private float rotationSpeed = 0.5f;

    void Start()
    {
        //rotate sideways
        spin = 0;
        pickupManager = Object.FindFirstObjectByType<PickupManager>();
        transform.Rotate(0, 310, 0, 0);

        //move up & down
        pointA = transform.position;
        pointB = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
    }

    private void Update()
    {
        MoveObject(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player is walking into our trigger
        if (other.CompareTag("Player"))
        {
            //instantiate and parent it to the socket
            //inside instantiate(Gameobject, where we want it to spawn, no rotation, and what to parent it to)
            GameObject newPickUp = Instantiate(pickupObject, pickupSocket.position, Quaternion.identity, pickupSocket);

            //reset local position and rotation to ensure it first correctly into the socket
            newPickUp.transform.localPosition = Vector3.zero;
            newPickUp.transform.localRotation = Quaternion.identity;

            //add it to our list
            pickupManager.AddPickUp(newPickUp);

            //Destroy the representation of the weapon 
            Destroy(gameObject);

            Debug.Log("Picked Up");


        }
    }

    private void MoveObject()
    {
        Vector3 yes = new Vector3(0, 0.05f, 0);
        //Debug.Log("rotation of object is " + this.transform.rotation);
        transform.Rotate(yes); // adds value of yes to the rotation

        float time = Mathf.PingPong(Time.time * 0.9f, 1f);
        transform.position = Vector3.Lerp(pointA, pointB, time);

    }
}
