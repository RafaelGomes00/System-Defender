using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Text conta, lugarOcupado, dinheiroInsuficiente, compraTorre;
    public Image vida, telaDerrota, telaVitoria;
    public Camera cam;
    public GameObject avc, amaste, maisCafe, bidu;

    private Text temp;
    private float tempo = 0;
    private bool contaTempo = false;

    private void QualAntiVirus()
    {
        if(GameController.Controller.AntiVirus == "MaisCafe")
        {
            maisCafe.SetActive(true);
        }

        else if (GameController.Controller.AntiVirus == "Bidu")
        {
            bidu.SetActive(true);
        }

        else if (GameController.Controller.AntiVirus == "AVC")
        {
            avc.SetActive(true);
        }

        else if (GameController.Controller.AntiVirus == "Amaste")
        {
            amaste.SetActive(true);
        }
    } 
    public void DestroiTorre()
    {
        GameController.Controller.DestroiTorre();
    }
    public void MensagensErro(string erro)
    {
        if(erro == "lugar ocupado")
        {
            contaTempo = true;
            lugarOcupado.gameObject.SetActive(true);
            temp = lugarOcupado;
        }

        if(erro == "dinheiro insuficiente")
        {
            contaTempo = true;
            dinheiroInsuficiente.gameObject.SetActive(true);
            temp = dinheiroInsuficiente;
        }

        if (erro == "compra torre")
        {
            contaTempo = true;
            compraTorre.gameObject.SetActive(true);
            temp = compraTorre;
        }
    }
    public void Derrota()
    {
        telaDerrota.gameObject.SetActive(true);
    }

    public void Vitoria()
    {
        telaVitoria.gameObject.SetActive(true);
    }
    public void AtualizaConta(int valor)
    {
        conta.text = "R$ " + valor;
    }

    public void AtualizaVida(float valor)
    {
        vida.fillAmount = valor/10000;
    }

    private void Start()
    {
        GameController.Controller.cam = cam;
        GameController.Controller.uiController = this;
        AtualizaConta(GameController.Controller.conta);
        QualAntiVirus();
    }
    private void Update()
    {
        if (contaTempo)
        {
            tempo += Time.deltaTime;
            if (tempo >= 2)
            {
                contaTempo = false;
                tempo = 0;
                temp.gameObject.SetActive(false);
            }
        }
    }
    public void CarregaCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

}
