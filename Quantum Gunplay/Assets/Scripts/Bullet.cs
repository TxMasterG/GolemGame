using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public int team = 1;
    public int damage = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.team != team)
            {
                player.Damage(damage);
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the bullet 
        Destroy(gameObject);

    }
}
