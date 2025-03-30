using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlarmGame : MonoBehaviour
{
    public RectTransform movingBar; // 움직이는 바
    public RectTransform targetZone; // 정중앙 성공 구역
    public Button stopAlarmButton; // 알람 끄기 버튼
    public Button nextButton; // "다음" 버튼
    public RectTransform gaugePanel; // 🚀 GaugePanel의 크기 참조

    private float speed = 100f; // 🚀 초기 속도를 40으로 설정
    private bool movingRight = true;
    private int successCount = 0; // 타이밍 성공 횟수
    private Vector2 originalTargetSize; // 타겟 존 원래 크기 저장

    void Start()
    {
        nextButton.gameObject.SetActive(false); // 🚀 "다음" 버튼 처음엔 숨김
        originalTargetSize = targetZone.sizeDelta; // 타겟 존 크기 저장
    }

    void Update()
    {
        MoveBar(); // 바 움직이기
    }

    void MoveBar()
{
    float moveAmount = speed * Time.deltaTime * 9f;

    float gaugeHalfWidth = gaugePanel.rect.width / 2f;
    float barHalfWidth = movingBar.rect.width / 2f;
    float maxX = gaugeHalfWidth - barHalfWidth;

    Vector3 pos = movingBar.localPosition;

    if (movingRight)
    {
        pos.x += moveAmount;
        if (pos.x >= maxX)
        {
            pos.x = maxX;
            movingRight = false;
        }
    }
    else
    {
        pos.x -= moveAmount;
        if (pos.x <= -maxX)
        {
            pos.x = -maxX;
            movingRight = true;
        }
    }

    movingBar.localPosition = pos;
}


    public void CheckTiming()
{
    // 바의 좌우 위치 계산
    float barLeft = movingBar.localPosition.x - movingBar.rect.width / 2f;
    float barRight = movingBar.localPosition.x + movingBar.rect.width / 2f;

    // 타겟 존의 좌우 위치 계산
    float targetLeft = targetZone.localPosition.x - targetZone.rect.width / 2f;
    float targetRight = targetZone.localPosition.x + targetZone.rect.width / 2f;

    // 바가 타겟 존 범위 안에 겹치면 성공
    if (barRight >= targetLeft && barLeft <= targetRight)
    {
        successCount++;
        Debug.Log("성공 횟수: " + successCount);

        if (successCount < 3)
        {
            ShrinkOrMoveTargetZone(); // 크기 줄이거나 랜덤 이동
        }

        if (successCount >= 3)
        {
            nextButton.gameObject.SetActive(true); // ✅ 3번 성공 시 "다음" 버튼 보이기
            Debug.Log("다음 버튼 활성화됨!");
        }
    }
    else
    {
        successCount = 0;
        ResetTargetZone(); // 실패 시 리셋
    }
}


    private void ShrinkOrMoveTargetZone()
{
    float rand = Random.value;

    if (rand < 0.5f)
    {
        // 크기 줄이기
        float newWidth = Mathf.Max(targetZone.sizeDelta.x * 0.8f, 40f);
        targetZone.sizeDelta = new Vector2(newWidth, targetZone.sizeDelta.y);
        Debug.Log("🔧 타겟 존 크기 축소됨! → " + newWidth);
    }
    else
    {
        // 위치 랜덤 이동
        float maxX = gaugePanel.rect.width / 2f - targetZone.rect.width / 2f;
        float newX = Random.Range(-maxX, maxX);
        targetZone.localPosition = new Vector3(newX, targetZone.localPosition.y, 0);
        Debug.Log("🧭 타겟 존 위치 이동됨! → " + newX);
    }
}


    private void ResetTargetZone()
    {
        targetZone.sizeDelta = originalTargetSize; // 🚀 원래 크기로 복구
        targetZone.localPosition = Vector3.zero; // 🚀 원래 위치로 복구
    }

    public void LoadNextScene() // 🚀 "다음" 버튼을 누르면 씬 이동
    {
        Debug.Log("씬 이동: AtHome-level1_2");
        SceneManager.LoadScene("AtHome-level1_2");
    }
}

