﻿using BlackSpiritHelper.Core;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BlackSpiritHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Dispatcher Unhandled Exception

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Log error.
            if (IoC.Logger != null)
            {
                IoC.Logger.Log($"An unhandled exception occurred: {e.Exception.Message}", LogLevel.Fatal);
            }
            MessageBox.Show($"An unhandled exception just occurred: {e.Exception.Message}. {Environment.NewLine}Please, contact the developers to be able to fix that issue.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            e.Handled = true;
        }

        #endregion

        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs.
            base.OnStartup(e);

            // Configuration.
            ApplicationSetup();

            // Log it.
            IoC.Logger.Log("Application starting up...", LogLevel.Debug);

            // Check for application available updates.
            checkForUpdates();
            
            // Show the main window.
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application read for use.
        /// </summary>
        private void ApplicationSetup()
        {
            // Setup IoC.
            IoC.Setup();

            // Bind Logger.
            IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLogFactory(new[] 
            {
                new FileLogger(IoC.Get<ApplicationViewModel>().ApplicationName.Replace(' ', '_').ToLower() + "_log.txt"),
            }));

            // Bind task manager.
            IoC.Kernel.Bind<ITaskManager>().ToConstant(new TaskManager());

            // Bind file manager.
            IoC.Kernel.Bind<IFileManager>().ToConstant(new FileManager());

            // Bind Application data.
            IoC.Kernel.Bind<ApplicationDataContent>().ToConstant(new ApplicationDataContent());
        }

        /// <summary>
        /// Checks if a new version of the SW is available.
        /// </summary>
        private void checkForUpdates()
        {
            string title = (Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute).Title;
            Version currVersion = Assembly.GetExecutingAssembly().GetName().Version;

            // TODO: Version check.
            if (true)
            {
                return;
            }
            
            // Dialog window.
            string messageBoxText = "New version of " + title + " is available!\r\nDo you want to download it now?";
            string caption = title + " - New version available!";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
