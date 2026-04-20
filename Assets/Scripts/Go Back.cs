using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathSceneControl : MonoBehaviour
{
    void Update()
    {
        // Right controller trigger
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Ethan 2");
        }
    }
}