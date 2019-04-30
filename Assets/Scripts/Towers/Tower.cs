using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 10; // damage of the tower
    public float attackRate = 1f; // how fast the tower attacks
    public float attackRange = 2f; // how far the tower attacks

    protected Enemy currentEnemy; // Current target to shoot at

    private float attackTimer = 0f; // Time elapsed for attacking

    void OnDrawGizmosSelected()
    {
        //Draw the attack spehere around tower
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
    public virtual void Aim (Enemy e)
    {
        print("I have a eye on you'" + e.name + "'");
    }
    public virtual void Attack (Enemy e)
    {
        print("I shoot you'" + e.name+"'");
    }
    protected virtual void DetectEnemy()
    {
        //Reset current enemy
        currentEnemy = null;
        //Get hit colliders from overlapSphere
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        //loop through all hit colliders
        foreach (var hit in hits)
        {
            // if we hit an enemy
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                // set currentenemy to enemy
                currentEnemy = enemy;
                
            }
        }
    }
    //protected -  accessible to Cannon/other Tower classes(Derivatives)
    //Virtual - Able to change what this function does in derived classes
    protected virtual void Update()
    {
        //Detect enemies before performing attack logic
        DetectEnemy();
        //Count up the timer
        attackTimer += Time.deltaTime;
        //If there is a current enemy
        if (currentEnemy)
        {
            //Aim at the enemy
            Aim(currentEnemy);
            //Is attack timer ready
            if (attackTimer >= attackRate)
            {
                //Attack the enemy!
                Attack(currentEnemy);
                //Reset the timer
                attackTimer = 0;
            }   
        }
    }
}
