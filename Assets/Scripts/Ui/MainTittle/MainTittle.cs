using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainTittle : MonoBehaviour
{
        [Header("Audio Sources")]
    [SerializeField] private AudioClip titleTheme, hoverbottom, clickbottom;
 UIDocument menu;
    Button btnPlay;
    Button btnExit;
     void Start()
    {
        AudioManager.Instance.PlayMusic(titleTheme);

    } 
    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;
        btnPlay = root.Q<Button>("btnPlay");
        btnExit = root.Q<Button>("btnExit");

        btnPlay.RegisterCallback<ClickEvent>(startGame);
        btnExit.RegisterCallback<ClickEvent>(exitGame);

        // Registrar el evento de hover para los botones
        btnPlay.RegisterCallback<MouseEnterEvent>(playHoverSound);
        btnExit.RegisterCallback<MouseEnterEvent>(playHoverSound);
    }
void startGame(ClickEvent evt)
    {
        AudioManager.Instance.PlaySound(clickbottom);
        AudioManager.Instance.StopMusic();
   SceneManager.LoadScene("03 level");
    }
    void exitGame(ClickEvent evt)
    {
        Application.Quit();
    }
       void playHoverSound(MouseEnterEvent evt)
    {
        AudioManager.Instance.PlaySound(hoverbottom);
    }
}