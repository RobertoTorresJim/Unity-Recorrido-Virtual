using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {
	public GameObject chips;
	public GameObject bottle;
	public GameObject magazine;
	public GameObject spawnPosition;
	private GameObject spawn;


	//metodo que aparece un objeto en la mano derecha del personaje
	public void spawnItem(string item){
		switch (item) {
		case("Magazine"):
			//si spawn es null tiene la mano vacía y puede agarrar un objeto, así aseguramos que solo agarre uno a la vez
			if (spawn == null) {
				spawn = Instantiate (magazine, spawnPosition.GetComponent<Transform> ().position, 
					spawnPosition.GetComponent<Transform> ().rotation, spawnPosition.GetComponent<Transform> ());
			}
				break;

				// crea un aleatorio para elegir que aparecer entre bolsa de papas o botella, de la maquina de dulces
		case("Snack"):
			if (spawn == null) 
			{
				int randomObject = (int)Random.Range (0, 2);
				if (randomObject == 0) {
					spawn = Instantiate (bottle, spawnPosition.GetComponent<Transform> ().position, 
						spawnPosition.GetComponent<Transform> ().rotation, spawnPosition.GetComponent<Transform> ());
				} 
				else {

					spawn = Instantiate (chips, spawnPosition.GetComponent<Transform> ().position, 
						spawnPosition.GetComponent<Transform> ().rotation, spawnPosition.GetComponent<Transform> ());	
				}
			}	
			break;
			//cuando se "tira" el objeto al bote de basura nuevamente puede agarrar uno nuevo
			case("Trash"):
				Destroy (spawn);
				break;

			}

	}


}
