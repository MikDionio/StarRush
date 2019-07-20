using System.Collections;
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
    private bool isExploding = false;

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

        /*if (this.transform.position.y < -5 && !isExploding)
        {
            isExploding = true;
            deactivate();
        }*/
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
            //rotation = Quaternion.Euler(Vector3.Lerp(this.transform.eulerAngles, new Vector3(0.0f,0.0f,540.0f), rotateSpeed));
            if(this.transform.rotation.eulerAngles.z < 540f)
            {
                rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, this.transform.rotation.eulerAngles.z + rotateSpeed));
            }
        }
        else
        {
            //Update rotation
            //rotation = Quaternion.Euler(Vector3.Lerp(this.transform.eulerAngles, new Vector3(0.0f, 0.0f, 0.0f), rotateSpeed));
            /*if (this.transform.rotation.eulerAngles.z >= 0f)
            {
                rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, this.transform.rotation.eulerAngles.z - rotateSpeed));
            }*/
            rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, this.transform.rotation.eulerAngles.z - rotateSpeed));
        }

        this.transform.SetPositionAndRotation(this.transform.position, rotation);
    }

    public void move()
    {
        this.transform.Translate(Vector3.up*Time.deltaTime*moveSpeed);
    }

    public void deactivate()
    {
        explosion_effect.transform.position = this.transform.position;
        thruster_flame.SetActive(false);
        thruster_smoke.SetActive(false);
        StartCoroutine("explode");
        //this.gameObject.SetActive(false);
    }

    IEnumerator explode()
    {
        
        explosion_effect.Play();
        explosion_sound.Play();
        this.rocketSprite.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
        isExploding = false;
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().stopGame();
    }
}
