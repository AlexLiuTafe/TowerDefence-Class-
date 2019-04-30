using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{

    public bool isAvailable = true; //Can a tower be place here?
    public Transform pivotPoint; // Where to place the tower

    /// Returns the point attached to the tile (if any)
    ///<returns>Returns placeable posiiton if no pivot is made</returns>


    public Vector3 GetPivotPoint()
    {
        //if there is no pivot point added to placeable
        if(pivotPoint == null)
        {
            // Return placeable's position
            return transform.position;

        }
        //Return pivot point otherwise
        return pivotPoint.position;
    }
}
