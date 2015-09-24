using UnityEngine;
using System.Collections;

public class Clone : Player
{
    private ArrayList readInputs = new ArrayList();
    private int index = 0;
    private bool init = false;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void TheStart(ArrayList inputs)
    {
        readInputs = inputs;
        init = true;
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

    new public void Die()
    {
        this.gameObject.SetActive(false);
    }
}
