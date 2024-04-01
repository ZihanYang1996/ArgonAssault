using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    // List<ParticleSystem> lasers = new List<ParticleSystem>();
    ParticleSystem[] lasers = new ParticleSystem[2];

    void Awake()
    {
        ParticleSystem leftLaser = gameObject.transform.Find("LaserLeft").GetComponent<ParticleSystem>();
        ParticleSystem rightLaser = gameObject.transform.Find("LaserRight").GetComponent<ParticleSystem>();
        // lasers.Add(leftLaser);
        // lasers.Add(rightLaser);
        lasers[0] = leftLaser;
        lasers[1] = rightLaser;
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
