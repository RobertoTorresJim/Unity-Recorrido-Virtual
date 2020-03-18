using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.8f; //Permite caminar al personaje 

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;//Origen del ray 
		float m_TurnAmount; //Cantidad de giro
		float m_ForwardAmount; //Cantidad de movimiento hacia enfrente
		Vector3 m_GroundNormal; //Vector normal hacia el piso
		float m_CapsuleHeight;  //Altura del collider 
		Vector3 m_CapsuleCenter; //Centro geometrico del collider
		CapsuleCollider m_Capsule; //Collider
        //Parametros recibidos para reproducir la animacion
        bool m_Sit;
        bool m_hold;


        void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
            //Congela la rotacion del rigidbody del personaje ****************** xD 
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;
            
        }

        //Metodo que recibe las desiciones del Control de usuario
		public void Move(Vector3 move,bool sit, bool hold, int selection)
		{
            
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
            // Codigo que permite mover el personaje actuliza la posicion
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

            //Permite girar al personaje
			ApplyExtraTurnRotation();

			// Control que veifica si puedo moverme o no
			if (m_IsGrounded)
			{
				HandleGroundedMovement(sit, hold, selection);
                
			}
			else
			{
				HandleAirborneMovement(sit); //Estoy sentado 
			}

			

			// send input and other state parameters to the animator
			UpdateAnimator(move);// envia parametros para la maquina de estados

		}

		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			m_Animator.SetBool("OnGround", m_IsGrounded);
            
            if (m_IsGrounded)
            {
                m_Animator.SetBool("OnSit", m_Sit);
                m_Animator.SetBool("OnHold", m_hold);

                //Para que la animacion solo pase una vez
                m_hold = false;
               
                
            }
			
		}

        void HandleAirborneMovement(bool sit)
		{
			
            if (!sit && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Sit"))
            {
                
                m_Sit = false;
                m_Capsule.center = new Vector3(m_Capsule.center.x, 0.4f, 0.0f);
                m_Capsule.radius = 0.18f;
                m_Capsule.height = 0.8f;
            }
        }


        void HandleGroundedMovement(bool sit, bool hold, int selection)
        {
            
            if (sit && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                m_Sit = true;
                m_Capsule.center = new Vector3(m_Capsule.center.x, 0.45f, 0.25f);
                m_Capsule.radius = 0.06f;
                m_Capsule.height = 0.5f;
                
            }
            if (hold && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                m_hold = true;

            }
            
        }

            void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (m_IsGrounded && Time.deltaTime > 0)
			{
                //Modifica el vector de posicion del personaje con la informacion del vector de posicion de la animacion 
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y; 
				m_Rigidbody.velocity = v;
			}
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
                //Me puedo mover tocando el piso
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				m_Animator.applyRootMotion = true;
			}
			else
			{
                //Ya no me puedo mover sin tocar el piso 
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
				m_Animator.applyRootMotion = false;
			}
		}
	}
}
