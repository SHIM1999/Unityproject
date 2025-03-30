using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 필요 시 씬 전환용
using TMPro; // 꼭 필요함

public class AlarmTimer : MonoBehaviour
{
    public float timeRemaining = 15f; // 타이머 시작 시간
    public TMP_Text timerText; // <-- 이걸로 바꿔야 함!
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining > 0)
            {
                timerText.text = Mathf.Ceil(timeRemaining).ToString();
            }
            else
            {
                timerText.text = "0";
                isRunning = false;
                OnTimerEnd();
            }
        }
    }


void OnTimerEnd()
{
    Debug.Log("⛔ 실패! 타이머 종료됨");
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
}

}
