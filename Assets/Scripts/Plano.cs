using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plano : MonoBehaviour
{
    public Material matNormal, matDisponivel, matIndisponivel;
    private Grid grid;
    private Transform[] planos;
    private UIController uiController;

    //Método para retornar o ponto médio entre 4 planos.
    private Vector3 MediaPlanos(Transform[] pos)
    {
        float x = (pos[0].position.x + pos[1].position.x + pos[2].position.x + pos[3].position.x) / 4;
        float y = (pos[0].position.y + pos[1].position.y + pos[2].position.y + pos[3].position.y) / 4;
        float z = (pos[0].position.z + pos[1].position.z + pos[2].position.z + pos[3].position.z) / 4;

        Vector3 posFinal = new Vector3(x, y, z);
        return posFinal;
    }

    //OnMouseDown colocará, efetivamente, as torres no jogo
    private void OnMouseDown()
    {     
            if (grid.Torre.CompareTag("Firewall"))
            {
                if (grid.Torre.GetComponent<Firewall>().precoCompra <= GameController.Controller.conta)
                {
                   if (grid.RetornaDisponibilidade())
                   {
                       GameObject t = Instantiate(grid.Torre, this.transform.position, Quaternion.identity);
                       t.GetComponent<Firewall>().PosXPress = grid.GridX;
                       t.GetComponent<Firewall>().PosZPress = grid.GridZ;
                       t.GetComponent<Firewall>().Grid = grid;

                       grid.SetDisponibilidade();
                       grid.gameObject.SetActive(false);
                       this.GetComponent<Renderer>().material = matNormal;

                   }
                   else
                   {
                       uiController.MensagensErro("lugar ocupado");
                   }
                }

               else
               {
                   uiController.MensagensErro("dinheiro insuficiente");
               }

            }

            else
            {
                if (grid.Torre.GetComponent<Torre>().precoCompra <= GameController.Controller.conta)
                {
                    //É checada a disponibilidade dos planos, se disponíveis a torre é setada, do contrário uma mensagem de "ocupado" é mostrada
                    if (grid.RetornaDisponibilidade())
                    {
                        Vector3 pos = MediaPlanos(planos);

                        planos[0].gameObject.GetComponent<Renderer>().material = matNormal;
                        planos[1].gameObject.GetComponent<Renderer>().material = matNormal;
                        planos[2].gameObject.GetComponent<Renderer>().material = matNormal;
                        planos[3].gameObject.GetComponent<Renderer>().material = matNormal;

                        GameObject t = Instantiate(grid.Torre, pos, this.transform.rotation);
                        t.GetComponent<Torre>().PosXPress = grid.GridX;
                        t.GetComponent<Torre>().PosZPress = grid.GridZ;
                        t.GetComponent<Torre>().Grid = grid;

                        grid.SetDisponibilidade();
                        grid.gameObject.SetActive(false);

                    }
                    else
                    {
                        uiController.MensagensErro("lugar ocupado");
                    }
                }

                else
                {
                   uiController.MensagensErro("dinheiro insuficiente");
                }
        }
    }

    //OnMouseEnter usado para realizar um preview de onde a torre irá ser posicionada
    private void OnMouseEnter()
    {
        if (grid.Torre.CompareTag("Firewall"))
        {
            grid.PlanoAtual(this.transform.position);

            if (grid.RetornaDisponibilidade())
            {
                this.GetComponent<Renderer>().material = matDisponivel;
            }

            else
            {
                this.GetComponent<Renderer>().material = matIndisponivel;
            }
        }

        else
        {
            planos = grid.ProximosPlanos(this.transform.position);

            //Aqui é verificado se as torres adjacentes ao plano atual estão livres. Se sim, ficam verdes. Se não, ficam vermelhos.
            if (grid.RetornaDisponibilidade())
            {
                planos[0].gameObject.GetComponent<Renderer>().material = matDisponivel;
                planos[1].gameObject.GetComponent<Renderer>().material = matDisponivel;
                planos[2].gameObject.GetComponent<Renderer>().material = matDisponivel;
                planos[3].gameObject.GetComponent<Renderer>().material = matDisponivel;
            }
            else
            {
                planos[0].gameObject.GetComponent<Renderer>().material = matIndisponivel;
                planos[1].gameObject.GetComponent<Renderer>().material = matIndisponivel;
                planos[2].gameObject.GetComponent<Renderer>().material = matIndisponivel;
                planos[3].gameObject.GetComponent<Renderer>().material = matIndisponivel;
            }
        }
    }

    //OnMouseExit foi usado para fazer os planos retornarem ao material normal
    private void OnMouseExit()
    {
        if (grid.Torre.CompareTag("Firewall"))
        {
            this.gameObject.GetComponent<Renderer>().material = matNormal;
        }
        else
        {
            planos[0].gameObject.GetComponent<Renderer>().material = matNormal;
            planos[1].gameObject.GetComponent<Renderer>().material = matNormal;
            planos[2].gameObject.GetComponent<Renderer>().material = matNormal;
            planos[3].gameObject.GetComponent<Renderer>().material = matNormal;
        }
    }

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
    }
}