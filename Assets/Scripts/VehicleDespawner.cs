using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDespawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        pos.y = 0;
        if (transform.name == "DownDespawner")
        {
            pos.x = pos.x - GetComponent<MeshRenderer>().bounds.size.x / 2;
            transform.position = pos;
        }
        else if (gameObject.name == "UpDespawner")
        {
            Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            transform.position = p;
        }
        //else if (gameObject.name == "RightDespawner")
        //{
        //    transform.position = ;
        //}
        //else if (gameObject.name == "LeftDespawner")
        //{
        //    //pos.z = pos.z - GetComponent<MeshRenderer>().bounds.size.x / 2;
        //    //transform.position = pos;
        //}
    }
}
