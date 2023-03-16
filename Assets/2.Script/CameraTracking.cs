using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CameraTracking : MonoBehaviour
{
    public Transform target; //따라갈 대상

    public float smoothSpeed = 3; //카메라 선형보간 속도
    public Vector2 offset;
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float width, height;

    private void Start()
    {
        width = Camera.main.aspect * Camera.main.orthographicSize; //aspect는 화면 비율을 뜻함.
        height = Camera.main.orthographicSize; //camera 컴포넌트 안의 size는 높이만을 의미함. 넓이는 화면 비율에 따라 비례함.
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + width, limitMaxX - width),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + height, limitMaxY - height), // Y
            -10); // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}

