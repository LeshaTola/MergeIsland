using System;
using App.Scripts.Modules.Commands.General;

namespace App.Scripts.Modules.PopupAndViews.Popups.InputField
{
    public class InputFieldPopupData
    {
        public string Header;
        public string Mesage;
        public string Placeholder;
        public string StartValue;
        public Action<string> OnValueChanged;
        public Action<string> OnEndEdit;
        public ILabeledCommand Command;
    }
}