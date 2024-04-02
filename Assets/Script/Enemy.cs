using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);
        Explode(collisionEvents[0].intersection);
        // Debug.Log("Enemy hit by laser");
        Destroy(gameObject);
    }

    void Explode(Vector3 location)
    {
        GameObject explosionInstance = Instantiate(explosion, location, Quaternion.identity);
        ParticleSystem explosionParticleSystem = explosionInstance.GetComponent<ParticleSystem>();
        Destroy(explosionInstance, explosionParticleSystem.main.duration + explosionParticleSystem.main.startLifetime.constantMax);
    }
}
