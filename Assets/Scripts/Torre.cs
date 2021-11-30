using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{
    public int precoVenda, precoCompra;
    public GameObject projetil, pontaCanhao, torreMovel;
    public float raioAtaque, velocidadeDisparo;
    public float energia = 100;
    public GameObject material1;
    public GameObject material2;
    public GameObject upgrade;

    private Grid grid;
    public Collider[] hit;
    private int posXPress, posZPress;

    public Grid Grid
    {
        set
        {
            grid = value;
        }
    }
    public int PosXPress
    {
        get
        {
            return posXPress;
        }
        set
        {
            posXPress = value;
        }
    }

    public int PosZPress
    {
        get
        {
            return posZPress;
        }
        set
        {
            posZPress = value;
        }
    }

    private void OnDestroy()
    {
        grid.SetDisponbilidade(posXPress, posZPress, this.tag);
        GameController.Controller.QntTorre--;
        if (GameController.Controller.QntInimigo > 0)
        {
            GameObject[] inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
            for (int i = 0; i < inimigo.Length; i++)
            {
                Vector3 v3 = GameController.Controller.Destino(inimigo[i].transform.position);
                inimigo[i].GetComponent<Inimigo>().agente.SetDestination(v3);
                inimigo[i].GetComponent<Inimigo>().Destino = v3;
            }
        }
    }

    public void CausaDano(float dano)
    {
        energia -= dano;

        if (energia <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Dispara()
    {
        torreMovel.transform.LookAt(hit[0].transform);
        Instantiate(projetil, pontaCanhao.transform.position,
                              pontaCanhao.transform.rotation);
    }
    void BuscaAlvo()
    {
        hit = null;
        int layerMask = LayerMask.GetMask("Inimigo");
        hit = Physics.OverlapSphere(this.transform.position,
                                    raioAtaque, layerMask);
        if (hit.Length > 0)
        {
            Dispara();
        }
    }

    void Start()
    {

        if (GameController.Controller.QntInimigo > 0)
        {
            GameObject[] inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
            for (int i = 0; i < inimigo.Length; i++)
            {
                Vector3 v3 = GameController.Controller.Destino(inimigo[i].transform.position);
                inimigo[i].GetComponent<Inimigo>().agente.SetDestination(v3);
                inimigo[i].GetComponent<Inimigo>().Destino = v3;
            }
        }

        GameController.Controller.Conta -= precoCompra;
        GameController.Controller.QntTorre++;
        InvokeRepeating("BuscaAlvo", 0, velocidadeDisparo);
    }
}