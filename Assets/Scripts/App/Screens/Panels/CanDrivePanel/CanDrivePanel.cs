using System;
using App.Gameplay.Controllers;
using App.Gameplay.Model;
using App.Helpers;
using App.Managers.Interfaces;
using App.Screens.Pages;
using App.Settings;

namespace App.Screens.Panels.CanDrivePanel
{
    public class CanDrivePanel
    {
        private ILoadDataManager _loadDataManager;
        private CrossRoadEntitiesController _crossRoadEntitiesController;
        private TrafficLightsController _trafficLightsController;
        
        private CanDriveView _view;
        private LightDescriptionTextsData _lightDescriptionTextsData;
        
        private Timer _timer;
        
        private readonly string _canDriveText = "Can drive";
        private readonly string _cantDriveText = "Can't drive";
        private int _textShowDuration = 2;

        public CanDrivePanel(CanDriveView view)
        {
            _view = view;
            _loadDataManager = GameClient.Get<ILoadDataManager>();
            _trafficLightsController = GameClient.Get<IGameplayManager>().GetController<TrafficLightsController>();
            _crossRoadEntitiesController = GameClient.Get<IGameplayManager>().GetController<CrossRoadEntitiesController>();
            _lightDescriptionTextsData = _loadDataManager.GetObjectByPath<LightDescriptionTextsData>(AssetPath.LightDescriptionTexts);
            _timer = new Timer();
            
            _trafficLightsController.SubscribeOnCharacterState(_crossRoadEntitiesController.Character.TrafficLightsControlPoint.RoadDirection, StateChangedHandler);
            _timer.TimerEndedEvent += TimerEndedEvent;
            
            _view.CanDriveButton.onClick.AddListener(CanDriveButtonClickHandler);
        }

        public void Update()
        {
            _timer.Update();
        }

        public void Dispose()
        {
            _trafficLightsController.UnSubscribeOnCharacterState(_crossRoadEntitiesController.Character.TrafficLightsControlPoint.RoadDirection, StateChangedHandler);
            _timer.TimerEndedEvent -= TimerEndedEvent;
            _view.CanDriveButton.onClick.RemoveListener(CanDriveButtonClickHandler);
        }
        
        private void StateChangedHandler(Enumerators.TrafficLightState state)
        {
            _view.SetDescription(_lightDescriptionTextsData.lightDescriptionTexts.Find(t => t.TrafficLightState == state).Description);
        }

        private void CanDriveButtonClickHandler()
        {
            Enumerators.TrafficLightState characterLightState = _crossRoadEntitiesController.Character.TrafficLightsControlPoint.TrafficLight.State;
            
            _view.SetCanDriveText(DefineCanDriveText(characterLightState));
            _timer.StartTimer(_textShowDuration);
        }

        private void TimerEndedEvent()
        {
            _view.SetCanDriveText(String.Empty);
        }

        private string DefineCanDriveText(Enumerators.TrafficLightState characterLightState)
        {
            switch (characterLightState)
            {
                case Enumerators.TrafficLightState.Red:
                case Enumerators.TrafficLightState.RedAmber:
                case Enumerators.TrafficLightState.Amber:
                    return _cantDriveText;
                case Enumerators.TrafficLightState.Green:
                    return _canDriveText;
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterLightState), characterLightState, null);
            }
        }
    }
}