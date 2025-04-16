using UnityEngine;
using TMPro; // TextMeshPro ���g�����߂ɒǉ�

public class BallSelectorUI: MonoBehaviour
{
    [Header("���̓��{�ꖼ")]
    public string[] ballNamesInJapanese;

    [Header("UI (TextMeshPro)")]
    public TextMeshProUGUI ballNameTMPText;

    private int selectedBallIndex = 0;

    void Start()
    {
        UpdateBallNameUI();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            selectedBallIndex = (selectedBallIndex + 1) % ballNamesInJapanese.Length;
            UpdateBallNameUI();
        }
        else if (scroll < 0f)
        {
            selectedBallIndex = (selectedBallIndex - 1 + ballNamesInJapanese.Length) % ballNamesInJapanese.Length;
            UpdateBallNameUI();
        }
    }

    void UpdateBallNameUI()
    {
        if (ballNameTMPText != null && selectedBallIndex < ballNamesInJapanese.Length)
        {
            ballNameTMPText.text = $"select�F{ballNamesInJapanese[selectedBallIndex]}";
        }
    }

    public int GetSelectedBallIndex()
    {
        return selectedBallIndex;
    }
}
