using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("SinglePlayerLobby");
    }
}
