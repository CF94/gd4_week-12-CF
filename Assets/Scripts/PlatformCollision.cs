using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag.Equals(playerTag))
        {
            gameObject.transform.parent = platform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag.Equals(playerTag))
        {
            gameObject.transform.parent = null;
        }
    }
}
