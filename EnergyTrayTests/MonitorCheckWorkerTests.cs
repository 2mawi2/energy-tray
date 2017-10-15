﻿using EnergyTray.Application.AppSettings;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Worker;
using Moq;
using Xunit;

namespace EnergyTrayTests
{
    public class MonitorCheckWorkerTests
    {
        private readonly Mock<IPowerProcessor> _powerProcessor = new Mock<IPowerProcessor>();
        private readonly Mock<IWorkerSettings> _workerSettings = new Mock<IWorkerSettings>();
        private readonly Mock<IBackgroundWorkerAdapter> _backgroundWorker = new Mock<IBackgroundWorkerAdapter>();

        public MonitorCheckWorkerTests()
        {
            _backgroundWorker.SetupAllProperties();
        }


        private IMonitorCheckWorker CreateMonitorCheckWorker() => new MonitorCheckWorker(
            _powerProcessor.Object,
            _workerSettings.Object,
            _backgroundWorker.Object);

        [Fact]
        public void MonitorCheckWorkerTest_SetupBackgroundWorker()
        {
            CreateMonitorCheckWorker();
            _backgroundWorker.Verify(i => i.RunWorkerAsync());
            _backgroundWorker.VerifySet(i => i.WorkerReportsProgress = true);
            _backgroundWorker.VerifySet(i => i.WorkerSupportsCancellation = true);
        }

        [Fact]
        public void MonitorCheckWorkerTest_DoWorkEvent()
        {
            CreateMonitorCheckWorker();
        }
    }
}