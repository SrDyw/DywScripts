using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Transform[] planes;
    [SerializeField]
    private Vector2 parallaxFactor;
    [SerializeField]
    private float distanceBetweenPlanes = 10;
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private float leftLimitValue = -2, rigthLimitValue = -80;
    [SerializeField]
    private float repositionValue = 40;


    private Vector2 cameraPosition;
    private Vector2 cameraSpeed = Vector2.zero;


    private float difference;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < planes.Length; i++) {
            planes[i].localPosition = new Vector3(
                planes[i].localPosition.x,
                planes[i].localPosition.y,
                distanceBetweenPlanes * (i)
            );
        }

        cameraPosition = mainCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraSpeed = CalculateCameraSpeed();

        for(int i = 0; i < planes.Length; i++) {
            float divider = ((i + 1));
            float moveX = cameraSpeed.x *  parallaxFactor.x;
            float moveY = cameraSpeed.y * parallaxFactor.y;

            planes[i].localPosition -= (new Vector3(moveX, moveY) / divider) * Time.fixedDeltaTime;

            difference = planes[i].position.x - mainCamera.position.x;
            if (difference > leftLimitValue) 
                planes[i].localPosition += Vector3.left * repositionValue;
            
            if (difference < rigthLimitValue) 
                planes[i].localPosition += Vector3.right * repositionValue;
        }

    }

    Vector2 CalculateCameraSpeed() {
        Vector2 speed = Vector2.zero;

        if (cameraPosition != (Vector2)mainCamera.position) {
            speed = new Vector2 (
                mainCamera.position.x - cameraPosition.x,
                mainCamera.position.y - cameraPosition.y
            );

            cameraPosition = mainCamera.position;
        }

        return speed;
    }
}
