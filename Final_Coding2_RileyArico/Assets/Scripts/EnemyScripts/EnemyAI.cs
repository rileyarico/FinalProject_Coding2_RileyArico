using System.IO;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    //define different states & switches between them
    public enum EnemyState { Idle, Patrol, Chase, Attack, Death }
    public EnemyState currentState;

    private Transform player;
    private NavMeshAgent agent;

    //patrol settings
    public Transform[] patrolPoints; //also called waypoints
    private int currentPatrolIndex;

    //enemy states loaded from json
    public string enemyType;
    private float maxHealth;
    private float currentHealth;
    private float speed;
    private float detectionRange = 10f;
    private float attackRange;
    private float attackCoolDown;
    private float currentAttackCooldown = 0;

    private float lastAttackTime;
    private int collisionCount = 0;

    //for visual health bar
    public Image healthBar;

    //vfx prefab
    public GameObject vFXSpritePrefab;
    
    //what enemy drops once killed
    public GameObject dropLoot;

    //testing smth
    float distanceToPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        LoadEnemyData(enemyType);
        currentHealth = maxHealth;
        
        currentState = EnemyState.Patrol; //start with patrolling
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Bullet>() != null)
        {
            collisionCount++;
            float dmg = other.gameObject.GetComponent<Bullet>().damage;
            if (collisionCount <= 1)
            {
                currentHealth -= dmg;
                Destroy(other.gameObject);
                collisionCount = 0;
            }
            Debug.Log("Dealt " + dmg + "to enemy");
            Debug.Log("Health is " + currentHealth);
        }
    }

    void Update()
    {
        //find and assign our player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        if(currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }

        if (currentHealth <= 0)
        {
            ChangeState(EnemyState.Death);
        }
        //switch statement determines what behaviors the enemies should perform based on its current state
        //switch statement checks current state of enemy & decides which behavior to execute
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehavior();
                //break make sure program doesnt check other cases once a match is found
                break;
            case EnemyState.Patrol:
                PatrolBehavior();
                if(distanceToPlayer <= detectionRange)
                {
                    ChangeState(EnemyState.Chase);
                }
                break;
            //moves toward player if close enough & switches to attack
            case EnemyState.Chase:
                ChaseBehavior();
                if(distanceToPlayer <= attackRange)
                {
                    ChangeState(EnemyState.Attack);
                }
                else if(distanceToPlayer > attackCoolDown)
                {
                    ChangeState(EnemyState.Patrol);
                }
                break;
            case EnemyState.Attack:
                AttackBehavior();
                if(distanceToPlayer <= attackRange)
                {
                    ChangeState(EnemyState.Patrol);
                }
                else if (distanceToPlayer <= detectionRange)
                {
                    ChangeState(EnemyState.Chase);
                }
                    break;
            case EnemyState.Death:
                if (dropLoot != null)
                {
                    GameObject drop = Instantiate(dropLoot, this.transform.position, dropLoot.transform.rotation);
                }

                Destroy(gameObject);
                break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }
    void IdleBehavior()
    {
        //you can add an animation here
    }
    void PatrolBehavior()
    {
        //ensures the enemy only switches patrol points after reaching the target
        //if enemy is close enough to patrol point, 0.5 moves to the next one
        if(!agent.pathPending && agent.remainingDistance < 3f)
        {
            //move to next patrol point
            MoveToNextPatrolPoint();
        }
    }

    void MoveToNextPatrolPoint()
    {
        //if we have no patrol points, exit function
        if(patrolPoints.Length == 0)
        {
            return;
        }

        //set destination moves to next patrol point
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        //update index
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
    void ChaseBehavior()
    {
        //set the destination to follow the player
        agent.SetDestination(player.position);
    }
    void AttackBehavior()
    {
        if (currentAttackCooldown <= 0)
        {
            Debug.Log("Dealt damage to Player");
            player.GetComponent<FinalPlayer>().currentHealth -= 2;
            currentAttackCooldown = attackCoolDown;
            Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.75f);
            GameObject vFX = Instantiate(vFXSpritePrefab, spawnPos, Quaternion.identity);
            vFX.transform.SetParent(this.transform, true);
        }
    }

    private void LoadEnemyData(string enemyName)
    {
        //path to our json file
        string path = Application.dataPath + "/Data/enemies.json";
        //if the file exists
        if(File.Exists(path))
        {
            Debug.Log("File path found"); //runs
            //read json file as text & store it into a string
            string json = File.ReadAllText(path);
            Debug.Log(json);
            //convert json to c# objects
            //stores the result
            EnemyDataBase enemyDB = JsonUtility.FromJson<EnemyDataBase>(json);
            Debug.Log("EnemyDataBase imported from JSON"); //runs

            //find correct enemy in the json file
            //loop through all of our enemies
            foreach(EnemyStats enemy in enemyDB.enemiesList) //issue is here
            {
                Debug.Log($"Checking enemy: {enemy.name}"); //not running

                //if the enemy that matches the requested name
                if(enemy.name == enemyName)
                {
                    Debug.Log($"Enemy: {enemy.name} found! Assigning Stats...");
                    maxHealth = enemy.health;
                    speed = enemy.speed; 
                    detectionRange = enemy.detectionRange;
                    attackRange = enemy.attackRange;
                    attackCoolDown = enemy.attackCoolDown;
                    Debug.Log($"Loaded enemy {enemy.name}");
                }
            }

        }
        else
        {
            Debug.Log("enemy json file not found");
        }

    }

}
