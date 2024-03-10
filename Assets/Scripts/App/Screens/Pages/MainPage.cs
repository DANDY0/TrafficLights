using App.Core;
using App.Managers.Interfaces;
using App.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Screens.Pages
{
    public class MainPage : IUIElement
    {
        private GameObject _selfPage;

        private IUIManager _uiManager;
        private ILoadDataManager _loadDataManager;
        private IAppStateManager _appStateManager;

        private Button _playGameButton;

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
            _loadDataManager = GameClient.Get<ILoadDataManager>();
            _appStateManager = GameClient.Get<IAppStateManager>();

            _selfPage = MonoBehaviour.Instantiate(_loadDataManager.GetObjectByPath<GameObject>(AssetPath.MainPage));
            _selfPage.transform.SetParent(_uiManager.Canvas.transform, false);
            _selfPage.name = GetType().Name;

            _playGameButton = _selfPage.transform.Find("Button_Start").GetComponent<Button>();
            _playGameButton.onClick.AddListener(PlayGameButtonOnClickHandler);

            _selfPage.SetActive(false);
        }

        public void Hide()
        {
            _selfPage.SetActive(false);
        }

        public void Show()
        {
            _selfPage.SetActive(true);
        }

        public void Update()
        {
        }

        public void Dispose()
        {
        }

        private void PlayGameButtonOnClickHandler()
        {
            _appStateManager.ChangeAppState(Enumerators.AppState.Game);
        }
        
    }
}