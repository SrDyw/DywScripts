using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DywFunctions.Pool;

public class SimplePoolerObject : PoolerObject
{
    [SerializeField]
    private float lifeTime = 5f;
    public override void OnPoolSpawn()
    {
        StartCoroutine(Timer(lifeTime));
    }

    IEnumerator Timer(float time) {
        yield return new WaitForSeconds(time);
        ReturnToPool();
    }
}
