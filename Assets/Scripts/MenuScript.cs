using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace RightChoice
{
    public class MenuScript : MonoBehaviour
    {   
        [SerializeField] private Toggle _easy;
        [SerializeField] private Toggle _normal;
        [SerializeField] private Toggle _hard;
        public static float ScaleSpeed { get; set; }
        
        public void OnPlayClick()
        {
            if (_easy.isOn) ScaleSpeed = 0.5f;
            if (_normal.isOn) ScaleSpeed = 1f;
            if (_hard.isOn) ScaleSpeed = 1.5f;
            SceneManager.LoadScene("Game");
        }

        public void OnExitClick()
        {
            Application.Quit();
        }
    }
}
