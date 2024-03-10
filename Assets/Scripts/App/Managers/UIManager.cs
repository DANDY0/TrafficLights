using System;
using System.Collections.Generic;
using App.Core;
using App.Managers.Interfaces;
using App.Screens.Pages;
using UnityEngine;
using UnityEngine.UI;

namespace App.Managers
{
    public class UIManager : IService, IUIManager
    {
        public GameObject Canvas { get; set; }
        public CanvasScaler CanvasScaler { get; set; }
        public IUIElement CurrentPage { get; set; }
        
        private List<IUIElement> _uiPages = new List<IUIElement>();

        public void Init()
        {
            Canvas = GameObject.Find("Canvas");
            
            _uiPages = new List<IUIElement>();
            _uiPages.Add(new MainPage());
            _uiPages.Add(new GamePage());

            foreach (var page in _uiPages)
                page.Init();
        }

        public void Update()
        {
            foreach (var page in _uiPages)
                page.Update();
        }

        public void HideAllPages()
        {
            foreach (var _page in _uiPages)
            {
                _page.Hide();
            }
        }

        public void SetPage<T>(bool hideAll = false) where T : IUIElement
        {
            IUIElement previousPage = null;

            if (hideAll)
            {
                HideAllPages();
            }
            else
            {
                if (CurrentPage != null)
                {
                    CurrentPage.Hide();
                    previousPage = CurrentPage;
                }
            }

            foreach (var _page in _uiPages)
            {
                if (_page is T)
                {
                    CurrentPage = _page;
                    break;
                }
            }
            CurrentPage.Show();
        }

        public T GetPage<T>() where T : IUIElement
        {
            IUIElement page = null;
            foreach (var _page in _uiPages)
            {
                if (_page is T)
                {
                    page = _page;
                    break;
                }
            }

            return (T)page;
        }

        public void Dispose()
        {
            foreach (var page in _uiPages)
                page.Dispose();
        }
    }

 
}