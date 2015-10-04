using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	protected Rigidbody2D rb2d;
    public int team = 1;
	protected float damage = 3;
	protected float fallOff = 0.1f;
	protected bool init = false;

	void Initialize(int teamInit)
	{
		team = teamInit;
		init = true;
	}

    // Update is called once per frame
    void Update()
    {
		damage -= fallOff;
		if( damage <= 0 )
			Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
		else if (init && other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.team != team)
            {
                player.Damage((int)Mathf.Ceil(damage));
				Destroy(gameObject);
            }
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the bullet 
        Destroy(gameObject);

    }
}
