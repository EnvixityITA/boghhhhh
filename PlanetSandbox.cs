using UnityEngine;
using System.Collections.Generic;

public class PlanetSandbox : MonoBehaviour
{
    public Transform planet;
    public GameObject asteroidPrefab;
    public GameObject humanPrefab;
    public GameObject animalPrefab;
    public Camera cam;

    public float gravity = 9.8f;
    public float spawnDistance = 200f;

    List<Rigidbody> bodies = new List<Rigidbody>();

    void Start()
    {
        Rigidbody[] rbs = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            bodies.Add(rb);
        }
    }

    void Update()
    {
        CameraControls();

        if (Input.GetKeyDown(KeyCode.A))
            SpawnAsteroid();

        if (Input.GetKeyDown(KeyCode.H))
            SpawnHuman();

        if (Input.GetKeyDown(KeyCode.N))
            SpawnAnimal();
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        foreach (Rigidbody rb in bodies)
        {
            if (rb == null) continue;

            Vector3 dir = (planet.position - rb.position).normalized;
            rb.AddForce(dir * gravity);
        }
    }

    void SpawnAsteroid()
    {
        Vector3 dir = Random.onUnitSphere;
        Vector3 pos = planet.position + dir * spawnDistance;

        GameObject asteroid = Instantiate(asteroidPrefab, pos, Quaternion.identity);

        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        if (rb == null) rb = asteroid.AddComponent<Rigidbody>();

        rb.velocity = -dir * Random.Range(30f, 80f);

        bodies.Add(rb);
    }

    void SpawnHuman()
    {
        Vector3 pos = planet.position + Random.onUnitSphere * 50f;

        GameObject human = Instantiate(humanPrefab, pos, Quaternion.identity);

        Rigidbody rb = human.GetComponent<Rigidbody>();
        if (rb == null) rb = human.AddComponent<Rigidbody>();

        bodies.Add(rb);
    }

    void SpawnAnimal()
    {
        Vector3 pos = planet.position + Random.onUnitSphere * 50f;

        GameObject animal = Instantiate(animalPrefab, pos, Quaternion.identity);

        Rigidbody rb = animal.GetComponent<Rigidbody>();
        if (rb == null) rb = animal.AddComponent<Rigidbody>();

        bodies.Add(rb);
    }

    void CameraControls()
    {
        float rotX = Input.GetAxis("Mouse X") * 5f;
        float rotY = Input.GetAxis("Mouse Y") * 5f;

        cam.transform.RotateAround(planet.position, Vector3.up, rotX);
        cam.transform.RotateAround(planet.position, cam.transform.right, -rotY);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.transform.position += cam.transform.forward * scroll * 50f;
    }
}
