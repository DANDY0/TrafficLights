using App.Core;
using App.Managers;
using App.Managers.Interfaces;

namespace App
{
    public class GameClient : ServiceLocatorBase
    {
        private static object _sync = new object();

        private static GameClient _Instance;
        public static GameClient Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_sync)
                    {
                        _Instance = new GameClient();
                    }
                }
                return _Instance;
            }
        }

        private GameClient() : base()
        {
            AddService<ILoadDataManager>(new LoadDataManager());
            AddService<IGameFactoryManager>(new GameFactoryManager());
            AddService<IUIManager>(new UIManager());
            AddService<IAppStateManager>(new AppStateManager());
            AddService<IGameplayManager>(new GameplayManager());
        }

        public static T Get<T>()
        {
            return Instance.GetService<T>();
        }
    }
}