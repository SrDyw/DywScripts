using UnityEngine;
using DywFunctions.Pool;

namespace DywFunctions
{
    namespace Pool
    {
        public abstract class PoolerObject : MonoBehaviour
        {
            private ObjectPooler pooler;
            private string poolerTag;

            public string PoolerTag { get => poolerTag; private set => poolerTag = value; }

            public abstract void OnPoolSpawn();

            public virtual void ReturnToPool()
            {
                pooler.BackToPooler(this);
            }

            public void Init(ObjectPooler pooler, string poolerTag) {
                this.pooler = pooler;
                this.poolerTag = poolerTag;
            }
        }

    }
}
