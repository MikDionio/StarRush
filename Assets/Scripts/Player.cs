﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float strength;
    [SerializeField] float rotateSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject rocketSprite;
    [SerializeField] ParticleSystem explosion_effect;
    [SerializeField] GameObject thruster_flame;
    [SerializeField] GameObject thruster_smoke;
    [SerializeField] AudioSource explosion_sound;

    private Quaternion rotation;
    public Rigidbody2D rb;

    private void Awake()
    {
        rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void OnEnable()
    {
        this.rocketSprite.SetActive(true);
        this.thruster_flame.SetActive(true);
        this.thruster_smoke.SetActive(true);
    }

    private void FixedUpdate()
    {
        goUp();
    }

    void goUp()
    {
        if (Input.GetButton("Jump"))
        {
            //Update force
            //rb.AddForce(new Vector2(0.0f, strength));

            //Update rotation
            rotation = Quaternion.Euler(Vector3.Lerp(this.transform.eulerAngles, new Vector3(0.0f,0.0f,360.0f), rotateSpeed));
            this.transform.SetPositionAndRotation(this.transform.position, rotation);
        }
        else
        {
            //Update rotation
            rotation = Quaternion.Euler(Vector3.Lerp(this.transform.eulerAngles, new Vector3(0.0f, 0.0f, 180.0f), rotateSpeed));
        }

        this.transform.SetPositionAndRotation(this.transform.position, rotation);
    }

    public void move()
    {
        this.transform.Translate(Vector3.up*Time.deltaTime*moveSpeed);
    }

    public void deactivate()
    {
        StartCoroutine("explode");
        //this.gameObject.SetActive(false);
    }

    IEnumerator explode()
    {
        explosion_effect.transform.position = this.transform.position;
        explosion_effect.Play();
        explosion_sound.Play();
        this.rocketSprite.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
        //GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        //gameController.GetComponent<GameController>().stopGame();
    }
}