using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAround : MonoBehaviour
{
    // butterflyType: "Targer"/"Distractor"
    public string butterflyType;
    
    // maxRadius: Max Radius of Travel
    // minRadius: Min Radius of Tavel
    // maxTravelRadius: Max Radius for butterfly to travel to from current location
    public int maxRadius, minRadius, maxTravelRadius;

    // speed: Speed of butterfly
    // verticalSpeed: Vertical speed along the path
    // amplitude: Amplitude
    // reachDist: Distance from TravelLoc to find a new TravelLoc point
    public float speed, verticalSpeed, amplitude, reachDist;

    // center: Location of player / center of all circles
    // currentLoc: Current location of Butterfly
    public Vector3 center, TravelLoc;

    void FixedUpdate()
    {
        Vector3 tempPosition = transform.position;
        Vector3 direction = TravelLoc - tempPosition;

        tempPosition += direction * Time.deltaTime * speed;

        //tempPosition.x += direction.x * Time.deltaTime * speed;
        //tempPosition.y += direction.y * Time.deltaTime * speed;
        //tempPosition.z += direction.z * Time.deltaTime * speed;

        //if (direction.magnitude > reachDist)
            //tempPosition.y += Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;

        transform.position = tempPosition;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation * Quaternion.Euler(-90, -90, 0);

        if (direction.magnitude <= reachDist)
        {
            TravelLoc = newLocation();
        }
    }

    void LateUpdate()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    Vector3 newLocation()
    {
        Vector3 newLoc = new Vector3();

        // generate new Travel Location until it's within the bounds
        bool cont = false;
        float x = new float(), y = new float(), z = new float();
        float distOrigin;

        while (!cont)
        {
            x = transform.position.x + ((Random.Range(0, 2) * 2 - 1) * Random.Range(0.0f, maxTravelRadius));
            y = transform.position.y + ((Random.Range(0, 2) * 2 - 1) * Random.Range(0.0f, maxTravelRadius));
            z = transform.position.z + ((Random.Range(0, 2) * 2 - 1) * Random.Range(0.0f, maxTravelRadius));

            distOrigin = Mathf.Sqrt(Mathf.Pow(x - center.x, 2) + Mathf.Pow(y - center.y, 2) + Mathf.Pow(z - center.z, 2));

            if(distOrigin < maxRadius && distOrigin > minRadius)
            {
                cont = true;
                if (y < 0)
                    y = y * (-1);

                if (x < 0)
                    x = x * (-1);
                break;
            }

        }

        newLoc.Set(x, y, z);

        return newLoc;
    }

    public void setValue(string butterflyType, int minRadius, int maxRadius, float speed, float verticalSpeed, float amplitude, float reachDist, Vector3 center)
    {
        this.butterflyType = butterflyType;
        this.minRadius = minRadius;
        this.maxRadius = maxRadius;
        this.speed = speed;
        this.verticalSpeed = verticalSpeed;
        this.amplitude = amplitude;
        this.reachDist = reachDist;
        this.center = center;

        maxTravelRadius = maxRadius - minRadius;

        transform.position = newLocation();     // To spawn at a newLocation
        TravelLoc = newLocation();              // To travel to a newLocation
    }
}
