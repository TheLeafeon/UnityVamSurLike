using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileReposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        else
        {
            Vector3 playerPosition = GameManager.instance.playerInput.transform.position;
            Vector3 myPosition = transform.position;

            float diffX = Mathf.Abs(playerPosition.x - myPosition.x) ;
            float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

            Vector3 playerDirection  = GameManager.instance.playerInput.getInputVector;
            float dirX = playerDirection.x < 0 ? -1 : 1;
            float dirY = playerDirection.y < 0 ? -1 : 1;

            switch(transform.tag)
            {
                case "Ground":
                    if(diffX > diffY)
                    {
                        transform.Translate(Vector3.right * dirX * 40);
                    }
                    else if(diffX < diffY)
                    {
                        transform.Translate(Vector3.up * dirY * 40);
                    }
                    break;
            }
        }
    }
}
