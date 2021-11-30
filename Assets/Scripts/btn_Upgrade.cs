using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_Upgrade : MonoBehaviour
{
    public UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Upgrade()
    {

        if (GameController.Controller.TorreSelecionada.upgrade!= null)
        {
            if (GameController.Controller.TorreSelecionada.upgrade.GetComponent<Torre>().precoCompra < GameController.Controller.Conta)
            {
                Instantiate(GameController.Controller.TorreSelecionada.upgrade, GameController.Controller.TorreSelecionada.transform.position, GameController.Controller.TorreSelecionada.transform.rotation);
                Destroy(GameController.Controller.TorreSelecionada.gameObject);
            }
            else
            {
                uiController.MensagensErro("dinheiro insuficiente");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(GameController.Controller.TorreSelecionada != null)
        {
            this.GetComponent<Button>().interactable = true;
        }

        else
        {
            this.GetComponent<Button>().interactable = false;
        }
    }
}
