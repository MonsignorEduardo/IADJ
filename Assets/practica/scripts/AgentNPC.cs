﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AgentNPC : Agent
{
    public Steering miSteering;
    List<SteeringBehaviour> listSteerings = new List<SteeringBehaviour>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ApplySteering();
    }

    private void Awake()
    {
        //usar GetComponents<>() para cargar los SteeringBehavior del personaje
        listSteerings = GetComponents<SteeringBehaviour>().ToList<SteeringBehaviour>();
        foreach (SteeringBehaviour str in listSteerings)
        {
            str.enabled = true;
        }
    }

    private void LateUpdate()
    {
        //Recorre la lista construida en Awake() y calcula los Steering de los SteeringBehaviour
        List<Steering> calculatedStearing = new List<Steering>();
        foreach (SteeringBehaviour str in listSteerings)
        {
            if (str.enabled)
            {
                this.steering = str.GetSteering(this);
            }

        }

    }

    public void ApplySteering()
    {
        this.vAceleracion = Vector3.zero;
        this.vVelocidad = this.steering.velocidad;
        this.rotacion = this.steering.angulo;

        transform.position += this.vVelocidad * Time.deltaTime;
        this.orientacion += this.rotacion * Time.deltaTime;

        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, this.orientacion);
    }
}
