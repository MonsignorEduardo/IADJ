using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : GoTarget
{
    [SerializeField]
    public List<GameObject> objetivos;
    AgentNpc objetivo;
    public override Steering GetSteering(AgentNpc miAgente)
    {
        foreach (GameObject v in objetivos)
        {
            objetivo = miAgente;
            objetivo.transform.position = v.transform.position;
            return base.GetSteering(objetivo);
        }
        return base.steering;
    }
}
