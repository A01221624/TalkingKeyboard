// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Shell
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    ///     Interaction logic for the application base.
    /// </summary>
    public partial class App
    {
        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var pname = Process.GetProcessesByName("presage_wcf_service_system_tray");
            if (pname.Length == 0)
            {
                try
                {
                    var presageProcess = new Process();
                    var path = Environment.ExpandEnvironmentVariables("%ProgramW6432%")
                               + "\\presage\\bin\\presage_wcf_service_system_tray.exe";
                    presageProcess.StartInfo.FileName = path;
                    presageProcess.EnableRaisingEvents = false;

                    presageProcess.Start();
                }
                catch (Exception)
                {
                    Console.WriteLine(
                        "Could not find presage in the default folder (%PROGRAMFILES%\\presage)." +
                        "Have you installed it? It is used for providing auto-completion.");
                }
            }

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}