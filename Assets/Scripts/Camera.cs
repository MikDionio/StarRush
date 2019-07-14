using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float yBorder;
    [SerializeField] float dampTime;

    float playerToCameraDistance;
    Vector2 playerToCameraOffset;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        playerToCameraDistance = Vector2.Distance(this.transform.position, player.transform.position);
        playerToCameraOffset = this.transform.position - player.transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        trackPlayer();
    }

    public void trackPlayer()
    {
        //float yOffset = ((player.transform.position.y - this.transform.position.y));

        /*if (yOffset >= yBorder)
        {
            this.transform.position = new Vector3(player.transform.position.x + offset, player.transform.position.y - yBorder, -10f);
        }else if(yOffset <= yBorder)
        {
            this.transform.position = new Vector3(player.transform.position.x + offset, player.transform.position.y - yBorder, -10f);
        }
        else
        {
            this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y , -10f);
        }*/

        float newAngle = player.transform.rotation.eulerAngles.z - 270f;
        float newXposition = Mathf.Cos((newAngle) * Mathf.Deg2Rad) * playerToCameraDistance;
        float newYposition = Mathf.Sin((newAngle) * Mathf.Deg2Rad) * playerToCameraDistance;

        if (player.transform.rotation.eulerAngles.z > 360f || player.transform.rotation.eulerAngles.z < 180f)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x - playerToCameraOffset.x, 0.0f, -10f);
            this.transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, dampTime);
        }
        else
        {
            //this.transform.position = new Vector3(player.transform.position.x + offset, this.transform.position.y, -10f);
            Vector3 newPosition = new Vector3(player.transform.position.x + playerToCameraOffset.x, 0.0f, -10f);
            this.transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, dampTime);
        }

        //Debug.Log(newXposition + "," + newYposition);
        //Debug.Log(player.transform.rotation.eulerAngles.z - 270f);
        //Debug.Log(Mathf.Cos(player.transform.rotation.eulerAngles.z - 270f) + "," + Mathf.Sin(player.transform.rotation.eulerAngles.z - 270f));
        
    }
}
