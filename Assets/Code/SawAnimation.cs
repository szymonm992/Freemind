using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DragonsGame
{
    public class SawAnimation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 20f;

        private void Update()
        {
            ChangeSawRotation();
        }

        private void ChangeSawRotation()
        {
           transform.Rotate (rotationSpeed * Time.deltaTime,0,0);
        }
    }
}
