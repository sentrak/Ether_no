using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

/*
 * Clase: MainTittle.
 * Descripción: Gestiona la pantalla del menú principal, incluyendo la música, navegación
 * entre escenas, y efectos sonoros al interactuar con los botones mediante el sistema UI Toolkit.
 */
public class MainTittle : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip titleTheme; // Música de fondo para el menú principal
    [SerializeField] private AudioClip hoverbottom; // Sonido al pasar el cursor sobre un botón
    [SerializeField] private AudioClip clickbottom; // Sonido al hacer clic en un botón

    private UIDocument menu; // Referencia al UIDocument para manejar la interfaz del menú
    private Button btnPlay; // Botón para iniciar el juego
    private Button btnExit; // Botón para salir del juego

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Reproduce la música de fondo al iniciar el menú principal.
     */
    void Start()
    {
        AudioManager.Instance.PlayMusic(titleTheme);
    }

    /*
     * Método: OnEnable.
     * Parámetros: Ninguno.
     * Descripción: Inicializa los elementos de la interfaz de usuario y registra los eventos de los botones.
     */
    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;
        btnPlay = root.Q<Button>("btnPlay");
        btnExit = root.Q<Button>("btnExit");

        btnPlay.RegisterCallback<ClickEvent>(startGame);
        btnExit.RegisterCallback<ClickEvent>(exitGame);

        btnPlay.RegisterCallback<MouseEnterEvent>(playHoverSound);
        btnExit.RegisterCallback<MouseEnterEvent>(playHoverSound);
    }

    /*
     * Método: startGame.
     * @param evt: Evento de clic en el botón de iniciar el juego.
     * Descripción: Reproduce un sonido de clic, detiene la música de fondo y carga la escena del nivel principal.
     */
    void startGame(ClickEvent evt)
    {
        AudioManager.Instance.PlaySound(clickbottom);
        AudioManager.Instance.StopMusic();
        //SceneManager.LoadScene("02 Level01");
        SceneManager.LoadScene("02 Level01");
    }

    /*
     * Método: exitGame.
     * @param evt: Evento de clic en el botón de salir del juego.
     * Descripción: Cierra la aplicación.
     */
    void exitGame(ClickEvent evt)
    {
        Application.Quit();
    }

    /*
     * Método: playHoverSound.
     * @param evt: Evento de pasar el cursor sobre un botón.
     * Descripción: Reproduce un efecto de sonido al pasar el cursor sobre un botón.
     */
    void playHoverSound(MouseEnterEvent evt)
    {
        AudioManager.Instance.PlaySound(hoverbottom);
    }
}
