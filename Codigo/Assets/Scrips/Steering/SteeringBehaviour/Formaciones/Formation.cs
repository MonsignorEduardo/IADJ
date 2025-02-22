using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Formation
{
    private bool deletingFormation;
    private Dictionary<AgentNpc, bool> formationDone;
    public bool formationReady;
    public AgentNpc leader;
    public Dictionary<AgentNpc, Steering> soldiers; // Solados y su poscion relativa con respecto al lider.
    private bool debug;
    
    public Formation(AgentNpc leader,bool debug=false)
    {
        this.leader = leader;
        soldiers = new Dictionary<AgentNpc, Steering>();
        formationDone = new Dictionary<AgentNpc, bool>();
        this.debug = debug;
    }
    

    // El lider creala formacion y le dice a los soldados que son soldados.
    public void MakeFormation()
    {
        leader.BecameLeader(this);
        foreach (var agent in soldiers.Keys)
        {
            agent.BecameSoldier(this);
            formationDone.Add(agent, false);
        }
        formationReady = true;
        if (!debug) return;
        leader.name = "Lider";
        foreach (var agent in soldiers.Keys.Select((value, index) => new { value, index }))
        {
            agent.value.name = $"Soldado {agent.index}";
        }

    }

    // El lider o un soldado disuelve la formacion y le dice al resto de soldados que la disuelva
    public void DissolveFormation()
    {
        if (deletingFormation) return;
        // Yo soy quien borra la formacion
        formationReady = false;
        deletingFormation = true;

        // C# Funciona con recogedor de basura
        // Es decir que si no hay puntero apuntando a esta clase se borra
        leader.ResetStateAction();

        foreach (var agent in soldiers.Keys) agent.ResetStateAction();
    }

    // Obtiene la posicion globlal de agente npc
    public Steering GetGlobalPosition(AgentNpc agente)
    {
        var relativePosition = soldiers[agente];
        relativePosition.lineal = leader.transform.TransformPoint(relativePosition.lineal);
        return relativePosition;
    }

    // Obtiene el rango del del lider
    private FormationRank GetRank(AgentNpc agente)
    {
        if (agente == leader) return FormationRank.Leader;
        if (soldiers.Keys.Contains(agente)) return FormationRank.Soldier;
        throw new Exception($"{agente.name} no esta en formacion");
    }


    public bool ImLeader(AgentNpc agentNpc)
    {
        if (agentNpc == null) return false;
        return GetRank(agentNpc) == FormationRank.Leader;
    }

    public bool ImSoldier(AgentNpc agentNpc)
    {
        if (agentNpc == null) return false;
        return GetRank(agentNpc) == FormationRank.Soldier;
    }

    // Cuando todos est�n en posici�n le decimos al l�der que se espere para moverse.
    public void ImInPosition(AgentNpc agentNpc)
    {
        formationDone[agentNpc] = true;

        if (IsFormationDone())
        {
            leader.AllInPositionWaitABitMore();
        }
    }

    // El soldado no esta en posici�n

    public void ImNotInPosition(AgentNpc agentNpc) =>formationDone[agentNpc] = false;


    // Los solados ya no est�n en posici�n porque el l�der se ha movido
    public void WaitForSoldiers()
    {
        foreach (var key in formationDone.Keys.ToList())
            formationDone[key] = false;

        leader.MoveABitBeforeWaiting(); // Le decimos al l�der que espere a los soldados en un futuro
    }

    private bool IsFormationDone() => !formationDone.Values.Contains(false);

    public List<AgentNpc> GetRest(AgentNpc agentNpc)
    {
        // Se supone que esta en la formacion

        if (ImLeader(agentNpc)) return soldiers.Keys.ToList();
        var resto = new List<AgentNpc> {leader};
        resto.AddRange(soldiers.Keys.Where(npc => npc != agentNpc));

        return resto;
    }
}