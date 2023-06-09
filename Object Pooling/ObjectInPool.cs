using UnityEngine;
using UnityEngine.Events;
public class ObjectInPool : MonoBehaviour
{
    [HideInInspector] public Transform parent;

    public bool resetScale = true;
    public bool resetPosition = true;

    private Vector3 defaultScale;

    private void Start() {
        defaultScale = transform.localScale;
    }
    public void RestartObjectInPool() {
        gameObject.SetActive(false);
        transform.SetParent(parent);
        
        if (resetScale) transform.localScale = defaultScale;
        if (resetPosition) transform.position = Vector3.zero;

        var poolReturn = GetComponent<PoolReturnAtTime>();
        if (poolReturn)
            poolReturn.inProgress = false;
    }
}
