using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        else
        {
            Vector3 playerPosition = GameManager.instance.playerInput.transform.position;
            Vector3 myPosition = transform.position;

            float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
            float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

            Vector3 playerDirection = GameManager.instance.playerInput.getInputVector;
            float dirX = playerDirection.x < 0 ? -1 : 1;
            float dirY = playerDirection.y < 0 ? -1 : 1;

            if(transform.tag == "Enemy")
            {
                transform.Translate(playerDirection * 20 + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f));
            }
        }

    }
}
