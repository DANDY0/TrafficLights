using App.Settings;

namespace App.Managers.Interfaces
{
    public interface IAppStateManager
    {
        Enumerators.AppState AppState { get; }
        void ChangeAppState(Enumerators.AppState stateTo);
    }
}