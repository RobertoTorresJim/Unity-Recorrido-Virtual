using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        //Desicion para hacer una accion
        private bool m_Sit;
        private bool m_hold;
        //Definen si estoy en la zona de accion (Collider)
        private bool holdZone;
        private bool sitZone;
        //Decide que aparecer objeto o desaparecer objeto
        private int selection;

        //Metodo para inicializar parametros (Solo se le una vez al poner play)
        private void Start()
        {

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }
        //Funciones para colisiones con collider tipo trigger( que se pueden atravesar)
        private void OnTriggerEnter(Collider other)
        {
            //Verifica que Collider fue atravesado 
            if (other.tag == "Bench")
            {
                sitZone = true;
                selection = 0;
                return;
            }
            if (other.tag == "SnackMachine")
            {
                holdZone = true;
                selection = 1;
                return;
            }
            if (other.tag == "Rack")
            {
                holdZone = true;
                selection = 2;
                return;
            }
            if (other.tag == "TrashCan")
            {
                holdZone = true;
                selection = 3;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Bench")
            {
                sitZone = false;
                selection = 0;
                return;
            }
            if (other.tag == "SnackMachine" || other.tag == "Rack" || other.tag == "TrashCan")
            {
                holdZone = false;
                selection = 0;
                return;
            }
        }
        //Funcion que se lee en cada fotogramas del juego (60 fotogramas por segundo)
        private void Update()
        {
            //Verifica si se presiono el boton para una accion
            if (!m_Sit && sitZone)
            {
                m_Sit = CrossPlatformInputManager.GetButtonDown("Fire1"); //quiero sentarme

            }
            else if (m_Sit) //Si ya estoy sentado entonces puedo levantarme
            {
                m_Sit = !CrossPlatformInputManager.GetButtonDown("Fire1"); // quiero levantarme  levanto

            }
            if (holdZone)
            {
                m_hold = CrossPlatformInputManager.GetButtonDown("Fire1"); //Quiero agarrar algo
                if (m_hold)
                {
                    switch (selection)
                    {
                        //se hace una llamada al script que crea un objeto y lo pone en la mano
                        //pasandole como valor el item que se desea aparecer dependiendo el caso
                        case 1:
                            this.SendMessage("spawnItem", "Snack");
                            break;

                        case 2:
                            this.SendMessage("spawnItem", "Magazine");
                            break;

                        case 3:
                            //se destruye el objeto si se acerca a los botes de basura
                            this.SendMessage("spawnItem", "Trash");
                            break;
                    }
                }
            }

        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");


            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, m_Sit, m_hold, selection);

            m_hold = false;
        }
    }
}
