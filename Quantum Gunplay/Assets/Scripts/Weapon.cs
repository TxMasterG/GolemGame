using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	protected int team = 1;
	protected int bulletSpeed = 10;
	protected float fireDelay = 0.5f;
	protected float lastShot = 0.0f;
	protected bool init = false;


	// Use this for initialization
	void Start () {
		lastShot = Time.time;
	}

	void Initialize(int teamInit)
	{
		team = teamInit;
		init = true;
	}

    public void Fire()
    {
		if (init && Time.time > lastShot) {
			lastShot = Time.time + fireDelay;
			GameObject newBullet = (GameObject)Instantiate(Resources.Load("Bullet"), transform.position, Quaternion.identity);
			Vector3 angle = transform.eulerAngles;
			newBullet.transform.eulerAngles = angle;
			Vector3 forceVector = newBullet.transform.right;
			newBullet.SendMessage("Initialize", team);
			newBullet.GetComponent<Rigidbody2D>().AddForce(forceVector * bulletSpeed * 1.5f, ForceMode2D.Impulse);
		}
    }
}
