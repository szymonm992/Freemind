using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float movementSpeed = 100f;
    [SerializeField] private int damage = 10;

    private readonly Color[] colors = {Color.yellow, Color.red, Color.white, Color.blue, Color.green};
   
    public void Initialize(Vector3 movementDirection)
    {
        SetRandomColor();
        rigidbody.velocity += movementDirection * movementSpeed;
    }

    private void SetRandomColor()
    {
        var material = meshRenderer.material;
        material.color = colors[Random.Range(0, colors.Length)];
    }

    private void OnCollisionEnter(Collision other)
    {
        rigidbody.useGravity = true;

        if (other.gameObject.TryGetComponent(out DragonAI dragonAi))
        {
            dragonAi.DealDamage(damage);
        }
    }
}