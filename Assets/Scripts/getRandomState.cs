using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class getRandomState : MonoBehaviour {
	int rows,columns;
	public ArrayList validPositions;
	public ArrayList validFinalState;
	private Vector2 finalState;
	// Use this for initialization
	void Start () {
		//validPositions = new ArrayList();
		validFinalState = new ArrayList();
		validFinalState.Add (new Vector2 (45, 68)); //Maquina de dulces
		validFinalState.Add (new Vector2 (45, 44)); //Palapa1
		validFinalState.Add (new Vector2 (46, 44)); //Palapa1
		validFinalState.Add (new Vector2 (29, 35)); //Palapa2
		validFinalState.Add (new Vector2 (28, 33)); //Palapa2 
		validFinalState.Add (new Vector2 (36, 43)); //Palapa3
		validFinalState.Add (new Vector2 (16, 45)); //Palapa4
		validFinalState.Add (new Vector2 (6, 38));  //Bote de basura
		validFinalState.Add (new Vector2 (44, 48)); //Palapa5 
		validFinalState.Add (new Vector2 (42, 28)); //Bote de Basura
		validFinalState.Add (new Vector2 (46, 11)); //Revistero
		validFinalState.Add (new Vector2 (45, 7)); //Banca1
		validFinalState.Add (new Vector2 (45, 12)); //Banca2 
		validFinalState.Add (new Vector2 (29, 90));//Entrada Rectoria
		validFinalState.Add (new Vector2 (39, 106)); //Rectoria 
		validFinalState.Add (new Vector2 (46, 117));//Anuncios de rectoria
		validFinalState.Add (new Vector2 (26, 125)); //Cajas
		validFinalState.Add (new Vector2 (28, 125)); //Cajas2 
		validFinalState.Add (new Vector2 (25, 120)); //BancasRectoria 
		validFinalState.Add (new Vector2 (25, 117)); //BancasRectoria
        validFinalState.Add (new Vector2 (22, 107)); // Rectoria
		validFinalState.Add (new Vector2 (25, 162)); //Explanada
		validFinalState.Add (new Vector2 (27, 161)); //Explanada2
		validFinalState.Add (new Vector2 (18, 2)); //Biblioteca
		validFinalState.Add (new Vector2 (20, 2)); 
		validFinalState.Add (new Vector2 (34, 4)); 
		validFinalState.Add (new Vector2 (2, 2)); 
		validFinalState.Add (new Vector2 (50, 34)); 
		validFinalState.Add (new Vector2 (28, 33)); 
		validFinalState.Add (new Vector2 (21, 35));
		validPositions = validFinalState;
		rows = GameObject.Find ("MatrizReader").GetComponent<MatrizReader> ().rows;
		columns = GameObject.Find ("MatrizReader").GetComponent<MatrizReader> ().columns;
		
		Debug.Log ("las filas son: "+rows+" Las columnas: "+columns);

		//getRandomPosition();
		//getFinalState();
	}		
    /*
	public Vector2 getRandomPosition()
	{
		
		do {
			int randRow = (int)Random.Range (0.0f, rows + 1);
			Debug.Log ("Entre a la fila: " + randRow);
			for (int j = 0; j < columns; j++) {
				if (GameObject.Find ("MatrizReader").GetComponent<MatrizReader> ().getRoutes () [randRow, j] == 1) {
					validPositions.Add (new Vector2 (randRow, j));
				}
			}
		} while(validPositions == null);
		Debug.Log ("Tamaño del arrayList: " + validPositions.Count);
		int randPos = (int)Random.Range (0.0f, validPositions.Count);
		Debug.Log ("Imprime posicion: "+randPos);
		foreach (Vector2  uv in validPositions) {
			Debug.Log ("Posiciones validas: "+uv);
		}
		finalState = (Vector2)validPositions[randPos];
		Debug.Log ("Estado final: "+finalState);
		return finalState;
	}*/

	public Vector2 getFinalState()
	{
		int randVectorPos = (int)Random.Range(0.0f, validPositions.Count);
		Debug.Log("Tamaño lista de estados: "+validPositions.Count);
		Debug.Log("Posicion aleatoria: "+randVectorPos);
		finalState = (Vector2)validPositions [randVectorPos];
		validPositions.Remove (randVectorPos);
		Debug.Log ("Estado final: "+finalState);
		return finalState;
	}

	public void setFinalState(Vector2 state)
	{
		validPositions.Add (state);
	}
    public int getRows()
    {
        return rows;
    }
    public int getColumns()
    {
        return columns;
    }
}
