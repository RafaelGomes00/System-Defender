using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    public float energia = 20;
    public int precoCompra = 50;

    private int posXPress, posZPress;
    private Grid grid;


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
        GameController.Controller.QntFirewall--;
    }
    public void CausaDano(float dano)
    {
        energia -= dano;
        if (energia <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GameController.Controller.QntFirewall++;
        GameController.Controller.Conta -= precoCompra;
    }
}
