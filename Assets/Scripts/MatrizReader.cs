using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class MatrizReader : MonoBehaviour
{
	//public  ArrayList route = new ArrayList();
	string line;
	public static int [,] routes;
	string [] numbers;
    int[,] test;
	public int rows, columns;

	// Use this for initialization
	void Start()
	{
		ReadString (line);//llamado al metodo read
    }

	[MenuItem("Tools/Read file")]
	 void ReadString(string line)
	{
		
		string path = "Assets/Resources/test.txt";
		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
		line = reader.ReadLine ();
		/*
		 * Bucle para contar filas y columnas
		 * 
		 */
		columns = line.Length;
		while(line != null){
			rows++;
			line = reader.ReadLine ();
		}
		routes = new int[rows, columns];
		reader.Read ();
		AssetDatabase.ImportAsset(path);
		Debug.Log(reader.ReadToEnd());
		reader.Close();
		/*TERMINA BUCLE PARA CONTAR FILAS Y COLUMNAS
		 * 
		 *
		 *INICIA
		 *Bucle para llenar la matriz
		 */
		//j columnas e i filas
		int i = 0, j = 0;
		reader = new StreamReader(path);
		line = reader.ReadLine ();
		while(line != null){
			//convierte string line a caracteres
            char[] lineArray = line.ToCharArray();
			for(j=0; j < line.Length; j++){
				//llena la matriz con casteo de char a entero 
				//si no se pone el -´0' toma el valor del ascii de 1 y cero (48 y 49)
				routes [i,j] = lineArray[j] - '0';
			}
			i++;
			line = reader.ReadLine ();
		}
		reader.Read ();
		AssetDatabase.ImportAsset(path);
		Debug.Log(reader.ReadToEnd());
		reader.Close();

	}

	public int [,]getRoutes(){

		return routes;
	}

}
