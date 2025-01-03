using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainTittle : MonoBehaviour
{
    [SerializeField] private AudioClip titleTheme;

     void Awake()
    {
        AudioManager.Instance.PlayMusic(titleTheme);
    }

}