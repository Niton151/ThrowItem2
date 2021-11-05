using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
[RequireComponent(typeof(SphereCollider))]
public class Singularity : MonoBehaviour
{
    //This is the main script which pulls the objects nearby
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;

    [SerializeField]
    private ParticleSystem particle;

    private bool isExistHole = false;

    private float runTime;

    private float timer = 0;

    private float excessTime = 7f;

    private GameObject core;

    private List<ParticleSystem> particleSystems;

    void Awake() {
        m_GravityRadius = GetComponent<SphereCollider>().radius;
        runTime = transform.root.gameObject.GetComponent<TeleportGrenade>().GetRunTime();
        core = transform.root.gameObject.GetComponent<TeleportGrenade>().GetCoreObj();
        particleSystems = transform.root.gameObject.GetComponentsInChildren<ParticleSystem>().ToList();

        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    private void Update()
    {
        if (particle.time >= runTime)
        {
            isExistHole = true;           
        }

        if (isExistHole)
        {
            ChangeParticleSpeed(1f);
            timer += Time.deltaTime;
            if (timer >= excessTime)
            {
                core.SetActive(true);
                if (timer >= excessTime + 3f)
                {
                    ChangeParticleSpeed(0.01f);
                    core.SetActive(false);
                    isExistHole = false;
                    particle.gameObject.SetActive(false);
                    timer = 0;
                }
            }
        }
    }

    void OnTriggerStay (Collider other) {
        if(isExistHole && other.attachedRigidbody && other.transform.root.GetComponent<SingularityPullable>()) {
            float gravityIntensity = 1/Vector3.Distance(transform.position, other.transform.position) ;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
        }
    }

    private void ChangeParticleSpeed(float speed)
    {
        foreach(var p in particleSystems)
        {
            var main = p.main;
            main.simulationSpeed = speed;
        }
    }
}
