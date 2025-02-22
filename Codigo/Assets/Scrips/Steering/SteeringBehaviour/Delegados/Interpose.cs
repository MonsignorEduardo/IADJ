using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpose : Arrive
{
    [SerializeField]
    Agent agenteA;
    [SerializeField]
    Agent agenteB;

    private Vector3 midPoint;
    private void Start()
    {
       // target = Instantiate(agenteA);
        //target.GetComponent<MeshRenderer>().enabled = false;
    }

    public override Steering GetSteering(AgentNpc agent)
    {
        //Si no tenemos los par�metros devolvemos un steering b�sico
        if (agenteA == agenteB || agenteA == null || agenteB == null) return new Steering();

        //Calculamos el punto medio entre los dos objetivos
        midPoint = (agenteA.transform.position + agenteB.transform.position) / 2;


        float timeToReachMidPoint = Vector3.Distance(agent.transform.position, midPoint) / agent.mVelocity;

        //Posiciones de los objetivos a futuro
        Vector3 posA = agenteA.transform.position + agenteA.vVelocidad * timeToReachMidPoint;
        Vector3 posB = agenteB.transform.position + agenteB.vVelocidad * timeToReachMidPoint;
        midPoint = (posA + posB) / 2;

        var direction = midPoint - agent.transform.position;

        UseCustomDirectionAndRotation(direction);

        return base.GetSteering(agent);
    }

    protected override void OnDrawGizmos()
    {
        if (debug)
        {
            base.OnDrawGizmos();
            Gizmos.DrawLine(agenteA.transform.position, agenteB.transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(midPoint, 2);

            Gizmos.color = Color.white;
            Vector3 v = gameObject.GetComponent<AgentNpc>().OrientationToVector();
            Gizmos.DrawLine(transform.position, transform.position + v);
        }
    }
}
