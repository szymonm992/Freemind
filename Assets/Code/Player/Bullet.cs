using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour {
    [SerializeField] MeshRenderer meshRenderer;

    readonly Color[] colors = {Color.yellow, Color.red, Color.white, Color.blue, Color.green};
    public int damage = 10;

    public void Init() {
        SetRandomColor();
    }

    void SetRandomColor() {
        var material = meshRenderer.material;
        material.color = colors[Random.Range(0, colors.Length)];
    }

    void OnCollisionEnter(Collision other) {
        GetComponent<Rigidbody>().useGravity = true;
        var dragon = other.gameObject.GetComponent<DragonAI>();
        if (dragon)
            dragon.DealDamage(damage);
    }
}