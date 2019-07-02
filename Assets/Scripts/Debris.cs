using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move();
        // deactivate();
    }

    void move()
    {
        this.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("collision!");
            //collision.gameObject.SetActive(false);
            destroyRocket(collision);
            //this.transform.parent.GetComponent<GameController>().stopGame();
        }
    }

    private static void destroyRocket(Collider2D collision)
    {
        collision.GetComponentInParent<Player>().deactivate();
    }

    void deactivate()
    {
        if(this.transform.position.x < -9.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
