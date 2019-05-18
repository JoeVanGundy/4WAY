using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Collider roads;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = roads.bounds.size.x * Screen.height / Screen.width * 0.5f;
        //float screenRatio = (float)Screen.width / (float)Screen.height;
        //float targetRatio = roads.bounds.size.x / roads.bounds.size.z;

        //if (screenRatio >= targetRatio) {
        //    Camera.main.orthographicSize = roads.bounds.size.z / 2;
        //}
        //else {
        //    float differenceInSize = targetRatio / screenRatio;
        //    Camera.main.orthographicSize = roads.bounds.size.z / 2 * differenceInSize;
        //}

    }

}
