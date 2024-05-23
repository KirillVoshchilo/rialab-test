using App.Runtime.Architecture;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
namespace App.Runtime.Content.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _startGameButton;

        private SceneLoader _loadSceneController;

        private void OnEnable()
        {
            _exitGameButton.onClick.AddListener(ExitGame);
            _startGameButton.onClick.AddListener(StartGame);
        }
        private void OnDisable()
        {
            _exitGameButton.onClick.RemoveListener(ExitGame);
            _startGameButton.onClick.RemoveListener(StartGame);
        }

        [Inject]
        public void Construct(SceneLoader loadSceneController)
        {
            _loadSceneController = loadSceneController; 
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        private void StartGame()
        {
            _loadSceneController.Load(SceneIndexes.LEVEL_1)
                .Forget();
        }
    }
}