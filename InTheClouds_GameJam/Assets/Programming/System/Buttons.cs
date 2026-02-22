using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject RoundManager;
    
    public void beginGame()
    {
        startPanel.SetActive(false);
        RoundManager.GetComponent<RoundManager>().RoundEnd();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
