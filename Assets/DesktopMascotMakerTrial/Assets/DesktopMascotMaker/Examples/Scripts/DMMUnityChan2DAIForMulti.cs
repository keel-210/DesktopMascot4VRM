using UnityEngine;
using System.Collections;
using System.Windows.Forms;
using DesktopMascotMaker;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class DMMUnityChan2DAIForMulti : MonoBehaviour
{
    // If you want to use this script with MascotMaker,
    // Comment out the following line and replace 'mascotMakerMulti' to 'MascotMaker.Instance'.
    public MascotMakerMulti mascotMakerMulti; // MascotMakerMulti's instance

	public float maxSpeed = 10f;
	public float jumpPower = 1000f;

	public LayerMask whatIsGround;

	private Animator m_animator;
	private BoxCollider2D m_boxcollier2D;
	private Rigidbody2D m_rigidbody2D;
	private bool m_isGround;
	private const float m_centerY = 1.5f;

	private float moveX;
	private bool jump;

	public Vector2 offset = Vector2.zero;

	void Awake()
	{
        if (mascotMakerMulti == null)
            Debug.LogError("mascotMakerMulti is null. Please assign Muscot Maker Multi component.", transform);

		m_animator = GetComponent<Animator>();
		m_boxcollier2D = GetComponent<BoxCollider2D>();
		m_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		// Set Event
        mascotMakerMulti.Left = 100;
        mascotMakerMulti.Top = 100;

        // Start AI
        StartCoroutine(Routine_AI());
    }

    IEnumerator Routine_AI()
    {
        while (true)
        {
            // determine moveX speed
            moveX = UnityEngine.Random.Range(-1, 2); // return -1 or 0 or 1 

            // determine jump or not
            int randVal = UnityEngine.Random.Range(0, 100);
            if (randVal < 50)
                jump = true; // do jump 50% probability
            else
                jump = false;

            float duration = UnityEngine.Random.Range(1.5f, 3.0f);
            yield return new WaitForSeconds(duration);
        }
    }

	void Update() //LateUpdate()
	{
        Move();

        // update mascot position
        mascotMakerMulti.Left = (int)(transform.position.x * 20 + offset.x);
        mascotMakerMulti.Top = (int)(transform.position.y * -35 + offset.y);
	}

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
    }

	void Move()
	{
		if (Mathf.Abs(moveX) > 0)
		{
			Quaternion rot = transform.rotation;
			transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(moveX) == 1 ? 0 : 180, rot.z);
		}

		m_rigidbody2D.velocity = new Vector2(moveX * maxSpeed, m_rigidbody2D.velocity.y);

		m_animator.SetFloat("Horizontal", moveX);
		m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
		m_animator.SetBool("isGround", m_isGround);

		if (jump && m_isGround)
		{
			jump = false;
			m_animator.SetTrigger("Jump");
			SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
			m_rigidbody2D.AddForce(Vector2.up * jumpPower);
		}
	}
}
