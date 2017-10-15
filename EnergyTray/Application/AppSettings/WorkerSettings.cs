﻿using EnergyTray.Application.PowerManagement;

namespace EnergyTray.Application.AppSettings
{
    public class WorkerSettings : IWorkerSettings
    {
        public bool IsAutoChangerEnabled
        {
            get => EnergyTraySetttings.Load().IsAutoChangerEnabled;
            set
            {
                var settings = EnergyTraySetttings.Load();
                settings.IsAutoChangerEnabled = value;
                settings.Save();
            }
        }
    }
}