using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position.x - player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        trackPlayer();
    }

    public void trackPlayer()
    {
        this.transform.position = new Vector3(player.transform.position.x + offset, 0.0f, -10f);
    }
}
