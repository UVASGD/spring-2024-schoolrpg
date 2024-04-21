using UnityEngine;

namespace SchoolRPG.Character.Runtime
{
    /// <summary>
    /// Base component the movement for all characters. 
    /// </summary>
    public class CharacterMovementComponent : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="Rigidbody2D"/>.
        /// </summary>
        [field: SerializeField, Tooltip("The Rigidbody2D.")]
        public Rigidbody2D Rb { get; protected set; }

        /// <summary>
        /// The normalized movement direction this frame. 
        /// </summary>
        public Vector2 MovementDirection { get; protected set; }

        /// <summary>
        /// The amount to move by. 
        /// </summary>
        [field: SerializeField, Tooltip("The amount to move by.")]
        public float Speed { get; set; } = 2f;

        /// <summary>
        /// The actual amount that we are moving this frame.
        /// </summary>
        private Vector2 Velocity { get; set; }
    
        /// <summary>
        /// The factor by which <see cref="MovementDirection"/> decreases each frame. 
        /// </summary>
        /// <remarks>
        /// 1 is the maximum amount of friction applied (the player won't move).
        /// </remarks>
        [field: SerializeField, Range(0f, 1f), Tooltip("The factor by which " + nameof(MovementDirection) + " decreases each frame.")]
        public float FrictionFactor { get; set; }

        /// <summary>
        /// If true, no movement will occur or be updated. 
        /// </summary>
        [field: SerializeField, Tooltip("If true, no movement will occur or be updated.")]
        private bool IsActive { get; set; }

        protected void Start()
        {
            IsActive = true; 
        }
        
        /// <summary>
        /// Moves the character in this direction. 
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        public void Move(Vector2 direction)
        {
            if (!IsActive) return;
            MovementDirection = direction; 
        }

        /// <summary>
        /// Sets <see cref="IsActive"/> to true. 
        /// </summary>
        public void Activate() => IsActive = true; 
    
        /// <summary>
        /// Sets <see cref="IsActive"/> to false. 
        /// </summary>
        public void Deactivate() => IsActive = false; 

        // execute on fixed timer, better for physics (default 50 times/sec)
        protected void FixedUpdate()
        {
            if (!IsActive) return;
            Velocity += MovementDirection * (Speed * Time.fixedDeltaTime); 
            Rb.MovePosition(Rb.position + Velocity); 
            Velocity *= 1 - FrictionFactor; 
        }
    
    }
}