using UnityEngine;

public class worldMusic : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic; // Música de fondo 
    void Start()
    {
        AudioManager.Instance.PlayMusic(levelMusic);
        
    }
}
