﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackSpiritHelper.Core
{
    public class TimerGroupSettingsFormPageViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The Group associated to this settings.
        /// </summary>
        public TimerGroupDataViewModel mFormVM;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Group associated to this settings.
        /// </summary>
        public TimerGroupDataViewModel FormVM
        {
            get
            {
                return mFormVM;
            }
            set
            {
                mFormVM = value;

                // Bind properties to the inputs.
                BindProperties();
            }
        }

        /// <summary>
        /// Title binding.
        /// </summary>
        public string Title { get; set; }

        #endregion

        #region Command Flags

        private bool mModifyCommandFlag { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to save changes in the group settings.
        /// </summary>
        public ICommand SaveChangesCommand { get; set; }

        /// <summary>
        /// The command to delete group.
        /// </summary>
        public ICommand DeleteGroupCommand { get; set; }

        /// <summary>
        /// The command to go back to the timer page.
        /// </summary>
        public ICommand GoBackCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TimerGroupSettingsFormPageViewModel()
        {
            // Create commands.
            CreateCommands();
        }

        #endregion

        /// <summary>
        /// Bind properties to the inputs.
        /// </summary>
        private void BindProperties()
        {
            if (FormVM == null)
                return;

            Title = FormVM.Title;
        }

        #region Command Methods

        /// <summary>
        /// Create commands.
        /// </summary>
        private void CreateCommands()
        {
            SaveChangesCommand = new RelayCommand(async () => await SaveChangesCommandMethodAsync());
            DeleteGroupCommand = new RelayCommand(async () => await DeleteGroupCommandMethodAsync());
            GoBackCommand = new RelayCommand(() => GoBackCommandMethod());
        }

        private async Task SaveChangesCommandMethodAsync()
        {
            await RunCommandAsync(() => mModifyCommandFlag, async () =>
            {
                if (!TimerGroupDataViewModel.ValidateInputs(Title))
                {
                    // Some error occured during saving changes of the group.
                    _ = IoC.UI.ShowNotification(new NotificationBoxDialogViewModel()
                    {
                        Title = "INVALID VALUES",
                        Message = $"Some of the entered values are invalid.{Environment.NewLine}" +
                                  $"Group Name can contain only letters and numbers, {TimerGroupDataViewModel.TitleAllowMinChar} characters at minimum and {TimerGroupDataViewModel.TitleAllowMaxChar} characters at maximum.",
                        Result = NotificationBoxResult.Ok,
                    });

                    return;
                }

                // Save changes.
                FormVM.Title = Title.Trim();

                // Resort groups alphabetically.
                IoC.DataContent.TimerData.SortGroupList();

                // Log it.
                IoC.Logger.Log($"Settings changed: timer group '{FormVM.Title}'.", LogLevel.Info);

                // Move back to the page.
                GoBackCommandMethod();

                await Task.Delay(1);
            });
        }

        private async Task DeleteGroupCommandMethodAsync()
        {
            await RunCommandAsync(() => mModifyCommandFlag, async () =>
            {
                if (!IoC.DataContent.TimerData.DestroyGroup(FormVM))
                {
                    // Some error occured during deleting the group.
                    _ = IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        Caption = "Cannot delete the group!",
                        Message = $"The group is running timers or it is the last existing group!{Environment.NewLine}" +
                                  $"Please, stop all the timers in the group first.{Environment.NewLine}Number of timers in this group is {FormVM.TimerList.Count}.",
                        Button = System.Windows.MessageBoxButton.OK,
                        Icon = System.Windows.MessageBoxImage.Warning,
                    });

                    return;
                }

                // Move back to the page.
                GoBackCommandMethod();

                await Task.Delay(1);
            });
        }

        private void GoBackCommandMethod()
        {
            // Move back to the page.
            IoC.Application.GoToPage(ApplicationPage.Timer);
        }

        #endregion
    }
}
