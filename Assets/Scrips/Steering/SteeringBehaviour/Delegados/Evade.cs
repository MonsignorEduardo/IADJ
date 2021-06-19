﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Evade : Flee
{

    public float maxPrediction;
    public override Steering GetSteering(AgentNPC miAgente)
    {
        // Vamosa  crear un nuevo target en la posicion donde estaria nuestro target
        Vector3 direction = target.transform.position - miAgente.transform.position;
        float distance = direction.magnitude;

        // Current Speed
        float speed = miAgente.vVelocidad.magnitude;

        // Si la velocidad es muy pequeña vamos a darle un predicion 
        float prediction = (speed <= distance / maxPrediction) ? maxPrediction : distance / speed;

        // NO Puedo usar el target porque va asignado a otro objecto

        this.predictedDirection = target.transform.position;

        this.predictedDirection += target.vVelocidad * prediction;

        this.usePredicted = true;

        return base.GetSteering(miAgente);

    }

    // Start is called before the first frame update
    void Start()
    {
        grupo = Grupo.COLISIONES;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
