﻿using UnityEngine;
using System.Collections;

public class Clone : Player
{
	protected ArrayList readInputs = new ArrayList();
	protected int index = 0;
	protected bool init = false;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		GetComponent<SpriteRenderer> ().sortingLayerName = "Player";
		weapon.SendMessage("Initialize", team);
	}

	void Activate(ArrayList inputs)
    {
        readInputs = inputs;
        init = true;
    }

	void Initialize( int teamInit )
	{
		team = teamInit;
	}

    // Update is called once per frame
	void FixedUpdate()
    {
        if( index < readInputs.Count )
        {
            ArrayList inputTick = (ArrayList)readInputs[index];

            float xVel = (float)inputTick[0];
            float yVel = (float)inputTick[1];
            rb2d.velocity = new Vector2(xVel * moveSpeed, yVel * moveSpeed);
            float angle = (float)inputTick[2];
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed);

            if (inputTick[3].Equals(1))
            {
                weapon.Fire();
            }

            index++;
        }
        else if( init )
        {
            this.gameObject.SetActive(false);
        }

    }

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }
}