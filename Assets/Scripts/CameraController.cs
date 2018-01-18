using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float cameraMoveSpeed;
    [SerializeField]
    private float cameraRotationSpeed;

    public void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Vector3 forward = Vector3.Cross(Vector3.up, transform.right);

        if(mousePosition.x > 0.95f)
        {
            transform.position += transform.right * cameraMoveSpeed * Time.deltaTime;
        }
        if (mousePosition.x < 0.05f)
        {
            transform.position -= transform.right * cameraMoveSpeed * Time.deltaTime;
        }
        if (mousePosition.y > 0.95f)
        {
            transform.position -= forward * cameraMoveSpeed * Time.deltaTime;
        }
        if (mousePosition.y < 0.05f)
        {
            transform.position += forward * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up * -cameraRotationSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up * cameraRotationSpeed * Time.deltaTime, Space.World);
    }
}
