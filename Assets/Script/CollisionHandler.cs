using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    GameObject InitiatedAtRuntime;

    void Awake()
    {
        InitiatedAtRuntime = GameObject.Find("InitiatedAtRuntime");
    }

    void OnCollisionEnter(Collision other)
    {
        Explode(other.contacts[0].point);
        Invoke("ReloadScene", 1f);
        // ReloadScene();
    }

    void Explode(Vector3 location)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        GameObject explosionInstance = Instantiate(explosion, location, Quaternion.identity);
        explosionInstance.transform.parent = InitiatedAtRuntime.transform;
        ParticleSystem explosionParticleSystem = explosionInstance.GetComponent<ParticleSystem>();
        Destroy(explosionInstance, explosionParticleSystem.main.duration + explosionParticleSystem.main.startLifetime.constantMax);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
