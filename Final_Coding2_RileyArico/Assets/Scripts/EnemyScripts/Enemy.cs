using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public Image healthBar;

    public GameObject dropLoot;

    public bool forward = true;
    public bool canMove = false;

    public float moveDuration = 3f;
    public float moveDistance = 0f;

    private Vector3 pointA;
    private Vector3 pointB;


    private void OnTriggerEnter(Collider other)
    {
        //if the enemy is hit by a bullet
        if (other.CompareTag("Bullet"))
        {
            //subtract 1 from enemy's health.
            currentHealth -= 1f;

            //destroy bullet if it makes contact with enemy
            Destroy(other.gameObject);
        }   
    }

    private void Start()
    {
        currentHealth = maxHealth;
        pointA = transform.position;
        pointB = new Vector3 (transform.position.x + moveDistance, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        //update the current health shown in game each frame
        //healthBar.fillAmount = currentHealth / maxHealth;

        MoveEnemy();
        if(currentHealth <= 0)
        {
            if (dropLoot != null)
            {
                GameObject drop = Instantiate(dropLoot, this.transform.position, dropLoot.transform.rotation);
            }
            Destroy(gameObject);
        }
        /*pointB -= Time.deltaTime;
        
        //checks if health is <0, if true destroy enemy
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if(forward && pointB <= 0)
        {
            pointB = 0;
            

            if (moveDistance != 0)
            {
                Debug.Log("move distance is not equal to 0");
                //bool canMove = true; 
                MoveOverTime(canMove);
                pointB = moveDuration;
                Debug.Log("called moveovertime function");
            }
        }



        //I dont know why this isn't working :(((

        //skips if move is not true, enemy is stationary
   
            //timer for 2 seconds
                /*if (forward)
                {
                    while (moveDuration > 0)
                    {//creates a new position that is the same as current but +0.1f to the X
                        Vector3 newPos = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z);
                        this.transform.position = newPos; //sets objects position to the new position
                        moveDuration -= 0.1f;
                    }
                    moveDuration = 3f; //resets countdown to switch
                    forward = false; //switches direction enemy is moving
                }
                else //same as above but subtracting 0.1f from x
                {
                    while (moveDuration > 0)
                    {
                        Vector3 newPos = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z);
                        this.transform.position = newPos;
                        moveDuration -= 0.1f;
                    }
                    moveDuration = 3f;
                    forward = true;
                }*/
    }
    private void MoveOverTime (bool yesMove)
    {
        //if the moveDistance is 0, return
        if (yesMove == false)
        { return; }
        else
        {
            //this is counting how much time has passed
            float timeElapsed = 0f;
            timeElapsed += Time.deltaTime;
            Debug.Log("Time elapsed" + timeElapsed);

            if (forward)
            {
                while (timeElapsed < moveDuration)
                {
                    Debug.Log("hit move duration");
                    //float ti = timeElapsed / moveDuration;
                    //Vector3 what = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + moveDistance);
                    //transform.position = Vector3.Lerp(startPos, what, ti);
                }
                //forward = false;
            }
            /*
            else
            {
                while (timeElapsed < moveDuration)
                {
                    float ti = timeElapsed / moveDuration;
                    Vector3 what = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - moveDistance);
                    transform.position = Vector3.Lerp(startPos, what, ti);
                    timeElapsed += Time.deltaTime;

                }
                forward = true;
            }*/
        }
    }

    private void MoveEnemy()
    {
        float time = Mathf.PingPong(Time.time * moveDuration, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
    }
}
