using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	private Transform panelPrincipal;//panel principal que se movera a la izquierda
	private Transform targetPrincipal; //posicion a la que se movera el panel





	// Use this for initialization
	void Start () {
		panelPrincipal = this.transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		//posicion a la que se moveran los paneles
		if (targetPrincipal != null) {
			panelPrincipal.position = Vector3.Lerp (new Vector3 (panelPrincipal.position.x, panelPrincipal.position.y, 
				panelPrincipal.position.z), targetPrincipal.position, 3 * Time.deltaTime);
		}

		
	}
	//movimiento de los paneles del menu
	public void MovePanelPrincipal(Transform targetPosition){
		targetPrincipal = targetPosition;

	}
}
