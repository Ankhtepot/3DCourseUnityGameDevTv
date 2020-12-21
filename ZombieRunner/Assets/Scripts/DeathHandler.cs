using ECM.Controllers;
using UnityEngine;
using MouseLook = ECM.Components.MouseLook;

//Fireball Games * * * PetrZavodny.com

public class DeathHandler : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private BaseCharacterController playerController;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    /// <summary>
    /// From Event // PlayerHealth
    /// </summary>
    public void HandleDeath()
    {
        GameOverCanvas.enabled = true;
        playerController.pause = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        var mouseLook = playerController.GetComponent<MouseLook>(); 
        mouseLook.SetCursorLock(false);
        mouseLook.verticalSensitivity = 0;
        mouseLook.lateralSensitivity = 0;
    }
    
    private void initialize()
    {
        GameOverCanvas.enabled = false;
    }
}
