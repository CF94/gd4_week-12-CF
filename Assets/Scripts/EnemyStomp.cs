using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weak Point")
        {
            Destroy(collision.gameObject);
        }
    }
}