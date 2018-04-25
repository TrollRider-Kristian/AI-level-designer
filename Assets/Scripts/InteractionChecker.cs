using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChecker : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Collect"))
                gameObject.SetActive(false);

            if (gameObject.CompareTag("Avoid"))
                collision.gameObject.transform.position = new Vector3(0.22f, 0, 0);

            if (gameObject.CompareTag("Defeat"))
            {
                SpriteRenderer playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
                bool feet_above_me = collision.gameObject.transform.position.y - playerSprite.bounds.extents.y - gameObject.transform.position.y > 0;
                if (feet_above_me)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    collision.gameObject.transform.position = new Vector3(0.22f, 0, 0);
                }
            }

            if (gameObject.CompareTag("Victory"))
            {
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                Debug.Log("YOU WIN!!");
            }
        }
    }
}
