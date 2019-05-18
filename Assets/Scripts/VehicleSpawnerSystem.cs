using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawnerSystem : MonoBehaviour
{
    private GameObject rightSpawner;
    private GameObject upSpawner;
    private GameObject leftSpawner;
    private GameObject downSpawner;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (!GameManager.isPaused)
        {
            switch (data.Direction)
            {
                case SwipeDirection.Up:
                    GameObject.Find("DownSpawner").GetComponent<VehicleSpawner>().SendVehicle();
                    break;
                case SwipeDirection.Down:
                    GameObject.Find("UpSpawner").GetComponent<VehicleSpawner>().SendVehicle();
                    break;
                case SwipeDirection.Left:
                    GameObject.Find("RightSpawner").GetComponent<VehicleSpawner>().SendVehicle();
                    break;
                case SwipeDirection.Right:
                    GameObject.Find("LeftSpawner").GetComponent<VehicleSpawner>().SendVehicle();
                    break;
            }
        }

    }

    void OnDestroy()
    {
        SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
    }

    // Start is called before the first frame update
    void Start()
    {
        downSpawner = GameObject.Find("downSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
