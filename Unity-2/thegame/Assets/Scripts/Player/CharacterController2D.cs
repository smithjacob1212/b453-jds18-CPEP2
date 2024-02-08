using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using FMODUnity;
[RequireComponent(typeof(Animator))]

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] public Animator animator;
	[SerializeField] public StudioEventEmitter jumping;
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private GameObject jumpEffect;                            // Jump effect
	[SerializeField] private float toonTime = 0.1f;									// Time while the player can jump after falling of a cliff
	[SerializeField] private float t_toonTime = 0f;									
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] public Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] public Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded = true;            // Whether or not the player is grounded.
	public bool m_OnOtherPlayer = false;            // Whether or not the player is grounded.
	public bool isMoving;				// Whether or not the player is moving.
	public bool isFlipping;            // Whether or not the player is flipping.
	public float t_doNotCheckGround = 0f;            // Whether or not the player is flipping.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void Update()
	{
		bool wasGrounded = m_Grounded;
		if (wasGrounded == true)
        {
			t_toonTime += Time.deltaTime;
			if (t_toonTime > toonTime)
            {
				m_Grounded = false;
				t_toonTime = 0f;
            }
        }
		isMoving = false;
		m_OnOtherPlayer = false;
			CheckGround(wasGrounded);

	}

	private void CheckGround(bool wasGrounded)
    {
		t_doNotCheckGround = 0f;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
                {
					Debug.Log("On Ground");
					animator.SetBool("isJumping", false);
					OnLandEvent.Invoke();
				}

			}
		}
	}

    internal void SetLayerWeight(int fromIndex, int layerInAnimator, int weight)
    {
		if (animator == null) animator = GetComponent<Animator>();
		Debug.Log("From layer " + fromIndex + " to Layer " + layerInAnimator);
		animator.SetLayerWeight(0, 0);
		animator.SetLayerWeight(1, 0);
		animator.SetLayerWeight(2, 0);
		animator.SetLayerWeight(3, 0);
		animator.SetLayerWeight(layerInAnimator, weight);
    }

    public void Move(float move, bool crouch, bool jump, bool isShortJump = false)
	{
		animator.SetFloat("speed", Mathf.Abs(move));

		isMoving = move != 0;
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			Jump();
		}
		// Gestion du short jump
		if (!m_Grounded && !jump && isShortJump && m_Rigidbody2D.velocity.y > 0)
		{
			m_Rigidbody2D.velocity = Vector2.Lerp(
				m_Rigidbody2D.velocity,
				new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y / 10),
				Time.deltaTime
				);
		}
	}

	private void Jump()
    {
		animator.SetBool("isJumping", true);
		Debug.Log("Jump !");
		// Add a vertical force to the player.
		RuntimeManager.PlayOneShot(jumping.EventReference);
		m_Grounded = false;
		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		Instantiate(jumpEffect, transform.position, transform.rotation);
	}


	private void Flip()
	{
		StartCoroutine(IsFlipping());
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public IEnumerator IsFlipping()
    {
		isFlipping = true;
		yield return new WaitForEndOfFrame();
		isFlipping = false;
    }

	public Rigidbody2D GetRigidbody2D()
    {
		return m_Rigidbody2D;
    }

    private void Start()
    {
		animator = GetComponent<Animator>();
	}
}