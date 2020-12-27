using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameVisual : MonoBehaviour
{
    public int Score { get; set; }
    [SerializeField]
    public TextMeshProUGUI scoreText;
    [SerializeField]
    public TextMeshProUGUI timeText;
    [SerializeField]
    public Image wavesImage;
    [SerializeField]
    public Image healthBarImage;
    [SerializeField]
    public GameObject startPanel;
    [SerializeField]
    public GameObject endPanel;

    private void Awake()
    {
        Score = 0;
    }

    private void Start()
    {
        StartCoroutine(WaveMovement());
    }

    public void AddPoint()
    {
        Score += 1;
        scoreText.text = $"SCORE : {Score}";
    }

    public void ShowTime(float elapsedTime)
    {
        timeText.text = $"{Mathf.Floor(elapsedTime)}";
    }

    public void UpdateHealthBar()
    {
        Vector3 scale = healthBarImage.transform.localScale;
        int health = FindObjectOfType<AntiAircraft>().Health;
        healthBarImage.transform.localScale = new Vector3(health, scale.y, scale.x);
    }

    public void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    private IEnumerator WaveMovement()
    {
        bool forward = true;
        while (true)
        {
            var pos = wavesImage.rectTransform.position;
            if (forward)
            {
                wavesImage.rectTransform.position = new Vector3(pos.x + 0.01f, pos.y, pos.z);
                if (pos.x >= 1)
                {
                    forward = false;
                }
            }
            else
            {
                wavesImage.rectTransform.position = new Vector3(pos.x - 0.01f, pos.y, pos.z);
                if (pos.x <= -1)
                {
                    forward = true;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
