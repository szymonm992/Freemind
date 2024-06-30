using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace DragonsGame.Pooling
{
    public class MonoObjectPool<T> where T : MonoBehaviour
    {
        private readonly T prefab;
        private readonly Queue<T> pool = new Queue<T>();
        private readonly int repopulatingSize;

        public MonoObjectPool(T prefab, int initialSize, int repopulatingSize)
        {
            this.prefab = prefab;
            PopulatePool(initialSize).Forget();
        }

        private async UniTask PopulatePool(int objectsAmount)
        {
            int currentPoolAmount = pool.Count;
            await PopulatePoolInternal(objectsAmount, currentPoolAmount);
        }

        private async UniTask PopulatePoolInternal(int objectsAmount, int currentPoolAmount)
        {
            for (int i = 0; i < objectsAmount; i++)
            {
                T newObject = MonoBehaviour.Instantiate(prefab);
                newObject.gameObject.SetActive(false);
                pool.Enqueue(newObject);
            }

            await UniTask.WaitUntil(() => pool.Count == (currentPoolAmount + objectsAmount));
        }

        public async UniTask<T> GetFreeObject(Transform newObjectTransform = null)
        {
            if (pool.Count == 0)
            {
                await PopulatePool(repopulatingSize);
            }

            T newObject = pool.Dequeue();
            newObject.gameObject.SetActive(true);

            if (newObjectTransform != null)
            {
                newObject.transform.position = newObjectTransform.position;
                newObject.transform.rotation = newObjectTransform.rotation;
            }

            return newObject;
        }

        public void ReturnObjectToPool(T poolObject)
        {
            poolObject.gameObject.SetActive(false);
            pool.Enqueue(poolObject);
        }
    }
}