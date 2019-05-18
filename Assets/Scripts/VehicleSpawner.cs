using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{

    private float spawnTime;
    private Quaternion vehicleRotation;
    private Vector3 vehicleDirection;

    private List<Vehicle> spawnedVehicles;

    public GameObject[] vehicles;

    private float minSpawnTime = 0.5f;
    private float maxSpawnTime = 2f;




    void Start()
    {
        vehicles = Resources.LoadAll<GameObject>("Prefabs/Vehicles");
        spawnedVehicles = new List<Vehicle>();
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        StartCoroutine(SpawnVehicle());
    }


    IEnumerator SpawnVehicle()
    {
        yield return new WaitForSeconds(spawnTime);
        while (true)
        {
            Spawn();
            setDifficulty();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void setSpawnTime(float min, float max)
    {
        spawnTime = Random.Range(min, max);
    }

    private void setDifficulty()
    {
        if (FindObjectOfType<GameManager>().score >= 0 && FindObjectOfType<GameManager>().score < 10)
        {
            setSpawnTime(2f, 5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 5 && FindObjectOfType<GameManager>().score < 10)
        {
            setSpawnTime(1f, 4f);
        }
        else if (FindObjectOfType<GameManager>().score >= 10 && FindObjectOfType<GameManager>().score < 20)
        {
            setSpawnTime(1f, 3.5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 20 && FindObjectOfType<GameManager>().score < 30)
        {
            setSpawnTime(1f, 3f);
        }
        else if (FindObjectOfType<GameManager>().score >= 30 && FindObjectOfType<GameManager>().score < 50)
        {
            setSpawnTime(1f, 3f);
        }
        else if (FindObjectOfType<GameManager>().score >= 50 && FindObjectOfType<GameManager>().score < 75)
        {
            setSpawnTime(1f, 2.5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 75 && FindObjectOfType<GameManager>().score < 100)
        {
            setSpawnTime(1f, 2.5f);
        }
        else if (FindObjectOfType<GameManager>().score >= 100)
        {
            setSpawnTime(0.7f, 2.2f);
        }
    }


    public void SendVehicle()
    {
        if (spawnedVehicles.Count != 0) {
            SendVehicle(GetFirstVehicle());
        }
    }

    private Vehicle GetFirstVehicle()
    {
        return spawnedVehicles[0];
    }

    public void SendVehicle(Vehicle vehicle)
    {
        if (vehicle.stopped) {
            FindObjectOfType<AudioManager>().PlaySound("Pop");
            vehicle.GetComponent<Vehicle>().StopChangingColor();
            vehicle.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
            // Pass through barrier
            spawnedVehicles.Remove(vehicle);
            vehicle.stopped = false;
            vehicle.gameObject.layer = 10;
            vehicle.hasBeenSent = true;
        }
    }

    public void OnDestroy()
    {
        spawnedVehicles = null;
    }


    void Spawn()
    {
        if (spawnedVehicles.Count > 8) {
            return;
        }
        int randomVehicle = Random.Range(0, vehicles.Length);
        if (transform.name == "DownSpawner")
        {
            vehicleRotation = Quaternion.Euler(0, 0, 0);
            vehicleDirection = Vector3.forward;
        }
        else if (gameObject.name == "UpSpawner")
        {
            vehicleRotation = Quaternion.Euler(0, 180, 0);
            vehicleDirection = Vector3.back;
        }
        else if (gameObject.name == "LeftSpawner")
        {
            vehicleRotation = Quaternion.Euler(0, 90, 0);
            vehicleDirection = Vector3.right;
        }
        else if (gameObject.name == "RightSpawner")
        {
            vehicleRotation = Quaternion.Euler(0, 270, 0);
            vehicleDirection = Vector3.left;
        }

        GameObject vehicle = Instantiate(vehicles[randomVehicle], transform.position, vehicleRotation);
        spawnedVehicles.Add(vehicle.GetComponent<Vehicle>());
        vehicle.GetComponent<Vehicle>().direction = vehicleDirection;
        vehicle.GetComponent<Vehicle>().spawner = this;
        //vehicle.GetComponent<Rigidbody>().AddForce(vehicleDirection * 10, ForceMode.VelocityChange);

    }
}
