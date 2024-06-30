using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SawAnimation : MonoBehaviour 
{
    private List<Bullet> bullets = new List<Bullet>();
    private int bulletsCount = 0;

    private void Update()
    {
        ChangeSawRotation();
    }

    private void ChangeSawRotation() 
    {
        var player = FindObjectOfType<PlayerController>();
        bullets = FindObjectsOfType<Bullet>().ToList();
        Random.seed = (int)(player.transform.position.x + player.transform.position.y + player.transform.position.z);
        bulletsCount = CalculateBulletsCount(bullets);
        var angleDiff = Random.value * bulletsCount;
        gameObject.transform.Rotate(angleDiff, 0, 0);        
    }

    private int CalculateBulletsCount(List<Bullet> bullets)
    {
        bulletsCount = 0;

        foreach (var bullet in bullets)
        {
            bulletsCount++;
        }
        
        if (bulletsCount < 10)
        {
            bulletsCount = 10;
        }

        return bulletsCount;
    }
}
