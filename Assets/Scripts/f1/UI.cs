using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public SimController simController;
    public TMP_Text timeText;
    public TMP_Text speedText;
    public Dropdown driverDropdown;
    public GameObject menuPanel;
    [SerializeField] private OVRInput.Controller controller;
    private bool readyToStart = false;

    private void Start()
    {
        simController.F1Data.OnDataFetched.AddListener(() =>
        {
            readyToStart = true;
            populateDriverDropdown();

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

        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            Debug.Log("Button " + OVRInput.Button.One + " pressed");
            ToggleMenu();
        }
    }

    private void populateDriverDropdown()
    {
        driverDropdown.ClearOptions();
        var driverOptions = simController.F1Data.AllDrivers().ConvertAll(d => new Dropdown.OptionData(d.full_name));
        driverDropdown.AddOptions(driverOptions);
        driverDropdown.onValueChanged.AddListener(delegate
        {
            var selectedDriver = simController.F1Data.AllDrivers()[driverDropdown.value];
            simController.selectDriver(selectedDriver.driver_number);
        });
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
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