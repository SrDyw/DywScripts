using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DywFunctions.Utils;



namespace DywFunctions
{
    namespace Pool
    {
        public class ObjectPooler : MonoBehaviour
        {
            [Serializable]
            public class Pooler
            {
                public string tag;
                public GameObject prefab;
                public int maxSize;

            }

            [Header("Pooler Settings")]

            [SerializeField]
            List<Pooler> poolers;

            private Dictionary<string, Queue<GameObject>> poolList;

            public Dictionary<string, Queue<GameObject>> PoolList { get => poolList; private set => poolList = value; }

            #region Singletton
            public static ObjectPooler instance;
            private void Awake() {
                instance = this;
            }
            #endregion Singletton

            private void Start()
            {
                QualitySettings.vSyncCount = -1;

                poolList = new Dictionary<string, Queue<GameObject>>();
                for (int i = 0; i < poolers.Count; i++)
                {
                    var tag = poolers[i].tag;
                    var cointainer = new GameObject($"{tag}");
                    poolList.Add(tag, new Queue<GameObject>());


                    cointainer.transform.SetParent(this.transform);

                    for (int j = 0; j < poolers[i].maxSize; j++)
                    {
                        var instance = Instantiate(poolers[i].prefab);
                        instance.transform.SetParent(cointainer.transform);
                        instance.name = $"{tag}.{j}";
                        var poolerObject = instance.GetComponent<PoolerObject>();
                        if (poolerObject)
                        {
                            instance.GetComponent<PoolerObject>().Init(this, tag);
                            poolList[tag].Enqueue(instance);
                            instance.gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.LogWarning($"The object {tag}{j} isn't PoolerObject, will not return");
                        }
                    }
                }

            }

            public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
            {
                if (!poolList.ContainsKey(tag)) {
                    Debug.LogError($"Pool don't contains the key {tag}");
                    return null;
                }

                if (poolList[tag].Count > 0)
                {
                    var spawned = poolList[tag].Dequeue();
                    spawned.gameObject.SetActive(true);
                    spawned.gameObject.transform.SetParent(null);

                    spawned.transform.position = position;
                    spawned.transform.rotation = rotation;

                    var pooler = spawned.GetComponent<PoolerObject>();
                    pooler?.OnPoolSpawn();

                    return spawned;
                }
                else
                {
                    Debug.LogWarning($"Pool ${tag} is Empty, imposible spawn!");
                    return null;
                }
            }


            public void BackToPooler(PoolerObject poolerObject)
            {
                poolerObject.gameObject.SetActive(false);
                poolerObject.transform.SetParent(GetParent(poolerObject.gameObject.name));

                poolList[poolerObject.PoolerTag].Enqueue(poolerObject.gameObject);
            }

            private Transform GetParent(string name)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    if (child.name == name.Split('.')[0])
                    {
                        return child;
                    }
                }
                return this.transform;
            }
        }

    }

}
