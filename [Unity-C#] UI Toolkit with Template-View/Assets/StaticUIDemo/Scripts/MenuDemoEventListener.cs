using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuDemoEventListener : MonoBehaviour
{
    UIDocument Document;
    VisualElement optionContainer;
    VisualElement root;
    Button button;

    private void Awake()
    {
        Document = GetComponent<UIDocument>();
        root = Document.rootVisualElement;
        root.Q<Button>("start").clicked += StartButtonClicked;
        root.Q<Button>("settings").clicked += SettingsButtonClicked;
        root.Q<Button>("exit").clicked += ExitButtonClicked;
        root.Q<Slider>("volumeSlider").RegisterValueChangedCallback(VolumeSliderChanged);

        optionContainer = root.Q<VisualElement>("optionsContainer");
        optionContainer.AddToClassList("hide");

        button = new Button();
        button.text = "Back";
        button.clicked += BackButtonClicked;
    }

    void OnDestroy()
    {
        root.Q<Button>("start").clicked -= StartButtonClicked;
        root.Q<Button>("settings").clicked -= SettingsButtonClicked;
        root.Q<Button>("exit").clicked -= ExitButtonClicked;
        root.Q<Slider>("slider").UnregisterValueChangedCallback(VolumeSliderChanged);
    }

    void StartButtonClicked()
    {
        Debug.Log("Start button clicked");
    }

    void SettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
        //optionContainer.ToggleInClassList("hide");
        optionContainer.RemoveFromClassList("hide");
        optionContainer.Add(button);
    }

    private void BackButtonClicked()
    {
        Debug.Log("Back button clicked");
        //optionContainer.ToggleInClassList("hide");
        optionContainer.AddToClassList("hide");
    }

    void ExitButtonClicked()
    {
        Debug.Log("Exit button clicked");
    }

    void VolumeSliderChanged(ChangeEvent<float> evt)
    {
        Debug.Log("Volume slider changed: " + evt.newValue);
    }



}
