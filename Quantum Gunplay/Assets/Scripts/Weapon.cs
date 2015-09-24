using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Bullet bullet;
    public int bulletSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fire()
    {
        Bullet newBullet = (Bullet)Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 angle = transform.eulerAngles;
        newBullet.transform.eulerAngles = angle;
        Vector3 forceVector = newBullet.transform.right;
        newBullet.GetComponent<Rigidbody2D>().AddForce(forceVector * bulletSpeed * 1.5f, ForceMode2D.Impulse);
    }
}
