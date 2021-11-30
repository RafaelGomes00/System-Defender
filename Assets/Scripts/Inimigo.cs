using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{

    public int granaDestroi;
    public float energia = 100;
    public float velocidadeAtaque;
    public NavMeshAgent agente;
    public Transform pontaCanhao;
    public GameObject projetil;

    private Vector3 destino;
    private Collider hit;

    private void OnDestroy()
    {
        GameController.Controller.qntInimigo--;
        if (GameController.Controller.QntInimigo <= 0 && GameController.Controller.QntOndas <= 0)
        {
            UIController ui = GameObject.Find("UIController").GetComponent<UIController>();

            ui.Vitoria();
            Debug.Log("cOLE ZÉ, GANHOU");
        }
    }
    public Vector3 Destino
    {
        set
        {
            if (value != null)
            {
                destino = value;
            }
        }
    }
    public void Dispara()
    {
            this.transform.LookAt(hit.transform);
            Instantiate(projetil, pontaCanhao.transform.position,
                                  pontaCanhao.transform.rotation);
    }
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torre") || other.gameObject.CompareTag("Destino"))
        {
            hit = other;
            agente.SetDestination(this.transform.position);
            InvokeRepeating("Dispara", 1f, velocidadeAtaque);
        }

        else if (other.gameObject.CompareTag("Firewall") && GameController.Controller.TemFileiraOcupada && destino == other.transform.position)
        {
            hit = other;
            agente.SetDestination(this.transform.position);
            InvokeRepeating("Dispara", 1f, velocidadeAtaque);
        }
    }

    public void CausaDano(float dano)
    {
        energia -= dano;

        if (energia <= 0)
        {
            Destroy(this.gameObject);
            GameController.Controller.Conta += granaDestroi;
        }
    }

    void Start()
    {      
        GameController.Controller.QntInimigo++;
        destino = GameController.Controller.Destino(this.transform.position);
        agente.SetDestination(destino);
    }
}
