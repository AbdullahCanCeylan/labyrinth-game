using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallControl : MonoBehaviour
{
    public Button btn;
    private Rigidbody rb;
    public float speed = 30;
    public Text time, health, status;
    float timeCounter = 500;
    float healthCounter = 25;
    bool gameContinues = true;
    bool gameFinished = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gameContinues && !gameFinished) 
        {
            timeCounter -= Time.deltaTime;
            time.text = (int)timeCounter + "";
        }
        else if (!gameFinished)
        {
            status.text = "Game Over You lose";
            btn.gameObject.SetActive(true);
        }
        if(timeCounter < 0)
        {
            gameContinues = false;
        }
    }
    void FixedUpdate()
    {
        if(gameContinues && !gameFinished)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 force = new Vector3(-horizontal, 0, -vertical);
            rb.AddForce(force * (speed * Time.fixedDeltaTime));
        }
        else 
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        string objectName = other.gameObject.name;
        if(objectName.Equals("Finish"))
        {
            gameFinished = true;
            status.text = "Game Over. You Win";
            btn.gameObject.SetActive(true);
        }
        else if(!objectName.Equals("MazeGround") && !objectName.Equals("Finish") && !objectName.Equals("Start") && !objectName.Equals("Ground"))
        {
            healthCounter -= 1;
            health.text = healthCounter + "";
            if(healthCounter == 0)
            {
                gameContinues = false;
            }
        }
    }
}
