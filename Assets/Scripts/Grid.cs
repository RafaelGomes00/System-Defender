using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject plano, pai;
    public int qntLargura = 10, qntComprimento = 10;

    private bool[,] disponibilidade;
    private GameObject[,] grid;
    private int gridX, gridZ;
    private GameObject torre;

    public int GridX
    {
        get
        {
            return gridX;
        }
    }

    public int GridZ
    {
        get
        {
            return gridZ;
        }
    }

    public void ConfereFileiraOcupada()
    {
        int conta;
        for (int z = 0; z < qntComprimento; z++)
        {
            conta = 0;
            for (int x = 0; x < qntLargura; x++)
            {
                if (!disponibilidade[x, z])
                {
                    conta++;
                }
            }

            if (conta == qntLargura)
            {
                GameController.Controller.TemFileiraOcupada = true;
                break;
            }

            else
            {
                GameController.Controller.TemFileiraOcupada = false;
            }
        }
    }

    public Transform[] FileiraOcupada()
    {
        int conta;
        Transform[] ocupada = new Transform[qntLargura];
        for(int z = 0; z<qntComprimento; z++)
        {
            conta = 0;
            for (int x = 0; x< qntLargura; x++)
            {
                if (!disponibilidade[x, z])
                {
                    conta++;
                    ocupada[x] = grid[x, z].transform;
                }
            }

            if (conta == qntLargura)
            {
                GameController.Controller.TemFileiraOcupada = true;
                break;
            }

            else
            {
                GameController.Controller.TemFileiraOcupada = false;
            }
        }
        return ocupada;
    }

    public GameObject Torre
    {
        set
        {
            torre = value;
        }
        get
        {
            return torre;
        }
    }

    public void SetDisponbilidade(int x, int z, string tag)
    {
        if (tag == "Firewall")
        {
            disponibilidade[x, z] = true;
        }

        else
        {
            if (x + 1 == qntLargura)
            {
                if (z + 1 == qntComprimento)
                {
                    disponibilidade[x, z] = true;
                    disponibilidade[x - 1, z] = true;
                    disponibilidade[x, z - 1] = true;
                    disponibilidade[x - 1, z - 1] = true;
                }

                else
                {
                    disponibilidade[x, z] = true;
                    disponibilidade[x - 1, z] = true;
                    disponibilidade[x, z + 1] = true;
                    disponibilidade[x - 1, z + 1] = true;
                }
            }

            else
            {
                if (z + 1 == qntComprimento)
                {
                    disponibilidade[x, z] = true;
                    disponibilidade[x + 1, z] = true;
                    disponibilidade[x, z - 1] = true;
                    disponibilidade[x + 1, z - 1] = true;
                }
                else
                {
                    disponibilidade[x, z] = true;
                    disponibilidade[x + 1, z] = true;
                    disponibilidade[x, z + 1] = true;
                    disponibilidade[x + 1, z + 1] = true;
                }
            }
        }
    }
    public void SetDisponibilidade()
    {
        if (torre.CompareTag("Firewall"))
        {
            disponibilidade[gridX, gridZ] = false;
        }

        else
        {
            if (gridX + 1 == qntLargura)
            {
                if (gridZ + 1 == qntComprimento)
                {
                    disponibilidade[gridX, gridZ] = false;
                    disponibilidade[gridX - 1, gridZ] = false;
                    disponibilidade[gridX, gridZ - 1] = false;
                    disponibilidade[gridX - 1, gridZ - 1] = false;
                }

                else
                {
                    disponibilidade[gridX, gridZ] = false;
                    disponibilidade[gridX - 1, gridZ] = false;
                    disponibilidade[gridX, gridZ + 1] = false;
                    disponibilidade[gridX - 1, gridZ + 1] = false;
                }
            }

            else
            {
                if (gridZ + 1 == qntComprimento)
                {
                    disponibilidade[gridX, gridZ] = false;
                    disponibilidade[gridX + 1, gridZ] = false;
                    disponibilidade[gridX, gridZ - 1] = false;
                    disponibilidade[gridX + 1, gridZ - 1] = false;
                }
                else
                {
                    disponibilidade[gridX, gridZ] = false;
                    disponibilidade[gridX + 1, gridZ] = false;
                    disponibilidade[gridX, gridZ + 1] = false;
                    disponibilidade[gridX + 1, gridZ + 1] = false;


                }
            }
        }
    }

    public bool RetornaDisponibilidade()
    {
        bool retorno = true;

        if (torre.CompareTag("Firewall"))
        {
            if (disponibilidade[gridX, gridZ])
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }

        else
        {
            if (gridX + 1 == qntLargura)
            {
                if (gridZ + 1 == qntComprimento)
                {
                    if (disponibilidade[gridX, gridZ] && disponibilidade[gridX - 1, gridZ] && disponibilidade[gridX, gridZ - 1] && disponibilidade[gridX - 1, gridZ - 1])
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }
                }

                else
                {
                    if (disponibilidade[gridX, gridZ] && disponibilidade[gridX - 1, gridZ] && disponibilidade[gridX, gridZ + 1] && disponibilidade[gridX - 1, gridZ + 1])
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }
                }
            }

            else
            {
                if (gridZ + 1 == qntComprimento)
                {
                    if (disponibilidade[gridX, gridZ] && disponibilidade[gridX + 1, gridZ] && disponibilidade[gridX, gridZ - 1] && disponibilidade[gridX + 1, gridZ - 1])
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }

                }
                else
                {
                    if (disponibilidade[gridX, gridZ] && disponibilidade[gridX + 1, gridZ] && disponibilidade[gridX, gridZ + 1] && disponibilidade[gridX + 1, gridZ + 1])
                    {
                        retorno = true;
                    }
                    else
                    {
                        retorno = false;
                    }
                }
            }

            return retorno;
        }
    }

    public void PlanoAtual(Vector3 posicao)
    {
        for (int x = 0; x < qntLargura; x++)
        {
            for(int z = 0; z<qntComprimento; z++)
            {
                if (grid[x, z].transform.position == posicao)
                {
                    gridX = x;
                    gridZ = z;
                }
            }
        }
    }

    public Transform[] ProximosPlanos(Vector3 posicao)
    {
        Transform[] proxPlano = new Transform[4];
        for (int x = 0; x < qntLargura; x++)
        {
            for (int z = 0; z < qntComprimento; z++)
            {
                if (grid[x, z].transform.position == posicao)
                {
                    if (x + 1 == qntLargura)
                    {
                        if (z + 1 == qntComprimento)
                        {
                            proxPlano[0] = grid[x, z].transform;
                            proxPlano[1] = grid[x - 1, z].transform;
                            proxPlano[2] = grid[x, z - 1].transform;
                            proxPlano[3] = grid[x - 1, z - 1].transform;

                            gridX = x;
                            gridZ = z;

                        }

                        else
                        {
                            proxPlano[0] = grid[x, z].transform;
                            proxPlano[1] = grid[x - 1, z].transform;
                            proxPlano[2] = grid[x, z + 1].transform;
                            proxPlano[3] = grid[x - 1, z + 1].transform;

                            gridX = x;
                            gridZ = z;

                        }
                    }

                    else
                    {
                        if (z + 1 == qntComprimento)
                        {
                            proxPlano[0] = grid[x, z].transform;
                            proxPlano[1] = grid[x + 1, z].transform;
                            proxPlano[2] = grid[x, z - 1].transform;
                            proxPlano[3] = grid[x + 1, z - 1].transform;

                            gridX = x;
                            gridZ = z;

                        }
                        else
                        {
                            proxPlano[0] = grid[x, z].transform;
                            proxPlano[1] = grid[x + 1, z].transform;
                            proxPlano[2] = grid[x, z + 1].transform;
                            proxPlano[3] = grid[x + 1, z + 1].transform;

                            gridX = x;
                            gridZ = z;
                        }
                    }
                    break;
                }
            }
        }

        return proxPlano;
    }
    private void Awake()
    {
        GameController.Controller.grid = this;
        //gerando uma matriz que salvará as informações dos planos que compõem a grid
        disponibilidade = new bool[qntLargura, qntComprimento];
        grid = new GameObject[qntLargura, qntComprimento];
        //pegando a escala do plano
        float escPlano = plano.transform.lossyScale.x;

        //loop que gera os planos da grid
        for (int x = 0; x < qntLargura; x++)
        {
            for (int z = 0; z < qntComprimento; z++)
            {

                GameObject gridPlane = (GameObject)Instantiate(plano, this.transform.position, this.transform.rotation, this.transform.parent);
                gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + escPlano * x * 10,
                    gridPlane.transform.position.y,
                    gridPlane.transform.position.z + escPlano * z * 10);
                //acima o plano é gerado e sua posição é ajustada de acordo com a escala do plano.

                gridPlane.transform.SetParent(pai.transform);
                grid[x, z] = gridPlane;
                disponibilidade[x, z] = true;
                //na primeira linha é gerado um parentesco para o plano e depois ele é salvo na matriz, assim como é gravada sua disponibilidade.

            }
        }
    }
}
