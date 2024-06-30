using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class CursorLocker : MonoBehaviour {
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
