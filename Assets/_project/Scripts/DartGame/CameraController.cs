using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain m_cinemachineBrain;

    [SerializeField] private CinemachineCamera m_mainMenu;
    [SerializeField] private CinemachineCamera m_dartGameplay;
    [SerializeField] private CinemachineCamera m_dartPlushies;

    void Start()
    {

    }
    void Update()
    {

    }

    public void SwitchToMainMenu()
    {
        m_mainMenu.gameObject.SetActive ( true);
        m_dartGameplay.gameObject.SetActive ( false);
        m_dartPlushies.gameObject.SetActive( false);
    }
    public void SwitchToDartGameplay()
    {
        Debug.Log ("switch gameplay");
        
        m_mainMenu.gameObject.SetActive(false);
        m_dartGameplay.gameObject.SetActive(true);
        m_dartPlushies.gameObject.SetActive(false);
    }
    public void SwitchToDartPlushies()
    {
        Debug.Log("hey");

        m_mainMenu.gameObject.SetActive ( false);
        m_dartGameplay.gameObject.SetActive( false);
        m_dartPlushies.gameObject.SetActive( true);
    }

    public void SwitchCamera(CinemachineVirtualCameraBase cam)
    {
        cam.Priority = 10;
    }
}
