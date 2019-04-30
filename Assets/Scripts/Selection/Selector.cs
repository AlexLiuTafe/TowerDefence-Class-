using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public Transform towerParent;
    public int currentTower = 0;
    public GameObject[] towers;
    public GameObject[] holograms;

    
    //testing
    private Vector3 placeablePoint;

    private void OnDrawGizmos()
    {
        //Create a ray
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(mouseRay.origin,mouseRay.origin + mouseRay.direction*1000f);
        //Draw a sphere on the point
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(placeablePoint, .5f);


    }
    //Disables the GameObjects of all referenced holograms
     void DisableAllHolograms()
    {
      //Loop through all Holograms
      foreach (var holo in holograms)
        {
            //Disable hologram
            holo.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Disable all holograms at the start of each frame
        DisableAllHolograms();

        //Creates a ray
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Performing raycast here
        if(Physics.Raycast(mouseRay,out hit))
        {
            //try to get placeable from thing we hit
            Placeable p = hit.collider.GetComponent<Placeable>();
            //is it placeable?...AND placeable is Available
            if (p && p.isAvailable)
            {
                //Set placeable point for testing
                //placeablePoint = p.transform.position;
                //>>HOVER MECHANIC<<
                //Get hologram of current tower
                GameObject hologram = holograms[currentTower];
                //Activate hologram
                hologram.SetActive(true);
                //position hologram to tile
                hologram.transform.position = p.GetPivotPoint();

                //>>PLACEMENT MECHANIC<<
                //if left mouse is down
                if (Input.GetMouseButtonDown(0))
                {
                    //get the current tower prefab
                    GameObject towerPrefab = towers[currentTower];
                    //spawn a new tower
                    GameObject tower = Instantiate(towerPrefab, towerParent);
                    //position new tower to tile
                    tower.transform.position = p.GetPivotPoint();
                    //tile is no longer placeable
                    p.isAvailable = false;

                }
            }
        }
	}
     public void SelectTower(int tower)
    {
        // is tower within range of array (towers)
        if (tower >= 0 && tower < towers.Length)
        {
            //set currentTower to tower
            currentTower = tower;


        }


    }
}
