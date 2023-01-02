using System;
using UnityEngine;
using Platformer;
using Platformer.UserInterface;

namespace Platformer.UserInterface.PopUp
{

    public struct PopUpConfig
    {
        public string PopUpName;
        public string IconWay;
        public string Title;
        public bool IsActiveCloseButton;
        public bool IsActiveInputField;
        public bool IsActiveCancelButton;
        public bool CloseAnywareClick;
}


    public class PopUpConfigLibrary
    {
        public static PopUpConfig EnterSeedPopUpConfig = new PopUpConfig() {
            PopUpName = "EnterSeedPopUp",
            IconWay = PopUpsLibrary.EnterSeedIconResource,
            Title = PopUpsLibrary.EnterSeedTitle,
            IsActiveCloseButton = true,
            IsActiveInputField = true,
            IsActiveCancelButton = true,
            CloseAnywareClick = false
        };

        public static PopUpConfig SeedCopiedPopUpConfig = new PopUpConfig()
        {
            PopUpName = "SeedCopiedPopUp",
            IconWay = PopUpsLibrary.SeedCopiedIconResource,
            Title = PopUpsLibrary.SeedCopied,
            IsActiveCloseButton = true,
            IsActiveInputField = false,
            IsActiveCancelButton = false,
            CloseAnywareClick = true
        };
    }
}


