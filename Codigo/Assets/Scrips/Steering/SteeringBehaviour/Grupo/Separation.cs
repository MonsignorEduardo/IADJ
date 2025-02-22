﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Separation : SteeringBehaviour
{
    [SerializeField]
    List<Agent> targets;
    [SerializeField]
    float threshold;
    [SerializeField]
    float decayCoefficient;
    public override Steering GetSteering(AgentNpc agent)
    {
        Steering steering = new Steering(0, new Vector3(0, 0, 0));
        foreach (Agent target in targets)
        {
            Vector3 direction = agent.transform.position - target.transform.position;
            float distance = direction.magnitude;
            if (distance < threshold)
            {
                //decayCoefficient = (float)agent.RExterior;
                // Fuerza de la repulsion
                float strenght = Mathf.Min(decayCoefficient /
                    (distance * distance), agent.mAcceleration);
                // Añadimos la aceleracion;
                direction.Normalize();
                steering.lineal += strenght * direction;
            }
        }
        // Convertimos velocidad en aceleracion | limitamos la aceleracion

        steering.lineal.Normalize();
        steering.lineal *= agent.mAcceleration;
        if (debug)
            Debug.DrawRay(transform.position, steering.lineal, Color.green);
        return steering;
    }
    private void Start()
    {
        this.targets = GameObject.FindGameObjectsWithTag("flock")
                        .Select(target => target.GetComponent<Agent>())
                        .ToList(); ;

        steeringGroup = SteeringGroup.Distance;
    }
}
