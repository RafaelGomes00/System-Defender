using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class GameController : MonoBehaviour
{
    static public GameController Controller;
    public float volume = 1;
    public int conta = 1000;
    public Grid grid;
    public float vida = 10000;
    public Camera cam;
    public UIController uiController;

    private int qntOndas = 5;
    private bool temFileiraOcupada = false;
    private GameObject torre;
    private Torre torreSelecionada = null;
    private int qntTorre = 0;
    public int qntInimigo = 0;
    private int qntFirewall = 0;
    private string antiVirus;

    public string AntiVirus
    {
        get
        {
            return antiVirus;
        }

        set
        {
            antiVirus = value;
        }
    }
    public int QntOndas
    {
        get
        {
            return qntOndas;
        }

        set
        {
            qntOndas = value;
        }
    }
    public GameObject Torre
    {
        get
        {
            return torre;
        }

        set
        {
            torre = value;
        }
    } 

    public int QntFirewall
    {
        get
        {
            return qntFirewall;
        }

        set
        {
            if (qntInimigo > 0)
            {
                GameObject[] inimigo = GameObject.FindGameObjectsWithTag("Inimigo");
                for (int i = 0; i < inimigo.Length; i++)
                {
                    Vector3 v3 = GameController.Controller.Destino(inimigo[i].transform.position);
                    inimigo[i].GetComponent<Inimigo>().agente.SetDestination(v3);
                    inimigo[i].GetComponent<Inimigo>().Destino = v3;
                }
            }
            qntFirewall = value;
        }
    }

    public int QntInimigo
    {
        get
        {
            return qntInimigo;
        }
        set
        {
            qntInimigo = value;
        }
    }

    public int QntTorre
    {
        get
        {
            return qntTorre;
        }
        set
        {
            qntTorre = value;
        }
    }

    public bool TemFileiraOcupada
    {
        get
        {
            return temFileiraOcupada;
        }
        set
        {
            temFileiraOcupada = value;
        }
    }

    public float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            if (value >= 0 && value <= 1)
            {
                volume = value;
            }
        }
    }

    public Torre TorreSelecionada
    {
        get
        {
            return torreSelecionada;
        }
        set
        {
            if (value != null)
            {
                torreSelecionada = value;
            }
        }
    }

    public int Conta
    {
        get
        {
            return conta;
        }
        set
        {
            if (value >= 0)
            {
                conta = value;
                uiController.AtualizaConta(conta);
            }
        }
    }

    public float Vida
    {
        get
        {
            return vida;
        }
        set
        {
                vida = value;
                uiController.AtualizaVida(vida);
            if (vida <= 0)
            {
                uiController.Derrota();
            }
        }
    }

    public void DestroiTorre()
    {
        if (torreSelecionada != null)
        {
            Conta += torreSelecionada.precoVenda;
            Destroy(torreSelecionada.gameObject);
            torreSelecionada = null;
        }
    }

    public Vector3 Destino(Vector3 pos)
    {
        Vector3 maisProx = this.transform.position;
        float distancia = float.MaxValue;
        grid.ConfereFileiraOcupada();

        if (GameController.Controller.TemFileiraOcupada || GameController.Controller.QntTorre > 0)
        {
            if (GameController.Controller.QntTorre > 0)
            {
                GameObject[] torres = GameObject.FindGameObjectsWithTag("Torre");
                for (int cont = 0; cont < torres.Length; cont++)
                {
                    if (Vector3.Distance(pos, torres[cont].transform.position) < distancia)
                    {
                        maisProx = torres[cont].transform.position;
                        distancia = Vector3.Distance(pos, torres[cont].transform.position);
                    }
                }
            }

            if (GameController.Controller.TemFileiraOcupada)
            {
                Transform[] firewalls = grid.FileiraOcupada();
                for (int cont = 0; cont < firewalls.Length; cont++)
                {
                    if (Vector3.Distance(pos, firewalls[cont].transform.position) < distancia)
                    {
                        maisProx = firewalls[cont].transform.position;
                        distancia = Vector3.Distance(pos, firewalls[cont].transform.position);
                    }
                }
            }

            if (Vector3.Distance(maisProx, pos) < Vector3.Distance(this.transform.position, pos))
            {
                return maisProx;
            }
            else
            {
                return this.transform.position;
            }
        }

        else
        {
            return this.transform.position;
        }
    }

    public void CarregaCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
    public void Sair()
    {
        Application.Quit();
    }

    void Start()
    {
        Controller = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hitInfo;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hitInfo))
            {
                if (torreSelecionada != null)
                {
                    if (hitInfo.transform.position != torreSelecionada.transform.position && hitInfo.transform.gameObject.CompareTag("Torre"))
                    {
                        torreSelecionada.material1.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                        torreSelecionada.material2.GetComponent<Renderer>().material.shader = Shader.Find("Standard");

                        torreSelecionada = hitInfo.transform.gameObject.GetComponent<Torre>();
                        torreSelecionada.material1.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Highlight");
                        torreSelecionada.material2.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Highlight");
                    }
                    else if (hitInfo.transform.position != torreSelecionada.transform.position && !hitInfo.transform.gameObject.CompareTag("Torre"))
                    {
                        torreSelecionada.material1.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                        torreSelecionada.material2.GetComponent<Renderer>().material.shader = Shader.Find("Standard");

                        torreSelecionada = null;
                    }
                }

                else
                {
                    if (hitInfo.transform.gameObject.CompareTag("Torre"))
                    {
                        torreSelecionada = hitInfo.transform.gameObject.GetComponent<Torre>();

                        torreSelecionada.material1.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Highlight");
                        torreSelecionada.material2.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Highlight");
                    }
                }
            }
        }
    }
}
