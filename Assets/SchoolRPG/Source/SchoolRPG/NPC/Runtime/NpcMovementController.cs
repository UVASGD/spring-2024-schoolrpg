using SchoolRPG.Source.SchoolRPG.Character.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace SchoolRPG.NPC.Runtime
{
    public class NpcMovementController : MonoBehaviour
    {
        /// <summary>
        /// The agent to follow
        /// </summary>
        [field: SerializeField]
        public Transform Target { get; set; }

        /// <summary>
        /// The <see cref="CharacterMovementComponent"/> that controls movement.
        /// </summary>
        [field: SerializeField] 
        public CharacterMovementComponent CharacterMovementComponent { get; set; }

        [SerializeField]
        private Vector3 movementDirection; 
        
        private void Update()
        {
            if (Target == null) return;
            
            var path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, Target.position, NavMesh.AllAreas, path);
            movementDirection = path.status == NavMeshPathStatus.PathComplete
                ? (path.corners[1] - transform.position).normalized
                : Vector3.zero;
            
            CharacterMovementComponent.Move(movementDirection);
        }

        private void OnDrawGizmos()
        {
            var path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, Target.position, NavMesh.AllAreas, path);
            for (var i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i], path.corners[i+1]);
            }
        }
    }
}
