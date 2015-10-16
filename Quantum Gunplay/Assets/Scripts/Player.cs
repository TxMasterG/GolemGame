using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    protected Rigidbody2D rb2d;
    public Weapon weapon;
	protected int moveSpeed = 3;
	protected float turnSpeed = 10;
	protected int health = 10;
    public int team = 1;
	private ArrayList writeInputs;
	protected bool init = false;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		writeInputs = new ArrayList();
		GetComponent<SpriteRenderer> ().sortingLayerName = "Player";
		weapon.SendMessage("Initialize", team);
    }

	void Activate()
	{
		init = true;
	}
	
	void Initialize( int teamInit )
	{
		team = teamInit;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (init) {
			ArrayList tickInput = new ArrayList();
			float xVel = Input.GetAxis("Horizontal");
			float yVel = Input.GetAxis("Vertical");
			rb2d.velocity = new Vector2(xVel * moveSpeed, yVel * moveSpeed);
			tickInput.Add(xVel);
			tickInput.Add(yVel);
			
			Vector2 mousePos = Input.mousePosition;
			Vector2 playerPos = Camera.main.WorldToScreenPoint(transform.localPosition);
			Vector2 offset = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y);
			float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			tickInput.Add(angle);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed);
			
			if (Input.GetKeyDown("e"))
			{
				
			}
			
			if (Input.GetMouseButtonDown(0))
			{
				weapon.Fire();
				tickInput.Add(1);
			}
			else
			{
				tickInput.Add(0);
			}
			
			writeInputs.Add(tickInput);
		}
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

	public ArrayList getInputs()
	{
		return writeInputs;
	}

    public virtual void Die()
    {
		init = false;
		GameController.instance.SendMessage("PlayerDeath", team);
    }
}
