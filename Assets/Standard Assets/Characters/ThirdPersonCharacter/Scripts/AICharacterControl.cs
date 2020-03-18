using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        Vector2 myPosition;
        Vector2 nextPosition;
        ArrayList route;
        int i = 0;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, 
                    //false, false, 
                    false, false, 0);
            else
                character.Move(Vector3.zero, 
                    //false, false, 
                    false, false, 0);

            myPosition = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.z);
            if (Vector2.Distance(myPosition, nextPosition) < 0.5f &&  i  < route.Count - 1)
            {
                i += 1;
                createTarget(i);

            }
            }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void setRoute(ArrayList route)
        {
            this.route = route;

            createTarget(0);

        }
        public void createTarget(int i)
        {
            GameObject nextPositionAux = new GameObject();
            nextPositionAux.GetComponent<Transform>().position = (Vector3)route[i];
            nextPosition = new Vector2(nextPositionAux.GetComponent<Transform>().position.x, nextPositionAux.GetComponent<Transform>().position.z);
            SetTarget(nextPositionAux.GetComponent<Transform>());
        }
    }
}
