using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaInimigo : Bala
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torre"))
        {
            other.GetComponent<Torre>().CausaDano(forcaDano);
            Destroy(this.gameObject);
        }

        else if (other.gameObject.CompareTag("Destino"))
        {
            GameController.Controller.Vida -= forcaDano;
            Destroy(this.gameObject);
        }

        else if (other.gameObject.CompareTag("Firewall"))
        {
            Debug.Log("AIII AI");
            other.GetComponent<Firewall>().CausaDano(forcaDano);
            Destroy(this.gameObject);
        }
    }
}
