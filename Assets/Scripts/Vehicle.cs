using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    //public Rigidbody vehicle;
    public Vector3 direction;
    public bool crashed;
    public bool stopped;
    public bool hasBeenSent = false;
    public RaycastHit hit;
    private bool IsTriggered;

    public VehicleSpawner spawner;

    float minWaitTime = 1f;
    float maxWaitTime = 6f;

    float waitTime = 6f;


    private Coroutine lerpCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        lerpCoroutine = null;
        Physics.IgnoreLayerCollision(9, 10);
        IsTriggered = false;
        setDifficulty();
    }

    public void setWaitTime(float min, float max) {
        waitTime = Random.Range(min, max);
    }

    private void setDifficulty()
    {
        if (FindObjectOfType<GameManager>().score >= 0 && FindObjectOfType<GameManager>().score < 10)
        {
            setWaitTime(3f, 6f);
        }
        else if (FindObjectOfType<GameManager>().score >= 5 && FindObjectOfType<GameManager>().score < 10)
        {
            setWaitTime(3f, 6f);
        }
        else if (FindObjectOfType<GameManager>().score >= 10 && FindObjectOfType<GameManager>().score < 20)
        {
            setWaitTime(3f, 5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 20 && FindObjectOfType<GameManager>().score < 30)
        {
            setWaitTime(3f, 5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 30 && FindObjectOfType<GameManager>().score < 50)
        {
            setWaitTime(2f, 4.5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 50 && FindObjectOfType<GameManager>().score < 75)
        {
            setWaitTime(2f, 4.5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 75 && FindObjectOfType<GameManager>().score < 100)
        {
            setWaitTime(2f, 4f);
        }
        else if (FindObjectOfType<GameManager>().score >= 100)
        {
            setWaitTime(2f, 4f);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Vehicle")
        {
            if (crashed == false) {
                crashed = true;
                if (!GameManager.isGameOver) {
                    FindObjectOfType<AudioManager>().PlaySound("Hit");
                }
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }

    void OnBecameInvisible()
    {
        if (!GameManager.isGameOver) {
            Score.ScorePoint();
        }
        Destroy(gameObject);
    }

    IEnumerator SendVehicleAfterTime()
    {
        yield return new WaitForSeconds(waitTime);
        if (!hasBeenSent) {
            spawner.SendVehicle(this);
            FindObjectOfType<AudioManager>().PlaySound("Horn");
            StopChangingColor();
        }
    }


    private void shouldStop()
    {
        float deploymentHeight = 1.5f;
        var raycastPos = transform.position;
        // Or else the ray will be cast from 0, sometimes missing the object
        raycastPos.y = raycastPos.y + 0.5f;

        if (direction == Vector3.forward) {
            raycastPos.z = raycastPos.z + (GetComponent<BoxCollider>().size.z / 2);
        }
        else if (direction == Vector3.back)
        {
            raycastPos.z = raycastPos.z - (GetComponent<BoxCollider>().size.z / 2);
        }
        else if (direction == Vector3.right)
        {
            raycastPos.x = raycastPos.x + (GetComponent<BoxCollider>().size.z / 2);
        }
        else if (direction == Vector3.left)
        {
            raycastPos.x = raycastPos.x - (GetComponent<BoxCollider>().size.z / 2);
        }

        Ray landingRay = new Ray(raycastPos, transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(raycastPos, transform.TransformDirection(Vector3.forward) * deploymentHeight);

        Collider ownCollider = transform.root.gameObject.GetComponent<Collider>();
        if (Physics.Raycast(landingRay, out hit, deploymentHeight))
        {
            if (hit.collider.tag == "Vehicle" && ownCollider != hit.collider)
            {
                StopVehicle();
            }
            else
            {
                moveVehicle();
            }
            if (hit.collider.tag == "Barrier") {
                StopVehicle();
                StartCoroutine(SendVehicleAfterTime());
                if (IsTriggered == false)
                {
                    IsTriggered = true;
                    lerpCoroutine = StartCoroutine(UpdateTextColor());
                }
            }
        } else {
            moveVehicle();
        }
    }

    private void StopVehicle()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stopped = true;
    }

    private void FixedUpdate()
    {
        if (gameObject != null && !hasBeenSent)
        {
            shouldStop();
        }
    }


    // Once a frame
    private void Update()
    {
        if (gameObject != null && !crashed && !stopped)
        {
            moveVehicle();
        }
    }

    public void StopChangingColor()
    {
        StopCoroutine(lerpCoroutine);
    }


    public IEnumerator UpdateTextColor()
    {
        float t = 0;
        while (t < 1)
        {
            // Now the loop will execute on every end of frame until the condition is true
            this.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, new Color(1f, 0.36f, 0.36f), t);
            t += Time.deltaTime / waitTime;
            yield return new WaitForEndOfFrame(); // So that I return something at least.
        }
    }


    void moveVehicle()
    {
        stopped = false;
        this.GetComponent<Rigidbody>().velocity = (direction * 35);
    }
}
