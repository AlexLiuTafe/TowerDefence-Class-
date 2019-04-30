using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Cannon : Tower
{
    public Transform orb; //reference to orb for rotation
    public float lineDelay = .2f; // how long the line appears to be
    public LineRenderer line; // reference to line renderer
    void Reset()
    {
        //Gets a reference to line renderer
        line = GetComponent<LineRenderer>();
    }
    IEnumerator ShowLine(float delay)
    {
        line.enabled = true;
        yield return new WaitForSeconds(delay);
        line.enabled = false;

    }
    //Rotates orb to Enemy
    public override void Aim (Enemy e)
    {
        //get orb to look at enemy
        line.SetPosition(0, orb.position);
        line.SetPosition(1, e.transform.position);
    }

    //Deals damage to enemy and show line
    public override void Attack(Enemy e)
    {
        //show the line
        StartCoroutine(ShowLine(lineDelay));
        //Deal Damage
        e.TakeDamage(damage);

    }

    

}
