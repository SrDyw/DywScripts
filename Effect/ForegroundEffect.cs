using UnityEngine;

public class ForegroundEffect : MonoBehaviour
{
	
    [SerializeField]
    private Transform[] planes;
    [SerializeField]
    private Vector2 foregroundFactor;
    [SerializeField]
    private Transform mainCamera;

    private Vector2 cameraPosition;

    [SerializeField]
    private float distanceBetweenPlanes;
    [SerializeField]
    private float leftLimitValue, rigthLimitValue;

    private Vector2 cameraSpeed;
	//< Properties

	//>
	
    void Start()
    {
        for(int i = 0; i < planes.Length; i++) {
            planes[i].localPosition = new Vector3(
                planes[i].localPosition.x,
                planes[i].localPosition.y,
                -distanceBetweenPlanes * (i)
            );
        }

        cameraPosition = transform.position;        
    }

    void Update()
    {
        cameraSpeed = CalculateCameraSpeed();

        for(int i = 0; i < planes.Length; i++) {
            float divider = ((i + 1));
            float moveX = cameraSpeed.x *  foregroundFactor.x;
            float moveY = cameraSpeed.y * foregroundFactor.y;

            planes[i].localPosition -= (new Vector3(moveX, moveY) / divider) * Time.fixedDeltaTime;

            float difference = planes[i].position.x - mainCamera.position.x;
            if (difference > leftLimitValue) 
                planes[i].localPosition += Vector3.left * 40;
            if (difference < rigthLimitValue) 
                planes[i].localPosition += Vector3.right * 40;
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
