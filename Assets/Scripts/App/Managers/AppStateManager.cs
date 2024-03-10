using App.Core;
using App.Managers.Interfaces;
using App.Screens.Pages;
using App.Settings;

namespace App.Managers
{
    public class AppStateManager : IService, IAppStateManager
    {
        private IUIManager _uiManager;
        public Enumerators.AppState AppState { get; private set; } = Enumerators.AppState.Unknown;

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
            
            ChangeAppState(Enumerators.AppState.Main);
        }

        public void Update()
        {

        }

        public void ChangeAppState(Enumerators.AppState stateTo)
        {
            if (AppState == stateTo)
                return;

            AppState = stateTo;

            switch (stateTo)
            {
                case Enumerators.AppState.Main:
                    _uiManager.SetPage<MainPage>();
                    GameClient.Get<IGameplayManager>().StopGameplay();
                    break;
                case Enumerators.AppState.Game:
                    _uiManager.SetPage<GamePage>();
                    GameClient.Get<IGameplayManager>().StartGameplay();
                    break;
            }
        }

        public void Dispose()
        {

        }
    }
}