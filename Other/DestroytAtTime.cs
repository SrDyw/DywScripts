using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroytAtTime : MonoBehaviour
{
    public float timeToDestroy = 1;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGameObject", timeToDestroy);
    }

    void DestroyGameObject() {
        Destroy(gameObject);
    }
}
