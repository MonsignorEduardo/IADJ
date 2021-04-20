﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Seek : SteeringBehaviour
{
    public override Steering GetSteering(AgentNPC miAgente)
    {
        this.steering = new Steering(0, new Vector3(0, 0, 0));

        // Full Aceleration Seek a tope


        Vector3 position = this.usePredicted ? this.predictedPosition : target.transform.position;

        steering.lineal = position - miAgente.transform.position;
        steering.lineal.Normalize();
        steering.lineal *= miAgente.mAceleracion;

        steering.angular = 0;
        return this.steering;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
