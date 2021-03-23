﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Body
{
    //Radio
    [SerializeField]
    double rInterior;
    double rExterior;
    //Angulos
    [SerializeField]
    double aInterior;
    double aExterior;

    public bool dEbUg = false;

    private const double margen = 1.1;

    public Agent()
    {
        // Construye las Propiedades para que los valores interiores siempre sean inferiores a los exteriores.
        this.rExterior = this.rInterior * margen;
        this.aExterior = this.aInterior * margen;
    }

    private void OnDrawGizmos() // Gizmo: una línea en la dirección del objetivo
    {
        if (this.dEbUg)
        {
            Gizmos.DrawSphere(transform.position, (float)this.rInterior);
            Gizmos.DrawSphere(transform.position, (float)this.rExterior);
        }
        //Gizmos.DrawSphere(transform.position, (float)this.);
        //Gizmos.DrawSphere(transform.position, (float)this.rInterior);
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
