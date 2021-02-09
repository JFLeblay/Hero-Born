using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // Public properties
    public Transform player;

    public Transform patrolRoute;
    public List<Transform> locations;

    // Private properties
    private int locationIndex = 0;
    private NavMeshAgent agent;

    // Getters/Setters
    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected -- attack!");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Player")
        {
            Debug.Log("Player out of range -- resuming patrol");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives--;
            Debug.Log("Critical hit!");
        }
    }

    private void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    private void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0) return;

        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }
}
