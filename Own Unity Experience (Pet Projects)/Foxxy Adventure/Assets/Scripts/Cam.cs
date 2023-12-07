using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Camera camera;
    public GameObject player;
    private Vector3 target;
    private float limitX = 3;
    public float leftLimit = 0;
    public float rightLimit = 0;

    // Start is called before the first frame update
    void Start()
    {
        limitX = Screen.width / 20;
        target = this.transform.position;
        target.x = player.transform.position.x;
        target = CheckCameraBounds(target);
        this.transform.position = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            UpdateTargetPositionBy();
            MoveCameraToTarget();
        }
    }

    void UpdateTargetPositionBy()
    {
        float distance = camera.WorldToScreenPoint(player.transform.position).x - camera.WorldToScreenPoint(this.transform.position).x;
        if (Mathf.Abs(distance) > limitX)
        {
            target.x = player.transform.position.x;
            target = CheckCameraBounds(target);
        }
    }

    void MoveCameraToTarget()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target, Time.deltaTime * 5);
    }

    Vector3 CheckCameraBounds(Vector3 cameraPos)
    {
        if (cameraPos.x < leftLimit)
            cameraPos.x = leftLimit;
        else if (cameraPos.x > rightLimit)
            cameraPos.x = rightLimit;
        return cameraPos;
    }
}