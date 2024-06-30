using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private int damage = 10;

    private readonly Color[] colors = {Color.yellow, Color.red, Color.white, Color.blue, Color.green};
   
    public void Init()
    {
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        var material = meshRenderer.material;
        material.color = colors[Random.Range(0, colors.Length)];
    }

    private void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().useGravity = true;
        var dragon = other.gameObject.GetComponent<DragonAI>();

        if (dragon)
        {
            dragon.DealDamage(damage);
        }   
    }
}