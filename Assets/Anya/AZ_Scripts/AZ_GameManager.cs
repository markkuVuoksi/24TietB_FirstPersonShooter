using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using Unity.Multiplayer.Center.Common;

public class AZ_GameManager : MonoBehaviour
{

    public GameObject winScreen;
    public GameObject loseScreen;

    void Start()
    {
   
    }

    void Update()
    {
    }

    public void Win()
    {
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
    }

    public void RecieveTimerEnded()
    {
        //recieve info from timer
        if(HasEnemiesOnScene() == true)
        {
            Lose();
        }
        else
        {
            Win();
        }
    }

    public bool HasEnemiesOnScene()
    {
        //Asks are there enemies on scene (tag), return bool
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length>0)
        {
            return true;
        }
        return false;
    }
}
