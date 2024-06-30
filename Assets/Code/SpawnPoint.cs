using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    private static SpawnPoint instance;

    private void Awake() 
    {
        instance = this;
        player = Instantiate(playerPrefab);
        ResetPlayerPosition();
        player.name = "Player";
        gameObject.SetActive(false);
    }

    public static void ResetPlayerPosition()
    {
        if (instance == null)
        {
            Debug.LogError("No spawn point");
            return;
        }

        instance.player.transform.position = instance.transform.position;
        instance.player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
