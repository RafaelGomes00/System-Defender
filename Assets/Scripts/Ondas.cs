using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ondas : MonoBehaviour
{
    public GameObject[] inimigo;
    public Transform[] localSpawn;
    public float tempoSpawn;
    private int contUnidades = 0;
    public Ondas proximaOnda;
    // Start is called before the first frame update
    public void IniciaOnda(int tempoEspera = 0)
    {
        InvokeRepeating("GeraUnidade", tempoEspera, tempoSpawn);
    }
    public void GeraUnidade()
    {
        Transform spawn = SpawnAleatorio(localSpawn);
        Instantiate(inimigo[contUnidades], spawn.position, spawn.rotation);
        contUnidades++;
        if (contUnidades == inimigo.Length)
        {
            CancelInvoke("GeraUnidade");
            GameController.Controller.QntOndas--;
            if (proximaOnda != null)
            {
                proximaOnda.IniciaOnda(5);
            }
        }
    }

    private Transform SpawnAleatorio(Transform[] spawn)
    {
        System.Random r = new System.Random();

        return spawn[r.Next(0, spawn.Length - 1)];
    }
}
