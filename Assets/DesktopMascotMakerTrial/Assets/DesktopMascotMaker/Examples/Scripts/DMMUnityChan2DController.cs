using System.Collections;
using System.Windows.Forms;
using DesktopMascotMaker;
using UnityEngine;

[RequireComponent (typeof (Animator), typeof (Rigidbody2D), typeof (BoxCollider2D))]
public class DMMUnityChan2DController : MonoBehaviour
{
	// If you want to use this script with MascotMakerMulti,
	// Uncomment the following line and replace 'MascotMaker.Instance' to 'mascotMakerMulti'.
	//public MascotMakerMulti mascotMakerMulti; // Assign MascotMakerMulti's instance to this variable.

	public float maxSpeed = 10f;
	public float jumpPower = 1000f;
	public Vector2 backwardForce = new Vector2 (-20.0f, 15.0f);

	public LayerMask whatIsGround;

	private Animator m_animator;
	private BoxCollider2D m_boxcollier2D;
	private Rigidbody2D m_rigidbody2D;
	[SerializeField]
	private bool m_isGround;
	private const float m_centerY = 1.5f;

	private State m_state = State.Normal;

	private float moveX;
	[SerializeField]
	private bool jump;

	public Vector2 offset;
	private float mouseDownPosX;
	private float mouseDownPosY;

	private bool leftMouseDown = false;

	void Awake ()
	{
		m_animator = GetComponent<Animator> ();
		m_boxcollier2D = GetComponent<BoxCollider2D> ();
		m_rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void Start ()
	{
		// Set Event
		/* MascotMaker.Instance.OnKeyDown += KeyDown;
		MascotMaker.Instance.OnKeyUp += KeyUp;
		MascotMaker.Instance.OnLeftMouseDown += LeftMouseDown;
		MascotMaker.Instance.OnLeftMouseUp += LeftMouseUp; */
	}

	void Update ()
	{
		if (m_state != State.Damaged)
		{
			Move ();
		}

		if (leftMouseDown)
		{
			offset.x = MascotMaker.Instance.Left - transform.position.x * 20;
			offset.y = MascotMaker.Instance.Top - transform.position.y * (-50);
		}
		else
		{
			//MascotMaker.Instance.Location = new System.Drawing.Point((int)(transform.position.x * 20 + offset.x), (int)(transform.position.y * -50 + offset.y)); // You can also set the Position like this
			MascotMaker.Instance.Left = (int)(transform.position.x * 20 + offset.x);
			MascotMaker.Instance.Top = (int)(transform.position.y * -50 + offset.y);
		}
	}

	void Move ()
	{
		if (Mathf.Abs (moveX)> 0)
		{
			Quaternion rot = transform.rotation;
			transform.rotation = Quaternion.Euler (rot.x, Mathf.Sign (moveX)== 1 ? 0 : 180, rot.z);
		}

		m_rigidbody2D.velocity = new Vector2 (moveX * maxSpeed, m_rigidbody2D.velocity.y);

		m_animator.SetFloat ("Horizontal", moveX);
		m_animator.SetFloat ("Vertical", m_rigidbody2D.velocity.y);
		m_animator.SetBool ("isGround", m_isGround);

		if (jump && m_isGround)
		{
			jump = false;
			m_animator.SetTrigger ("Jump");
			SendMessage ("Jump", SendMessageOptions.DontRequireReceiver);
			m_rigidbody2D.AddForce (Vector2.up * jumpPower);
		}
	}

	void LeftMouseDown (object sender, MouseEventArgs e)
	{
		mouseDownPosX = e.X;
		mouseDownPosY = e.Y;

		leftMouseDown = true;
	}
	void LeftMouseUp (object sender, MouseEventArgs e)
	{
		offset.x += (e.X - mouseDownPosX);
		offset.y += (e.Y - mouseDownPosY);

		leftMouseDown = false;
	}

	void KeyDown (object sender, KeyEventArgs e)
	{
		if (e.KeyCode == System.Windows.Forms.Keys.X || e.KeyCode == System.Windows.Forms.Keys.Right)
			moveX = 1.0f;

		if (e.KeyCode == System.Windows.Forms.Keys.Z || e.KeyCode == System.Windows.Forms.Keys.Left)
			moveX = -1.0f;

		if (m_isGround && e.KeyCode == System.Windows.Forms.Keys.Space)
			jump = true;
	}

	void KeyUp (object sender, KeyEventArgs e)
	{
		if (e.KeyCode == System.Windows.Forms.Keys.X || e.KeyCode == System.Windows.Forms.Keys.Right)
			moveX = 0.0f;

		if (e.KeyCode == System.Windows.Forms.Keys.Z || e.KeyCode == System.Windows.Forms.Keys.Left)
			moveX = 0.0f;
	}

	void FixedUpdate ()
	{
		Vector2 pos = transform.position;
		Vector2 groundCheck = new Vector2 (pos.x, pos.y - (m_centerY * transform.localScale.y));
		Vector2 groundArea = new Vector2 (m_boxcollier2D.size.x * 0.49f, 0.05f);

		m_isGround = Physics2D.OverlapArea (groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
		m_animator.SetBool ("isGround", m_isGround);
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "DamageObject" && m_state == State.Normal)
		{
			m_state = State.Damaged;
			StartCoroutine (INTERNAL_OnDamage ());
		}
	}

	IEnumerator INTERNAL_OnDamage ()
	{
		m_animator.Play (m_isGround ? "Damage" : "AirDamage");
		m_animator.Play ("Idle");

		SendMessage ("OnDamage", SendMessageOptions.DontRequireReceiver);

		m_rigidbody2D.velocity = new Vector2 (transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);

		yield return new WaitForSeconds (1.0f);

		while (m_isGround == false)
		{
			yield return new WaitForFixedUpdate ();
		}
		m_animator.SetTrigger ("Invincible Mode");
		m_state = State.Invincible;
	}

	void OnFinishedInvincibleMode ()
	{
		m_state = State.Normal;
	}

	enum State
	{
		Normal,
		Damaged,
		Invincible,
	}
}