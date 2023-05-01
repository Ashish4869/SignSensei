using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject _tutorialOverlay;
    [SerializeField] GameObject _slides;

    int _currentSlide = 1;
    int _slideCount = 4;
    string folderName = "";

    Image _image;
   
    void SetupTutorial()
    {
        _tutorialOverlay.SetActive(true);
        _image = _slides.GetComponentInChildren<Image>();
    }
    public void ShowCameraSetUp()
    {
        _currentSlide = 1;
        _slideCount = 4;
        folderName = "CameraSetup/";
        SetupTutorial();
        _image.sprite = Resources.Load<Sprite>("ImageData/TutorialSlides/" + folderName + _currentSlide.ToString());
    }

    public void CloseTutorial()
    {
        _tutorialOverlay.SetActive(false);
    }

    public void ShowBasicRules()
    {
        _currentSlide = 1;
        _slideCount = 5;
        folderName = "BasicRules/";
        SetupTutorial();
        _image.sprite = Resources.Load<Sprite>("ImageData/TutorialSlides/" + folderName + _currentSlide.ToString());
    }

    public void ShowSignLanguageInstructions()
    {
        _currentSlide = 1;
        _slideCount = 4;
        folderName = "SignLanguageInstructions/";
        SetupTutorial();
        _image.sprite = Resources.Load<Sprite>("ImageData/TutorialSlides/" + folderName + _currentSlide.ToString());
    }

    public void Left()
    {
        _currentSlide = ((_currentSlide-1)) % _slideCount;
        _image.sprite = Resources.Load<Sprite>("ImageData/TutorialSlides/" + folderName  + Math.Abs(_currentSlide).ToString());
    }

    public void Right()
    {
        _currentSlide = (_currentSlide+1) % _slideCount;
        _image.sprite = Resources.Load<Sprite>("ImageData/TutorialSlides/" + folderName + Math.Abs(_currentSlide).ToString());
    }
}
