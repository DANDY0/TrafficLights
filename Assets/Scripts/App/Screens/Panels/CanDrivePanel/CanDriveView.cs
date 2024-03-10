using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Screens.Pages
{
    public class CanDriveView
    {
        public Button CanDriveButton { get; private set; }
        private TextMeshProUGUI _textDescription;
        private TextMeshProUGUI _canDriveText;

        public CanDriveView(GameObject selfObject)
        {
            CanDriveButton = selfObject.transform.Find("Button_Drive").GetComponent<Button>();
            _canDriveText = selfObject.transform.Find("Text_CanDrive").GetComponent<TextMeshProUGUI>();
            _textDescription = selfObject.transform.Find("Text_SignalDescription").GetComponent<TextMeshProUGUI>();
        }

        public void SetDescription(string text)
        {
            _textDescription.text = text;
        }

        public void SetCanDriveText(string text)
        {
            _canDriveText.text = text;
        }
    }
}