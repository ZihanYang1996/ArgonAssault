using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    // List<ParticleSystem> lasers = new List<ParticleSystem>();
    ParticleSystem[] lasers = new ParticleSystem[2];
    Transform[] laserTransforms = new Transform[2];
    Vector3 mousePosition;
    [SerializeField] float laserDistance = 100f;

    void Awake()
    {
        ParticleSystem leftLaser = gameObject.transform.Find("LaserLeft").GetComponent<ParticleSystem>();
        ParticleSystem rightLaser = gameObject.transform.Find("LaserRight").GetComponent<ParticleSystem>();

        laserTransforms[0] = leftLaser.gameObject.transform;
        laserTransforms[1] = rightLaser.gameObject.transform;

        // lasers.Add(leftLaser);
        // lasers.Add(rightLaser);
        lasers[0] = leftLaser;
        lasers[1] = rightLaser;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = laserDistance;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        foreach (Transform laserTransform in laserTransforms)
        {
            laserTransform.LookAt(mouseWorldPosition);
        }
    }

    public void Fire()
    {
        foreach (ParticleSystem laser in lasers)
        {
            laser.Play();
        }
    }

    public void StopFire()
    {
        foreach (ParticleSystem laser in lasers)
        {
            laser.Stop();
        }
    }
}
