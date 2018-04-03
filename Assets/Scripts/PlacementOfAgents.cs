using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Use a Bezier Curve, https://en.wikipedia.org/wiki/B%C3%A9zier_curve 
// to place enemy agents in the game.  Randomly determine control points at locations
// START, (3, random(-5, 5)), (6, random(-5, 5)), (9, random(-2.5, 2.5)) <- GOAL.
// 2. Sample random points wrt time between 0 and 1 and place the agents along the curve.
// 3. Finally, iterate one step of the diamond-square algorithm on each location
// to generate new landscapes.

public class PlacementOfAgents : MonoBehaviour
{
    public GameObject[] moving_agents;
    public GameObject[] landscapes;

    //control points:
    float x1, y1, x2, y2, x3, y3, x4, y4;

	// Use this for initialization
	void Start ()
    {
        x1 = 0.0f;
        y1 = 0.0f;
        x2 = 15.0f;
        y2 = Random.Range(-15.0f, 15.0f);
        x3 = 30.0f;
        y3 = Random.Range(-15.0f, 15.0f);
        x4 = 45.0f;
        y4 = Random.Range(-7.2f, 7.2f);
        placeAgents(Random.Range(5, 10));
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void placeAgents(int numAgents)
    {
        //sample numbers between 0 and 1 to place agents on the curve
        GameObject monster;
        GameObject landscape;
        SpriteRenderer land_under_me;
        SpriteRenderer widthscape;
        for (int i = 0; i < numAgents; i++) {
            float t = Random.Range(0.0f, 1.0f);
            float xLoc = Mathf.Pow(1 - t, 3) * x1 + 3 * Mathf.Pow(1 - t, 2) * x2 * t + (1 - t) * x3 * Mathf.Pow(t, 2) + x4 * Mathf.Pow(t, 3);
            float yLoc = Mathf.Pow(1 - t, 3) * y1 + 3 * Mathf.Pow(1 - t, 2) * y2 * t + (1 - t) * y3 * Mathf.Pow(t, 2) + y4 * Mathf.Pow(t, 3);
            monster = Instantiate(moving_agents[Random.Range(0, 2)], transform.position, transform.rotation) as GameObject;
            monster.transform.position = new Vector3(xLoc, yLoc, 0);
            landscape = Instantiate(landscapes[0], transform.position, transform.rotation) as GameObject;
            land_under_me = monster.GetComponent<SpriteRenderer>();
            widthscape = landscape.GetComponent<SpriteRenderer>();
            landscape.transform.position = monster.transform.position - new Vector3(0, land_under_me.bounds.extents.y + 2 * widthscape.bounds.extents.y, 0); 
        }
    }
}
