using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MatrizMaker : MonoBehaviour
{
    Transform myPosition;
    public Terrain terrain;
    public float myHeight;
    public float myWidth;
    public LayerMask MyMask;
    public float timeToGo;
    public int x, z;
    bool onContact;
    
    //corners
    Vector3 leftDown;
    Vector3 rightDown;
    Vector3 rightUp;
    Vector3 leftUp;

    string line;


    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 1.0f;
        myPosition = this.GetComponent<Transform>();
        onContact = false;
        myHeight = terrain.terrainData.size.z;
        myWidth = terrain.terrainData.size.x;
        line = "";
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Actualizacion de la posicion inicial de cada una de las esquinas 
        leftDown = new Vector3(myPosition.position.x - 0.5f, myPosition.position.y, myPosition.position.z - 0.5f);
        rightDown = new Vector3(myPosition.position.x + 0.5f, myPosition.position.y, myPosition.position.z - 0.5f);
        leftUp = new Vector3(myPosition.position.x - 0.5f, myPosition.position.y, myPosition.position.z + 0.5f);
        rightUp = new Vector3(myPosition.position.x + 0.5f, myPosition.position.y, myPosition.position.z + 0.5f);

        if (isOnContact(leftDown, rightDown) || isOnContact(rightDown, rightUp) || isOnContact(rightUp, leftUp)
            || isOnContact(leftUp, leftDown) || isOnContact(leftDown, rightUp) || isOnContact(rightDown, leftUp)
            || isOnContact(leftDown + new Vector3(0, 0, 0.5f), rightDown + new Vector3(0, 0, 0.5f))
            || isOnContact(leftDown + new Vector3(0.5f, 0, 0), leftUp + new Vector3(0.5f, 0, 0))
            || isOnContact(myPosition.position + myPosition.up * 10, myPosition.position))
        {
            
            onContact = true;
        }
        /*
         * Lineas Exteriores
         */
        //Debug.Log(onContact);
        if (!onContact)
        {
            Debug.DrawLine(leftDown, rightDown, new Color(0, 255, 0, 255));
            Debug.DrawLine(rightDown, rightUp, new Color(0, 255, 0, 255));
            Debug.DrawLine(rightUp, leftUp, new Color(0, 255, 0, 255));
            Debug.DrawLine(leftUp, leftDown, new Color(0, 255, 0, 255));
            /*
             * Lineas interiores
             */
            Debug.DrawLine(leftDown, rightUp, new Color(0, 255, 0, 255));
            Debug.DrawLine(rightDown, leftUp, new Color(0, 255, 0, 255));
            Debug.DrawLine(leftDown + new Vector3(0.5f, 0, 0), leftUp + new Vector3(0.5f, 0, 0), new Color(0, 255, 0, 255));
            Debug.DrawLine(leftDown + new Vector3(0, 0, 0.5f), rightDown + new Vector3(0, 0, 0.5f), new Color(0, 255, 0, 255));
            /*
             * Linea Vertical
             */
            Debug.DrawLine(myPosition.position, myPosition.position + myPosition.up*10, new Color(0, 255, 0, 255));
            line = line + "1";
        }
        else
        {
            onContact = Physics.Linecast(leftDown, rightDown);
            Debug.DrawLine(leftDown, rightDown, new Color(255, 0, 0, 255));
            Debug.DrawLine(rightDown, rightUp, new Color(255, 0, 0, 255));
            Debug.DrawLine(rightUp, leftUp, new Color(255, 0, 0, 255));
            Debug.DrawLine(leftUp, leftDown, new Color(255, 0, 0, 255));
            /*
             * Lineas interiores
             */
            Debug.DrawLine(leftDown, rightUp, new Color(255, 0, 0, 255));
            Debug.DrawLine(rightDown, leftUp, new Color(255, 0, 0, 255));
            Debug.DrawLine(leftDown + new Vector3(0.5f, 0, 0), leftUp + new Vector3(0.5f, 0, 0), new Color(255, 0, 0, 255));
            Debug.DrawLine(leftDown + new Vector3(0, 0, 0.5f), rightDown + new Vector3(0, 0, 0.5f), new Color(255, 0, 0, 255));
            /*
             * Linea Vertical
             */
            Debug.DrawLine(myPosition.position + myPosition.up * 10, myPosition.position, new Color(255, 0, 0, 255));
            line = line + "0";
        }
        //if (Time.fixedTime >= timeToGo)
        //{
            //writeString(onContact);
            if (!onMovement())
            {
                DestroyObject(this);
            }
          //  timeToGo = Time.fixedTime + 1.0f;
        //}
    }

    bool isOnContact(Vector3 start, Vector3 end)
    {
        return Physics.Linecast(start, end);

    }

    bool onMovement()
    {
        if (x < 412) //myWidth)
        {
            this.GetComponent<Transform>().position = new Vector3(0, 0.7f, 0) + new Vector3(x, 0, z);
            x++;
            //Debug.Log(x);
            return true;
        }
        else if (z < 397) // myHeight - 1)
        {
            z++;
            x = 0;
           // Debug.Log(x);
            WriteString(line);
            line = "";
            return true;
        }
        return false;
    }

    void writeString(bool onContact)
    {
        if (onContact)
        {
            line += "0";
            Debug.Log("0");
        }
        else
        {
            line += "1";
            Debug.Log("1");
        }
    }

    [MenuItem("Tools/Write file")]
    static void WriteString(string line)
    {
        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(line);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file
       // Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
       // Debug.Log(reader.ReadToEnd());
        reader.Close();

    }

}
