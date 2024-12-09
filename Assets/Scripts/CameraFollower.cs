using UnityEngine;
using System.Collections;

public class CameraFollower: MonoBehaviour
{
    public GameObject PlayerToFollowXAxis;
    [SerializeField] float regularSizeForCamera = 12;
    [SerializeField] float cameraSpeed = 2f;
    float targetX;
    void start(){
        targetX = PlayerToFollowXAxis.transform.position.x;
        Camera cameraComponent = GetComponent<Camera>();
        if (cameraComponent != null)
        {
            cameraComponent.orthographicSize = regularSizeForCamera;
        }
    }

    public void resizeCamera(float scaleMultuply)
    {
        Camera.main.orthographicSize =regularSizeForCamera * scaleMultuply;
    }
    void Update()
    {
        if (PlayerToFollowXAxis.transform.position.x>=targetX && PlayerToFollowXAxis != null){
            Vector3 newPosition = new Vector3(PlayerToFollowXAxis.transform.position.x, transform.position.y, transform.position.z);
            transform.position = newPosition;

        }
    }
    public void MoveCamera(Vector3 startPoint, Vector3 endPoint)
    {
        StartCoroutine(MoveCameraCoroutine(endPoint, startPoint));

    }

    private IEnumerator MoveCameraCoroutine(Vector3 startPoint, Vector3 endPoint)
    {
        float duration = Vector3.Distance(startPoint, endPoint) / cameraSpeed; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPoint, endPoint, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPoint; 
    }
}