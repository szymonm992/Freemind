using UnityEngine;

public class RespawnOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnPoint.ResetPlayerPosition();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnPoint.ResetPlayerPosition();
        }    
    }
}
