using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

	PlayerInventory joueurIventaire;

    
    Animation animations;


    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
	public GameObject rayHit;

	 
	public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    
	public bool isDead = false;

    public float attackCooldown;
    public bool isAttacking;
    public float currentCoolDown;

	public float attackRange;

	public int hint;

    void Start () {
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
		joueurIventaire = gameObject.GetComponent<PlayerInventory> ();
		rayHit = GameObject.Find ("RayHit");
	}

    bool isGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.092f, layermask:3);
    }

    void Update () {

        if (!isDead) {
       

        if(Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift)) {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);

            if (!isAttacking)
            {
              animations.Play("walk");
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
               Attack();
            }
             
        }


        if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, runSpeed * Time.deltaTime);
            animations.Play("run");

        }


        if (Input.GetKey(inputBack))
        {
            transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                if (!isAttacking)
                {
                    animations.Play("walk");
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }


        if (Input.GetKey(inputLeft))
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime,0);
        }


        if (Input.GetKey(inputRight))
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime,0);
        }


        if (!Input.GetKey(inputBack) && !Input.GetKey(inputFront))
        {
                if (!isAttacking)
                {
                    animations.Play("idle");
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

 
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            v.y = jumpSpeed.y;


            gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
        }

        }

        if(isAttacking)
        {
            currentCoolDown -= Time.deltaTime;
        }

        if(currentCoolDown <= 0)
        {
            currentCoolDown = attackCooldown;
            isAttacking = false;
        }
    }

    public void Attack()
    {
		if (!isAttacking) 
		{
			animations.Play("attack");

			RaycastHit hit;

			if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection (Vector3.forward), out hit, attackRange))
			{
				Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

				if (hit.transform.tag == "Enemy") {

					hit.transform.GetComponent<gameEnemy> ().ApplyDamage (joueurIventaire.currentDamage);
				}	

			}

			isAttacking =  true;
		}    
	}
}
