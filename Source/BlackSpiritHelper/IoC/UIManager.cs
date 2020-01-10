﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using BlackSpiritHelper.Core;
using Microsoft.Win32;

namespace BlackSpiritHelper
{
    /// <summary>
    /// The applications implementation of the <see cref="IUIManager"/>.
    /// </summary>
    public class UIManager : IUIManager
    {
        #region Public Properties

        /// <summary>
        /// Notification area contains and serves detailed events about notifications.
        /// You can open a new notification by calling method here <see cref="ShowNotification(NotificationBoxDialogViewModel)"/>
        /// </summary>
        public NotificationAreaDialogViewModel NotificationArea { get; } = new NotificationAreaDialogViewModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UIManager()
        {
        }

        #endregion

        #region Dialog Windows

        /// <summary>
        /// Show a notification to a user.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Task ShowNotification(NotificationBoxDialogViewModel viewModel)
        {
            return IoC.Task.Run(() =>
            {
                NotificationArea.AddNotification(viewModel);
            });
        }

        /// <summary>
        /// Displays a single message box to the user.
        /// TODO: Change into custom notification - Custom panel preparation already in MainWindow.xaml
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return IoC.Task.Run(() =>
            {
                MessageBoxResult res = MessageBox.Show(viewModel.Message, viewModel.Caption, viewModel.Button, viewModel.Icon);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        viewModel.YesAction();
                        break;
                    default:
                        break;
                }
            });
        }

        /// <summary>
        /// Open folder browser dialog.
        /// </summary>
        /// <param name="action">Action on success/select browser item.</param>
        /// <returns></returns>
        public Task ShowFolderBrowserDialog(Action<string> action)
        {
            return IoC.Task.Run(() =>
            {
                using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                {
                    // UI thread required.
                    IoC.Dispatcher.UI.BeginInvokeOrDie((Action)(() =>
                    {
                        System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            action(fbd.SelectedPath);
                        }
                    }));

                }
            });
        }

        /// <summary>
        /// Open file browser dialog.
        /// </summary>
        /// <param name="action">Action on success/select browser item.</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task ShowFileBrowserDialog(Action<string> action, string filter)
        {
            return IoC.Task.Run(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = filter;
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    action(openFileDialog.FileName);
                }
            });
        }

        #endregion

        #region Overlay Window

        /// <summary>
        /// Open overlay window.
        /// </summary>
        public void OpenOverlay()
        {
            if (OverlayWindow.Window != null)
                return;

            IoC.DataContent.OverlayData.IsOpened = true;

            OverlayWindow.Window = new OverlayWindow(new WindowInteropHelper(Application.Current.MainWindow).Handle);
            OverlayWindow.Window.Show();
        }

        /// <summary>
        /// Close overlay window.
        /// </summary>
        public void CloseOverlay()
        {
            if (OverlayWindow.Window == null)
                return;

            IoC.DataContent.OverlayData.IsOpened = false;

            OverlayWindow.Window.Close();
            OverlayWindow.Window = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

        #region Progress Window

        /// <summary>
        /// Open progress window.
        /// </summary>
        /// <param name="viewModel"></param>
        public void OpenProgressWindow(ProgressDialogViewModel viewModel = null)
        {
            if (ProgressWindow.Window != null)
                return;

            ProgressWindow.Window = new ProgressWindow();

            if (viewModel != null)
                ((ProgressWindowViewModel)ProgressWindow.Window.DataContext).VM = viewModel;

            ProgressWindow.Window.Show();
        }

        /// <summary>
        /// Close progress window.
        /// </summary>
        public void CloseProgressWindow()
        {
            if (ProgressWindow.Window == null)
                return;

            ProgressWindow.Window.Close();
            ProgressWindow.Window = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #endregion

        #region Application MainWindow

        /// <summary>
        /// Get Application's MainWindow size.
        /// </summary>
        public Vector2Double GetMainWindowSize()
        {
            return new Vector2Double(Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }

        /// <summary>
        /// Set Application's MainWindow size.
        /// </summary>
        /// <param name="size"></param>
        public void SetMainWindowSize(Vector2Double size)
        {
            Application.Current.MainWindow.Width = size.X;
            Application.Current.MainWindow.Height = size.Y;
        }

        /// <summary>
        /// Show MainWindow.
        /// </summary>
        public void ShowMainWindow()
        {
            // Activate window if it is visible in background.
            if (Application.Current.MainWindow.IsVisible)
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;

                Application.Current.MainWindow.Activate();
            }
            // Open window if it is closed in tray.
            else
            {
                Application.Current.MainWindow.Show();
                WindowViewModel.DisposeTrayIcon();
            }
        }

        /// <summary>
        /// Close MainWindow to Windows tray.
        /// </summary>
        public void CloseMainWindowToTray()
        {
            // Create notification tray icon.
            System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon("Resources/Images/Logo/icon_white.ico");
            trayIcon.DoubleClick += (s, args) => ShowMainWindow();
            trayIcon.Visible = true;
            // Create context menu for the notification icon.
            trayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Open Application").Click += (s, e) => ShowMainWindow();
            trayIcon.ContextMenuStrip.Items.Add("Donate").Click += (s, e) => System.Diagnostics.Process.Start(IoC.Application.DonationURL);
            trayIcon.ContextMenuStrip.Items.Add("-");
            trayIcon.ContextMenuStrip.Items.Add("Quit").Click += (s, e) => IoC.Application.Exit();

            // Try to dispose previous ion if exists.
            WindowViewModel.DisposeTrayIcon();
            // Assign tray icon.
            WindowViewModel.TrayIcon = trayIcon;

            // Hide MainWindow.
            Application.Current.MainWindow.Hide(); // A hidden window can be shown again, a closed one not.
        }

        #endregion
    }
}
