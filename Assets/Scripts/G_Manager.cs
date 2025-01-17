using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class G_Manager : MonoBehaviour
{
    private Save_Load_Manager saveLoadManager;
    private Player_Control PlCon;
    public int shouldBeKiled;


    public void Restart_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Load_Game();
    }
    public void Go_To_Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) Restart_Game();
    }
    public void Start()
    {
        saveLoadManager = GetComponent<Save_Load_Manager>();
        PlCon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        Load_Game();
    }

    public void Save_Game()
    {
        saveLoadManager.Save(PlCon.kiledEnemie); 
    }

    public void Load_Game()
    {
 
        shouldBeKiled = saveLoadManager.Load();
        PlCon.kiledEnemie = shouldBeKiled;
    }

}
