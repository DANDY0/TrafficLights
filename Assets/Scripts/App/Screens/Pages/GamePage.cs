using App.Core;
using App.Managers.Interfaces;
using App.Screens.Panels.CanDrivePanel;
using App.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace App.Screens.Pages
{
    public class GamePage: IUIElement
    {
        private GameObject _selfPage;

        private IUIManager _uiManager;
        private ILoadDataManager _loadDataManager;

        private CanDriveView _canDriveView;
        private CanDrivePanel _canDrivePanel;
        
        private Button _playGameButton;

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
            _loadDataManager = GameClient.Get<ILoadDataManager>();

            _selfPage = MonoBehaviour.Instantiate(_loadDataManager.GetObjectByPath<GameObject>(AssetPath.GamePage));
            _selfPage.transform.SetParent(_uiManager.Canvas.transform, false);
            _selfPage.name = GetType().Name;
            
            _selfPage.SetActive(false);
        }

        public void Hide()
        {
            _selfPage.SetActive(false);
        }

        public void Show()
        {
            InitializeCanDrivePanel();
            _selfPage.SetActive(true);
        }

        public void Update()
        {
            if (_canDrivePanel != null) 
                _canDrivePanel.Update();
        }

        public void Dispose()
        {
            if (_canDrivePanel != null) 
                _canDrivePanel.Dispose();
        }

        private void InitializeCanDrivePanel()
        {
            GameObject canDrivePanelObject = _selfPage.transform.Find("CanDrivePanel").gameObject;

            _canDriveView = new CanDriveView(canDrivePanelObject);
            _canDrivePanel = new CanDrivePanel(_canDriveView);
        }
        
    }
}