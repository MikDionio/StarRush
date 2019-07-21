using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        despawn();
    }

    void despawn()
    {
        if((player.transform.position.x - this.transform.position.x) > 32)
        {
            this.gameObject.SetActive(false);
        }
    }
}
