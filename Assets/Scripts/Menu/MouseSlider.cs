using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSlider : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private float minSensitivity = 0.1f; // ����������� ����������������
    [SerializeField] private float maxSensitivity = 10f; // ������������ ����������������
    [SerializeField] private float defaultSensitivity = 1f; // �������� �� ���������

    public float Sensitivity { get; private set; } = 1f; // ������� ����������������

    private void Start()
    {
        // ������������� �������� �������� �� ���������
        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.value = defaultSensitivity;
        Sensitivity = defaultSensitivity;


        // ������������� �� ������� ��������� �������� ��������
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);

        //��������� ���������������� �� PlayerPrefs (�����������)
        LoadSensitivity();
    }

    private void UpdateSensitivity(float value)
    {
        Sensitivity = value;
        Debug.Log("Mouse Sensitivity: " + Sensitivity);
        // ����� �� ������ �������� ��� ��� ���������� Sensitivity � ����� ����
        // ��������, �������� �������� �������� ������.
    }


    private void LoadSensitivity()
    {
        // ��������� ����������� ���������������� �� PlayerPrefs
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
            UpdateSensitivity(PlayerPrefs.GetFloat("MouseSensitivity"));
        }
        else
        {
            // ���������� �������� �� ���������, ���� ���������� �����������.
            PlayerPrefs.SetFloat("MouseSensitivity", defaultSensitivity);
        }
    }


    private void OnApplicationQuit()
    {
        //��������� ���������������� � PlayerPrefs ��� ������ �� ���������� (�����������)
        PlayerPrefs.SetFloat("MouseSensitivity", Sensitivity);
    }
}
