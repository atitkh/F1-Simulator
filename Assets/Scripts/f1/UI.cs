using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public SimController simController;
    public TMP_Text timeText;

    public TMP_Text speedText;

    private bool readyToStart = false;

    private void Start()
    {
        simController.F1Data.OnDataFetched.AddListener(() =>
        {
            readyToStart = true;
        });
    }

    private void Update()
    {
        if (!readyToStart)
        {
            timeText.text = "Loading data...";
            timeText.enabled = true;
        }
        else
        {
            timeText.enabled = true;
            timeText.text = simController.simulatedTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        if (simController.timeMultiplier != 1)
        {
            speedText.text = "Speed: " + simController.timeMultiplier + "x";
        }
        else
        {
            speedText.text = "Speed: Normal";
        }
    }

    public void StartSimulation()
    {
        if (readyToStart)
        {
            simController.timeMultiplier = 1;
            simController.StartSimulation();
        }
    }

    public void StopSimulation()
    {
        simController.StopSimulation();
    }

    public void ResetSimulation()
    {
        simController.ResetSimulation();
    }

    public void PausePlaySimulation()
    {
        simController.PausePlaySimulation();
    }

    public void IncreaseSpeed()
    {
        simController.timeMultiplier++;
    }

    public void DecreaseSpeed()
    {
        simController.timeMultiplier--;
    }
}