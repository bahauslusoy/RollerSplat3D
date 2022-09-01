using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallController : MonoBehaviour
{

    enum State
    {
        preGame,
        inGame,


        succesGame,
    }
    public Rigidbody rb;
    private Vector2 firstPos, secondPos, currentPos;
    private float moveSpeed;
    public float currentGroundNumber;
    public Image levelBar;
    private GameManager gameManager;
    private State _currentState = State.preGame;

    public GameObject startPanel, failPanel, successPanel;
    void Start()
    {

        startPanel.SetActive(true);
        moveSpeed = 0;

        gameManager = GameObject.FindObjectOfType<GameManager>();

    }


    void Update()
    {
        levelBar.fillAmount = currentGroundNumber / gameManager.groundNumbers;
        switch (_currentState)
        {
            case State.preGame:
        
                if (Input.GetMouseButtonDown(0))
                {
                    startPanel.SetActive(false);

                    _currentState = State.inGame;
                }

                break;
            case State.inGame:
                moveSpeed = 20;
                Swipe();
                if (levelBar.fillAmount == 1)
                {
                    successPanel.SetActive(true);
                }

                break;
        }



    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentPos = new Vector2(
                secondPos.x - firstPos.x,
                secondPos.y - firstPos.y);

        }

        currentPos.Normalize();
        if (currentPos.y < 0 && currentPos.x < 0.5f && currentPos.x > -0.5f)
        {
            // aşşağı

            rb.velocity = Vector3.back * moveSpeed;
        }
        else if (currentPos.y > 0 && currentPos.x < 0.5f && currentPos.x > -0.5f)
        {
            //yukarı
            rb.velocity = Vector3.forward * moveSpeed;
        }
        else if (currentPos.x > 0 && currentPos.y < 0.5f && currentPos.y > -0.5f)
        {
            //sağ
            rb.velocity = Vector3.right * moveSpeed;
        }
        else if (currentPos.x < 0 && currentPos.y < 0.5f && currentPos.y > -0.5f)
        {
            //sol
            rb.velocity = Vector3.left * moveSpeed;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
        {
            if (other.gameObject.tag == "Ground")
            {
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                currentGroundNumber++;
            }
        }
    }

}
