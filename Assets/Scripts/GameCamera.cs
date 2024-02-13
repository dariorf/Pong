using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    public  bool goalScored = false;
    private Camera mainCamera;
    private float speed = 20f;
    private float zoomSpeed = 50f;
    private Vector3 startPos;
    private float startFOV;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        if (goalScored)
        {
            FollowBall();
        }
    }

    public void FollowBall()
    {
        Vector3 newPos = new Vector3(ball.transform.position.x, ball.transform.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, speed * Time.deltaTime);
        ZoomCamera(14);
    }

    public void Reset()
    {
        transform.position = startPos;
        mainCamera.fieldOfView = startFOV;
        goalScored = false;
    }

    void ZoomCamera(float target)
    {
        mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, target, zoomSpeed * Time.deltaTime);
    }
}
