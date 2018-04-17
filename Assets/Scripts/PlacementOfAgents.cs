using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use three Bezier Curves, https://en.wikipedia.org/wiki/B%C3%A9zier_curve 
// to place enemy agents in the game.  Randomly determine control points at locations
// START -> (0, 0), (3, random(-5, 5)), (6, random(-5, 5)), (9, random(-2.5, 2.5)) <- GOAL for each curve.
// 2. Sample random points wrt time between 0 and 1 and place the agents along the first curve.
// 3. If the random value t is less than 0.4 or greater than 0.6, add an agent along the second curve at 1 - t and a third on the third curve at random.
//  Else add two agents on the second and third curves, one at t + t/2 and one at t - t/2.

// 4. Which agent do we add?  We add up the sum of the areas of the image sizes for each agent so far, as well as the y location and determine a probability from that.
// Make a dice roll to determine which one gets put there.

public class PlacementOfAgents : MonoBehaviour
{
    public GameObject[] moving_agents;
    public GameObject[] landscapes;
    public GameObject flag;

    //control points first Bezier Curve:
    float x1, y1, x2, y2, x3, y3, x4, y4;
    //control points second Bezier Curve:
    float a1, b1, a2, b2, a3, b3, a4, b4;
    //control points third Bezier Curve:
    float c1, d1, c2, d2, c3, d3, c4, d4;

	// Use this for initialization
	void Start ()
    {
        //first
        x1 = 0.0f;
        y1 = 0.0f;
        x2 = 15.0f;
        y2 = Random.Range(-15.0f, 15.0f);
        x3 = 30.0f;
        y3 = Random.Range(-15.0f, 15.0f);
        x4 = 45.0f;
        y4 = Random.Range(-7.2f, 7.2f);
        //second
        a1 = 0.0f;
        b1 = 0.0f;
        a2 = 15.0f;
        b2 = Random.Range(-15.0f, 15.0f);
        a3 = 30.0f;
        b3 = Random.Range(-15.0f, 15.0f);
        a4 = 45.0f;
        b4 = Random.Range(-7.2f, 7.2f);
        //third
        c1 = 0.0f;
        d1 = 0.0f;
        c2 = 15.0f;
        d2 = Random.Range(-15.0f, 15.0f);
        c3 = 30.0f;
        d3 = Random.Range(-15.0f, 15.0f);
        c4 = 45.0f;
        d4 = Random.Range(-7.2f, 7.2f);

        placeAgents(Random.Range(2, 4));
        place_goalpost();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void placeAgents(int agentsPerCurve)
    {
        //sample numbers between 0 and 1 to place agents on the curve
        for (int i = 0; i < agentsPerCurve; i++)
        {
            float t = Random.Range(0.1f, 1.0f);
            float u, v;

            if (t < 0.4 || t > 0.6)
            {
                u = 1.0f - t;
                v = Random.Range(0.3f, 0.7f);
            }
            else
            {
                int coin_flip = Random.Range(0, 2);
                if (coin_flip == 0)
                {
                    u = t + (t / 2);
                    v = t - (t / 2);
                }
                else
                {
                    u = t - (t / 2);
                    v = t + (t / 2);
                }
            }

            float xLoc = BezierHelper(x1, x2, x3, x4, t);
            float yLoc = BezierHelper(y1, y2, y3, y4, t);
            float aLoc = BezierHelper(a1, a2, a3, a4, u);
            float bLoc = BezierHelper(b1, b2, b3, b4, u);
            float cLoc = BezierHelper(c1, c2, c3, c4, v);
            float dLoc = BezierHelper(d1, d2, d3, d4, v);

            placeAgentsHelper(xLoc, yLoc);
            placeAgentsHelper(aLoc, bLoc);
            placeAgentsHelper(cLoc, dLoc);
        }
    }

    float BezierHelper(float x1, float x2, float x3, float x4, float t)
    {
        return Mathf.Pow(1 - t, 3) * x1 + 3 * Mathf.Pow(1 - t, 2) * x2 * t + (1 - t) * x3 * Mathf.Pow(t, 2) + x4 * Mathf.Pow(t, 3);
    }

    void placeAgentsHelper(float xLoc, float yLoc)
    {
        GameObject monster, landscape;
        SpriteRenderer land_under_me, widthscape;
        monster = Instantiate(moving_agents[Random.Range(0, moving_agents.Length)], transform.position, transform.rotation) as GameObject;
        monster.transform.position = new Vector3(xLoc, yLoc, 0);
        landscape = Instantiate(landscapes[0], transform.position, transform.rotation) as GameObject;
        land_under_me = monster.GetComponent<SpriteRenderer>();
        widthscape = landscape.GetComponent<SpriteRenderer>();
        landscape.transform.position = monster.transform.position - new Vector3(0, land_under_me.bounds.extents.y + 2 * widthscape.bounds.extents.y, 0);
    }

    void place_goalpost()
    {
        GameObject landscape;
        SpriteRenderer land_under_me, widthscape;
        flag = Instantiate(flag, transform.position, transform.rotation) as GameObject;
        flag.transform.position = new Vector3(x4, y4, 0);
        landscape = Instantiate(landscapes[0], transform.position, transform.rotation) as GameObject;
        land_under_me = flag.GetComponent<SpriteRenderer>();
        widthscape = landscape.GetComponent<SpriteRenderer>();
        landscape.transform.position = flag.transform.position - new Vector3(0, land_under_me.bounds.extents.y + 2 * widthscape.bounds.extents.y, 0);
    }
}
