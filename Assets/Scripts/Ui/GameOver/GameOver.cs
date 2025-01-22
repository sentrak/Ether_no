using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

/*
 * Clase: GameOver.
 * Descripción: Gestiona la pantalla de Game Over, incluyendo la lógica para reiniciar el juego, salir, 
 * y reproducir efectos de sonido al interactuar con los botones. Utiliza el sistema de UI Toolkit de Unity.
 */
public class GameOver : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip titleTheme; // Música de fondo para la pantalla de Game Over
    [SerializeField] private AudioClip hoverbottom; // Sonido al pasar el cursor sobre un botón
    [SerializeField] private AudioClip clickbottom; // Sonido al hacer clic en un botón

    private UIDocument menu; // Referencia al componente UIDocument para manejar la UI de la pantalla de Game Over
    private Button btnPlay; // Botón para reiniciar el juego
    private Button btnExit; // Botón para salir del juego

    /*
     * Método: Start.
     * Parámetros: Ninguno.
     * Descripción: Reproduce la música de fondo al iniciar la pantalla de Game Over.
     */
    void Start()
    {
        AudioManager.Instance.PlayMusic(titleTheme);
    }

    /*
     * Método: OnEnable.
     * Parámetros: Ninguno.
     * Descripción: Inicializa los elementos de la UI, como los botones, y registra los eventos para manejarlos.
     */
    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        // Asignar botones desde la UI
        btnPlay = root.Q<Button>("btnPlay");
        btnExit = root.Q<Button>("btnExit");

        // Registrar callbacks para los botones
        btnPlay.RegisterCallback<ClickEvent>(startGame);
        btnExit.RegisterCallback<ClickEvent>(exitGame);

        // Registrar el evento de hover para los botones
        btnPlay.RegisterCallback<MouseEnterEvent>(playHoverSound);
        btnExit.RegisterCallback<MouseEnterEvent>(playHoverSound);
    }

    /*
     * Método: startGame.
     * @param evt: Evento de clic en el botón de reinicio.
     * Descripción: Detiene la música, reproduce un sonido de clic y carga el nivel principal.
     */
    void startGame(ClickEvent evt)
    {
        AudioManager.Instance.PlaySound(clickbottom);
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*
     * Método: exitGame.
     * @param evt: Evento de clic en el botón de salir.
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
