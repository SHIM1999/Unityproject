using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60; // 프레임레이트를 60으로 설정
    }

    public void StartGame()
    {
        SceneManager.LoadScene("AtHome-level1"); // 게임 씬 이름에 맞게 수정
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료
    }
}
