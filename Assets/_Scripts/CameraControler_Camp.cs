using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler_Camp : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;
    public float speed;
    private CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        Zoom();
        Moving();
    }

    public void Moving()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontalInput, verticalInput,0);

        transform.position += dir * speed;
    }

    public void Zoom()
    {
        if (Input.GetKey(KeyCode.Equals))
        {
            virtualCamera.m_Lens.OrthographicSize += 0.1f;
            if(virtualCamera.m_Lens.OrthographicSize > maxZoom)
            {
                virtualCamera.m_Lens.OrthographicSize = maxZoom;
            }
        }

        if (Input.GetKey(KeyCode.Minus))
        {
            virtualCamera.m_Lens.OrthographicSize -= 0.1f;
            if (virtualCamera.m_Lens.OrthographicSize < minZoom)
            {
                virtualCamera.m_Lens.OrthographicSize = minZoom;
            }
        }
    }
}
