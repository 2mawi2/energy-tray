﻿using System.Diagnostics;

namespace EnergyTray.Application.PowerManagement
{
    public class Cmd : ICmd
    {
        private readonly string _cmdPath;

        public Cmd(string cmdPath = @"C:\Windows\System32\cmd.exe")
        {
            _cmdPath = cmdPath;
        }

        public string ExecCommand(string command, DataReceivedEventHandler callback = null)
        {
            var cmdProcess = StartCmd();
            if (callback != null)
            {
                cmdProcess.OutputDataReceived += callback;
            }
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            //cmdProcess.BeginOutputReadLine();
            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");

            var output = "";
            string standard_output;
            while ((standard_output = cmdProcess.StandardOutput.ReadLine()) != null)
            {
                output += standard_output + System.Environment.NewLine;
            }


            cmdProcess.WaitForExit();

            return output;
        }


        private Process StartCmd() => new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _cmdPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            }
        };
    }
}