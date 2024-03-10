using System;
using App.Core;
using UnityEngine;
using UnityEngine.UI;

namespace App.Managers.Interfaces
{
    public interface IUIManager
    {
        GameObject Canvas { get; set; }
        CanvasScaler CanvasScaler { get; set; }
        IUIElement CurrentPage { get; set; }
        void SetPage<T>(bool hideAll = false) where T : IUIElement;
        T GetPage<T>() where T : IUIElement;
        void HideAllPages();
    }
}