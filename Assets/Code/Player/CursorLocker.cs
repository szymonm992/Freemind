using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class CursorLocker : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
