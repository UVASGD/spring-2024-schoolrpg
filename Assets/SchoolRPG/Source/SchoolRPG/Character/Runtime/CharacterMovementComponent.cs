using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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

        [field: SerializeField, Tooltip("The Animator Component of the Player.")]
        private Animator animator;

        private string currentAnimation;

        // could be an enum but idrc
        const string PLAYER_UP = "PlayerUp";
        const string PLAYER_DOWN = "PlayerDown";
        const string PLAYER_LEFT = "PlayerLeft";
        const string PLAYER_RIGHT = "PlayerRight";
        const string PLAYER_IDLE_UP = "PlayerIdleUp";
        const string PLAYER_IDLE_DOWN = "PlayerIdleDown";
        const string PLAYER_IDLE_RIGHT = "PlayerIdleRight";
        const string PLAYER_IDLE_LEFT = "PlayerIdleLeft";

        private static CharacterMovementComponent instance = null;
        void Awake()
        {
            List<string> cutscenes = new List<string> { "Final Scene", "Letter Scene", "Hospital" };
            if (cutscenes.Contains(SceneManager.GetActiveScene().name))
            {
                Destroy(gameObject);
                Debug.Log("Destroyed player");
            } else
            {
                if (instance == null)
                {
                    DontDestroyOnLoad(gameObject);
                    instance = this;
                }
                else if (instance != null)
                {
                    Destroy(gameObject);
                }
            }
            
        }

        protected void Start()
        {
            IsActive = true;
            ChangeAnimationState(PLAYER_IDLE_DOWN);
        }
        
        /// <summary>
        /// Moves the character in this direction. 
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        public void Move(Vector2 direction)
        {
            if (!IsActive) return;

            if (direction.normalized == Vector2.left)
            {
                ChangeAnimationState(PLAYER_LEFT);
            }else if (direction.normalized == Vector2.right)
            {
                ChangeAnimationState(PLAYER_RIGHT);
            }else if (direction.normalized == Vector2.up)
            {
                ChangeAnimationState(PLAYER_UP);
            }else if(direction.normalized == Vector2.down)
            {
                ChangeAnimationState(PLAYER_DOWN);
            }

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

            if (Velocity == Vector2.zero)
            {
                if (currentAnimation == PLAYER_UP)
                {
                    ChangeAnimationState(PLAYER_IDLE_UP);
                }
                else if (currentAnimation == PLAYER_DOWN)
                {
                    ChangeAnimationState(PLAYER_IDLE_DOWN);
                }
                else if (currentAnimation == PLAYER_LEFT)
                {
                    ChangeAnimationState(PLAYER_IDLE_LEFT);
                }
                else if (currentAnimation == PLAYER_RIGHT)
                {
                    ChangeAnimationState(PLAYER_IDLE_RIGHT);
                }
            }
        }

        // animation changer function, ensures no animation fighting
        void ChangeAnimationState(string newAnimation)
        {
            if (currentAnimation == newAnimation) return;

            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }

    }
}