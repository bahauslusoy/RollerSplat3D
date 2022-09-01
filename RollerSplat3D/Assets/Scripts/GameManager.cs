using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class GameManager : MonoBehaviour
{
    public GameObject[] grounds;
    public float groundNumbers;
    private int currentLevel;

    
    void Start()
    {
       grounds = GameObject.FindGameObjectsWithTag("Ground");
       currentLevel = SceneManager.GetActiveScene().buildIndex ;
       
    }

    // Update is called once per frame
    void Update()
    {
        groundNumbers = grounds.Length ;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevel + 1 );
    }
   
}
