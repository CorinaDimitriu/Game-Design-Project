using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor_enable : MonoBehaviour
{
   public AudioSource Music;
   void OnGUI()
   {
     Cursor.lockState = CursorLockMode.None;
     Cursor.visible = true;
   }
}
