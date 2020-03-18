using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhatanDistance : MonoBehaviour
{

    Transform myPosition;
    Vector2 destino;
    int pos_x;
    int pos_z;
    int des_x, des_z;
    string direccion;
    //public string sigmov = "";
    ArrayList ruta;
    public bool debug;
    bool flag = true;


    //0 espacio ocupado, 1 espacio libre
    public int[,] matriz = new int[,]{{1,1,1,1,1,1,0,1,1,0},
                                          {0,1,1,0,0,0,1,1,1,0},
                                          {0,1,1,1,1,1,1,1,1,1},
                                          {0,0,1,0,1,0,1,1,0,0},
                                          {0,0,0,0,1,1,1,1,1,1},
                                          {1,1,0,0,1,1,0,0,1,1},
                                          {0,1,1,1,1,0,1,1,0,0},
                                          {0,1,0,0,1,1,1,1,1,1},
                                          {0,1,1,1,1,1,1,1,1,1},
                                          {0,0,1,0,1,0,1,1,0,0}};


    // Use this for initialization
    void Start()
    {
        myPosition = this.GetComponent<Transform>();
        pos_x = (int)myPosition.position.x-115;
        pos_z = (int)myPosition.position.z-212;
        if (pos_z > 90)
        {
            destino = new Vector2(29, 90);
        }
        else
        {
            destino = GetComponent<getRandomState>().getFinalState();
        }
        des_x = (int)destino.x;
        des_z = (int)destino.y;
        direccion = "Derecha";
        ruta = new ArrayList();
        Debug.Log("Mi Origen es:" + pos_x + ", " + pos_z);
        Debug.Log("Mi Destino Es: " + des_x + ", " + des_z);
        Debug.Log("Moviento mediante distancia de Manhattan");
        Debug.Log("Rows " + GetComponent<getRandomState>().getRows());
        Debug.Log("Columns " + GetComponent<getRandomState>().getColumns());

    }

    // Update is called once per frame
    void Update()
    {
        if (!debug)
        {
            if (pos_x != des_x || pos_z != des_z)
            {
                movimientoManhattan();
                ruta.Add(new Vector3(pos_x +115.5f, 1.0f, pos_z+212.5f));
                if(ruta.Count > 100)
                {
                    debug = true;
                }
                Debug.Log("Nueva posicion" + pos_x + " " + pos_z);
            }
            else
            {
                //nuestro punto de inicio

                Vector3 v = (Vector3)ruta[0];
                v = new Vector3(v.x, v.y + 1.5f, v.z);
                //creamos nuestro ray
                //Ray ray = new Ray(transform.position, v);
                //como el ray es una linea imaginaria dibujamos la linea de nuestro ray
                Debug.DrawLine(transform.position, v, Color.red);

                //creamos una lista para agregar ray 
                ArrayList listaR = new ArrayList();


                for (int i = 0; i < ruta.Count - 1; i++)
                {
                    //hacemos un casteo para la posicion inicial y final
                    Vector3 pnext = (Vector3)ruta[i + 1];
                    Vector3 plast = (Vector3)ruta[i];
                    pnext = new Vector3(pnext.x, pnext.y + 1.5f, pnext.z);
                    plast = new Vector3(plast.x, plast.y + 1.5f, plast.z);
                    //añadimos a la lista un ray con posicion inicial y direccion)
                    listaR.Add(new Ray(plast, pnext));
                    //de igual manera se dibuja una lineas que representa el ray
                    Debug.DrawLine(plast, pnext, Color.blue);

                }
                if (flag)
                {
                    this.SendMessage("setRoute", ruta);
                    flag = false;
                }
                
            }
        }
        else
        {
            //nuestro punto de inicio

            Vector3 v = (Vector3)ruta[0];
            v = new Vector3(v.x, v.y + 1.5f, v.z);
            //creamos nuestro ray
            //Ray ray = new Ray(transform.position, v);
            //como el ray es una linea imaginaria dibujamos la linea de nuestro ray
            Debug.DrawLine(transform.position, v, Color.red);

            //creamos una lista para agregar ray 
            ArrayList listaR = new ArrayList();


            for (int i = 0; i < ruta.Count - 1; i++)
            {
                //hacemos un casteo para la posicion inicial y final
                Vector3 pnext = (Vector3)ruta[i + 1];
                Vector3 plast = (Vector3)ruta[i];
                pnext = new Vector3(pnext.x, pnext.y + 1.5f, pnext.z);
                plast = new Vector3(plast.x, plast.y + 1.5f, plast.z);
                //añadimos a la lista un ray con posicion inicial y direccion)
                listaR.Add(new Ray(plast, pnext));
                //de igual manera se dibuja una lineas que representa el ray
                Debug.DrawLine(plast, pnext, Color.blue);

            }
            if (flag)
            {
                this.SendMessage("setRoute", ruta);
                flag = false;
            }

        }
    }


    //Distancia Manhattan
    double distanciaManhattan(int i, int j)
    {

        double dis_x = 0, dis_z = 0;

        if (des_x < (pos_x + i))
        { //revisa para que el resultado sea positivo
          // calcula la diferencia tomando en cuenta la direccion que se quiere tomar 
          //(i, representa eje x)
            dis_x = (pos_x + i) - des_x;
        }
        else
        {
            dis_x = des_x - (pos_x + i);
        }
        if (des_z < (pos_z + j))
        { //revisa para que el resultado sea positivo
          // calcula la diferencia tomando en cuenta la direccion que se quiere tomar 
          //(j, representa eje y)
            dis_z = (pos_z + j) - des_z;
        }
        else
        {
            dis_z = des_z - (pos_z + j);
        }
        Debug.Log(dis_x + dis_z);
        return dis_x + dis_z; //devuelve la dist de manhattan
    }


    //Validaciones manhattan para poder caminar con la euritica de manhattan
    void movimientoManhattan()
    {
        // X ES IGUAL A J
        // Z ES IGUAL A I
        //Se validara cada una de las rutas posibles para tomar una decision
        //Derecha
        if (pos_x < GetComponent<getRandomState>().getColumns()-1
            && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z, pos_x + 1] != 0
            && distanciaManhattan(1, 0) < distanciaManhattan(0, 0) && direccion != "Izquierda")
        {
            Debug.Log("Me puedo mover a la derecha");

            Debug.Log("Me movi a la derecha");
            pos_x = pos_x + 1;
            //ruta.Add(new Vector3(pos_x+1, 0.5f, pos_z));
            direccion = "Derecha";
        }
        else
        {
            Debug.Log("Valida izquierda");
            //Izquierda
            if (pos_x > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z, pos_x - 1] != 0
                && distanciaManhattan(-1, 0) < distanciaManhattan(0, 0) && direccion != "Derecha")
            {     //32                               31
                Debug.Log("Me puedo mover a la izquierda");

                Debug.Log("Me movi a la izquierda");
                pos_x = pos_x - 1;
                //ruta.Add(new Vector3(pos_x - 1, 0.5f, pos_z));
                direccion = "Izquierda";
            }
            else
            {
                //Arriba                                                                                                                                  5,3
                if (pos_z > 0  && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z - 1, pos_x] != 0
                    && distanciaManhattan(0, -1) < distanciaManhattan(0, 0) && direccion != "Abajo")
                {
                    Debug.Log("Me puedo mover a la arriba");

                    Debug.Log("Me movi arriba");
                    pos_z = pos_z - 1;
                    //ruta.Add(new Vector3(pos_x, pos_z - 1));
                    direccion = "Arriba";
                }
                else
                {
                    //Abajo
                    if (pos_z < GetComponent<getRandomState>().getRows() - 1
                        && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z + 1, pos_x] != 0 && direccion != "Arriba"
                        && (distanciaManhattan(0, 1) < distanciaManhattan(0, 0)))
                    {
                        Debug.Log("Me puedo mover a la abajo");

                        Debug.Log("Me movi abajo");
                        pos_z = pos_z + 1;
                        //ruta.Add(new Vector3(pos_x, pos_z + 1));
                        direccion = "Abajo";

                    }
                    else
                    {
                        switch (direccion)
                        {
                            case ("Derecha"):
                                {
                                    //Derecha
                                    if (pos_x < GetComponent<getRandomState>().getColumns() - 1
                                        && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z, pos_x + 1] != 0)
                                    {
                                        pos_x = pos_x + 1;
                                        //ruta.Add(new Vector3(pos_x + 1, 0.5f, pos_z));
                                        direccion = "Derecha";
                                    }//Arriba
                                    else
                                    {
                                        if (pos_z > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z - 1, pos_x] != 0)
                                        {
                                            pos_z = pos_z - 1;
                                            //ruta.Add(new Vector3(pos_x, 0.5f, pos_z - 1));
                                            direccion = "Arriba";
                                        }//abajo
                                        else
                                        {
                                            pos_z = pos_z + 1;
                                            //ruta.Add(new Vector3(pos_x, 0.5f, pos_z + 1));
                                            direccion = "Abajo";
                                        }
                                    }
                                    break;
                                }
                            case ("Izquierda"):
                                {
                                    Debug.Log("VOY A LA IZQUIERDA");
                                    //Izquierda
                                    if (pos_x > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z, pos_x - 1] != 0)
                                    {
                                        pos_x = pos_x - 1;
                                        //ruta.Add(new Vector3(pos_x - 1, pos_z));
                                        direccion = "Izquierda";
                                    }//Arriba
                                    else
                                    {
                                        if (pos_z > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z - 1, pos_x] != 0)
                                        {
                                            pos_z = pos_z - 1;
                                            //ruta.Add(new Vector3(pos_x, 0.5f, pos_z - 1));
                                            direccion = "Arriba";

                                        }//Abajo
                                        else
                                        {
                                            pos_z = pos_z + 1;
                                            //ruta.Add(new Vector3(pos_x, 0.5f, pos_z + 1));
                                            direccion = "Abajo";
                                        }
                                    }
                                    break;
                                }
                            case ("Arriba"):
                                {
                                    //Arriba
                                    if (pos_z > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z - 1, pos_x] != 0)
                                    {
                                        pos_z = pos_z - 1;
                                        //ruta.Add(new Vector3(pos_x, 0.5f, pos_z - 1));
                                        direccion = "Arriba";
                                    }//Izquierda
                                    else
                                    {
                                        if (pos_x > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z, pos_x - 1] != 0)
                                        {
                                            pos_x = pos_x - 1;
                                            //ruta.Add(new Vector3(pos_x - 1, pos_z));
                                            direccion = "Izquierda";
                                        }//Derecha
                                        else
                                        {
                                            pos_x = pos_x + 1;
                                            //ruta.Add(new Vector3(pos_x + 1, 0.5f, pos_z));
                                            direccion = "Derecha";
                                        }
                                    }
                                    break;
                                }
                            case ("Abajo"):
                                {
                                    //Abajo
                                    if (pos_z < GetComponent<getRandomState>().getRows() - 1
                                        && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z + 1, pos_x] != 0)
                                    {
                                        pos_z = pos_z + 1;
                                        //ruta.Add(new Vector3(pos_x, pos_z + 1));
                                        direccion = "Abajo";
                                    }//Izquierda
                                    else
                                    {
                                        if (pos_x > 0 && GameObject.Find("MatrizReader").GetComponent<MatrizReader>().getRoutes()[pos_z, pos_x - 1] != 0)
                                        {
                                            pos_x = pos_x - 1;
                                            //ruta.Add(new Vector3(pos_x - 1, pos_z));
                                            direccion = "Izquierda";
                                        }//Derecha
                                        else
                                        {
                                            pos_x = pos_x + 1;
                                            //ruta.Add(new Vector3(pos_x + 1, 0.5f, pos_z));
                                            direccion = "Derecha";
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }
}