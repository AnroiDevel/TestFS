using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Test
{
    public class SecondSceneController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _privacePanel;
        [SerializeField] private Toggle _aceptPolice;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                _mainPanel?.SetActive(true);
                _privacePanel?.SetActive(false);
            }
        }

        public void StartNext()
        {
            if (_aceptPolice.isOn)
                SceneManager.LoadScene("Scena3");
        }
    }

}