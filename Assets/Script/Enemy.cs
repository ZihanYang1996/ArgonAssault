using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] int scoreValue = 1;
    [SerializeField] int health = 5;
    [SerializeField] float hitFlashDuration = 0.1f;
    [SerializeField] Color hitFlashColor = Color.red;
    ScoreBoard scoreBoard;
    Material material;
    Color baseColor;

    GameObject instantiatedAtRuntime;

    void Awake()
    {
        scoreBoard = GameObject.FindObjectOfType<ScoreBoard>();
        material = gameObject.GetComponent<MeshRenderer>().material;
        baseColor = material.GetColor("_Color");
        instantiatedAtRuntime = GameObject.Find("InitiatedAtRuntime");
    }
    void OnParticleCollision(GameObject other)
    {
        health -= scoreValue;
        if (health <= 0)
        {
            KillEnemy(other);
        }
        else
        {
            StartCoroutine(HitByLaser());
        }
        
        scoreBoard.IncreaseScore(scoreValue);
        // Debug.Log("Enemy hit by laser");

    }

    private void KillEnemy(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);
        Explode(collisionEvents[0].intersection);
        Destroy(gameObject);
    }

    void Explode(Vector3 location)
    {
        GameObject explosionInstance = Instantiate(explosion, location, Quaternion.identity);
        explosionInstance.transform.parent = instantiatedAtRuntime.transform;
        ParticleSystem explosionParticleSystem = explosionInstance.GetComponent<ParticleSystem>();
        Destroy(explosionInstance, explosionParticleSystem.main.duration + explosionParticleSystem.main.startLifetime.constantMax);
    }

    IEnumerator HitByLaser()
    {
        material.SetColor("_Color", hitFlashColor);
        yield return new WaitForSeconds(hitFlashDuration);
        material.SetColor("_Color", baseColor);
    }
}
