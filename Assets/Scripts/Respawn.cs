using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnController : MonoBehaviour
{
    public Vector2 originalPos;

    private void Start()
    {
        originalPos = gameObject.transform.position;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            gameObject.transform.position = originalPos;
        }
    }
}