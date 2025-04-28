using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    public TextMeshProUGUI killText;
    private int totalKillCount = 0; // Biến này lưu tổng số kill

    // Start is called before the first frame update
    void Start()
    {
        // Cập nhật killText ban đầu
        UpdateKillText();
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra và cập nhật tổng số kill mỗi lần
        UpdateKillText();
    }

    // Hàm để cập nhật UI với số kill hiện tại
    public void IncrementKillCount()
    {
        totalKillCount++;
        UpdateKillText();
    }

    // Cập nhật UI text
    private void UpdateKillText()
    {
        killText.text = "Kills: " + totalKillCount;
    }
}
