using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
   // CharacterController
   // -- Main script of the Player --
   // here you can find all about the player - movement, attack, damage, lifepoints, shield, etc..
   // this script is the heart of the player
   //----------------------------------------------------------


   //SerializeField is for UnityInterface - edit field for the variables
   [SerializeField] private Vector3 spawn = new Vector3(-6, -3, 0);           // Spawn Point when LP <= 0
	[SerializeField] private float m_JumpForce = 400f;							      // Jump strength of the player
   // Player
   [SerializeField] private int m_LifePoints = 125;                           // lp - default 125 
   [SerializeField] public int m_maxLP = 125;
   [SerializeField] public int m_shieldPoints = 0;                            //shield points zum start auf 0
   [SerializeField] public int m_maxshield = 125;                             // limit of shield points
   [SerializeField] public int m_MeleeDamage = 50;
   //Movement
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// maxSpeed at Crouch movement, 1=100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							   // for movement(left,right) while jumping
   //
	[SerializeField] private LayerMask m_WhatIsGround;							      // LayerMask for checking Ground(you can move script data in)
	[SerializeField] private Transform m_GroundCheck;							      // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							      // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				   // A collider that will be disabled when crouching

	const float    k_GroundedRadius = .2f;             // Radius of the overlap circle to determine if grounded
	private bool   m_Grounded;                         // Whether or not the player is grounded.
	const float    k_CeilingRadius = .2f;              // Radius of the overlap circle to determine if the player can stand up
	private        Rigidbody2D m_Rigidbody2D;
   private        Animator m_Animator;
	private bool   m_FacingRight = true;               // For determining which way the player is currently facing.
	private        Vector3 m_Velocity = Vector3.zero;

   //for "disabled" mode after die
   SpriteRenderer spriteRenderer;
   public bool playerEnabled = true;

   //Attack
   public bool melee = false; // "C" Nahkampf
   public bool fire = false; // "V" Fernkampf
   // fire
   public int bulletDmg = 50;
   public int bulletLPremove = 20;
   // melee
   public int attackDamage = 50;
   public float attackRange = 0.5f;
   public float attackRate = 2f;
   public float nextAttackTime = 0f;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

   public BoolEvent OnCrouchEvent;
   private bool m_wasCrouching = false;

   void Start() 
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
   }
   public void Update() 
   {
      if (this.m_LifePoints <= 0)
      {
         SetPlayerDisabled();
         PlayerRespawn();
         // player state - death
      }

   }


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
      m_Animator    = GetComponent<Animator>();

      if (OnLandEvent == null)
         OnLandEvent = new UnityEvent();

      if (OnCrouchEvent == null)
         OnCrouchEvent = new BoolEvent();
	}
	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

     // m_Animator.SetBool("Ground", m_Grounded);
	}
	public void Move(float move, bool crouch, bool jump)
	{
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
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
      transform.Rotate(0f, 180f, 0f);
	}
   public void GetHealth(int health) // Added the Health to actual LifePoints
   {
      if(this.m_LifePoints <= m_maxLP)
      {
         this.m_LifePoints += health;
         if (this.m_LifePoints > m_maxLP) 
         {
            this.m_LifePoints = m_maxLP;
         }
      }
   }
   public void AddShieldPoints(int shield) 
   {
      if (this.m_shieldPoints >= this.m_maxshield) // if actual shieldPoints higher or the same as max shield
      {
         this.m_shieldPoints = this.m_maxshield;   // set shield to maximum if actual shield higher then max
         shield = 0;
      }
      this.m_shieldPoints += shield;         // added shield to player shieldpoints
   }
   public int ShieldProtection(int damage)
   {
      if (this.m_shieldPoints > 0) //if shield is higher then 0
      {
         if (this.m_shieldPoints < damage) // example 20 shield and 30 damage
         {
            damage -= this.m_shieldPoints;
            this.m_shieldPoints = 0;
         }
         if (this.m_shieldPoints > damage) // example 40 shield and 10 damage
         {
            this.m_shieldPoints -= damage;
         }
      }
      if (this.m_shieldPoints <= 0) // if no shield there - return normal damage
      {
         return damage;
      }
      if (damage < 0) // if damage after protection lower then 0 (-20) set to 0
      {
         damage = 0;
      }
      return damage; // new damage value after shield protection
      
   }
   public int GetLifePoints() // return the actual LifePoints of the Player
   {
      return this.m_LifePoints;       // actual lifepoints
   }
   public void PlayerRespawn() // Teleport the Player to Spawn Point and Reset the LifePoints
   {
         transform.position = spawn; // transform position of player to spawn
         m_LifePoints = m_maxLP;  // set LP up to 100
         SetPlayerEnabled();
   }
    public void Stop()
    {
		m_Rigidbody2D.velocity = Vector2.zero;
    }

   public void TakeDamage(int damage)
   {
      int actualDamage = 0;   // actual damage set to 0
      actualDamage = ShieldProtection(damage); // actual damage after shield protection
      if (m_shieldPoints == 0)
      {
         m_LifePoints -= actualDamage;
      }
      else 
      {
         Debug.Log("Sucessfull Dodge");
      }
   }
   public void MeleeAttack(bool melee) // Player Meele("c")
   {
      this.melee = melee;
   }
   public bool getMelee() 
   {
      return this.melee;
   }
   public void FireAttack(bool fire) 
   {
      this.fire = fire;
   }
   public bool getFireAttack() 
   {
      return this.fire;
   }
   public bool getPlayerState() 
   {
      return this.playerEnabled;
   }
   private void SetPlayerDisabled() 
   {
      playerEnabled = false;
      spriteRenderer.enabled = false; // "disabled mode"
   }
   private void SetPlayerEnabled() 
   {
      playerEnabled = true;
      spriteRenderer.enabled = true; // "disabled mode"
   }
}
