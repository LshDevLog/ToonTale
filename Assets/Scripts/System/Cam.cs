using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cam : MonoBehaviour
{
    Camera cam;
    Transform m_Player;

    [Header("Adjustment")]
    public float Height;
    public float Distance;
    public float Damping;

    private void Awake()
    {
        cam = Camera.main;
        cam.GetUniversalAdditionalCameraData().antialiasing = AntialiasingMode.FastApproximateAntialiasing;

        m_Player = GameObject.Find("Player").transform;
    }
    void Start()
    {
        Height = 10.0f;
        Distance = 5.0f;

        //6 is smooth, 50 is almost fixed
        Damping = 50.0f;
    }

    void LateUpdate()
    {
        if(m_Player != null)
        {
            Follow();
        }
    }


    void Follow()
    {
        Vector3 camPos = m_Player.position + Vector3.up * Height - Vector3.forward * Distance;
        transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime * Damping);
        transform.LookAt(m_Player.position);
    }
}


