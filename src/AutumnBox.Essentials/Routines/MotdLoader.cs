﻿using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Routines
{
    internal sealed class MotdLoader
    {
        [AutoInject]
        private IXCardsManager XCardsManager { get; set; }

        [AutoInject]
        private IAppManager AppManager { get; set; }

        public void Do()
        {
           
        }
    }
}
