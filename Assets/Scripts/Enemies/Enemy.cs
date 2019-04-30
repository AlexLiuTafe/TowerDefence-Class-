using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Attribute
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public Transform target;

    private NavMeshAgent agent;
    private int health = 0;

    void Start()
    {
        //Set Max health at start
        health = maxHealth;
        //Get NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        //Follow destination
        agent.SetDestination(target.position);

    }
    public void TakeDamage(int damage)
    {
        //Reduce health with damage
        health -= damage;
        //if health reaches zero
        if(health <=0)
        {
            //Destroy the game object
            Destroy(gameObject);


        }
    }
}
