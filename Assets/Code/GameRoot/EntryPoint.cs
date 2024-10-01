using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using RadioSilence.InventorySystem.GameplayComponents;
using RadioSilence.Services.InputServices;
using RadioSilence.UI;

namespace RadioSilence.GameRoot
{
    public class EntryPoint
    {
        private const string COROUTINES_OBJECT_NAME = "COROUTINES";
        private const string UIROOT_PREFAB_PATH = "Root/UIRoot";

        private static EntryPoint _instance;

        private Coroutines _coroutines;
        private UIRootView _uIRoot;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Start()
        {
            _instance = new EntryPoint();
            _instance.Run();
        }

        private EntryPoint()
        {
            InitializeCoroutinesObject();
            InitializeUIRoot();
        }

        private void InitializeUIRoot()
        {
            UIRootView uiRootPrefab = Resources.Load<UIRootView>(UIROOT_PREFAB_PATH);
            _uIRoot = Object.Instantiate(uiRootPrefab);
            Object.DontDestroyOnLoad(_uIRoot.gameObject);
        }

        private void InitializeCoroutinesObject()
        {
            _coroutines = new GameObject(COROUTINES_OBJECT_NAME).AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
        }

        private void Run()
        {
#if UNITY_EDITOR
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAME)
            {
                _coroutines.StartCoroutine(LoadAndStartGame());
                return;
            }

            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif
            _coroutines.StartCoroutine(LoadAndStartGame());
        }

        private IEnumerator LoadAndStartGame()
        {
            _uIRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAME);
            yield return null;

            InitializeGameplayEntryPoint();

            _uIRoot.HideLoadingScreen();
        }

        private void InitializeGameplayEntryPoint()
        {
            GameplayEntryPoint gameplayEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            
            gameplayEntryPoint.Run(_uIRoot.UICanvasTransform);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}