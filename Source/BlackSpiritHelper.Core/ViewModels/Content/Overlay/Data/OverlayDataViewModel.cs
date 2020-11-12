﻿using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace BlackSpiritHelper.Core
{
    public class OverlayDataViewModel : ADataContentBaseViewModel<OverlayDataViewModel>
    {
        #region Private Members

        /// <summary>
        /// Says, if the overlay is shown (opened) or not.
        /// </summary>
        private bool mIsOpened = false;

        /// <summary>
        /// Allow opening the overlay on application start.
        /// </summary>
        private bool mAllowOpenOnStart = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// Says, if the overlay is shown (opened) or not.
        /// </summary>
        [XmlIgnore]
        public bool IsOpened
        {
            get => mIsOpened;
            set
            {
                mIsOpened = value;
                OpenOnStart = AllowOpenOnStart ? value : false;
            }
        }

        /// <summary>
        /// Allow opening the overlay on application start.
        /// </summary>
        public bool AllowOpenOnStart
        {
            get => mAllowOpenOnStart;
            set
            {
                mAllowOpenOnStart = value;
                OpenOnStart = value ? IsOpened : false;
            }
        }

        /// <summary>
        /// Says, if the overlay should be opened on application start (the next start).
        /// </summary>
        public bool OpenOnStart { get; set; } = false;

        /// <summary>
        /// Says, if the dragging with overlay object is locked or not.
        /// </summary>
        public bool IsDraggingLocked { get; set; } = false;

        /// <summary>
        /// Base Overlay data view model
        /// </summary>
        public OverlayBaseDataViewModel BaseOverlay { get; set; } = new OverlayBaseDataViewModel();

        /// <summary>
        /// List of all types of <see cref="Orientation"/>.
        /// </summary>
        [XmlIgnore]
        public Orientation[] OrientationList { get; private set; } = (Orientation[])Enum.GetValues(typeof(Orientation));

        /// <summary>
        /// List of all types of <see cref="OverlaySize"/>.
        /// </summary>
        [XmlIgnore]
        public BaseOverlaySize[] BaseOverlaySizeList { get; private set; } = (BaseOverlaySize[])Enum.GetValues(typeof(BaseOverlaySize));

        [XmlIgnore]
        public override bool IsRunning
        {
            get => false;
            protected set => throw new NotImplementedException();
        }

        #endregion

        #region Commands

        /// <summary>
        /// The command to lock overlay dragging.
        /// </summary>
        [XmlIgnore]
        public ICommand LockOverlayDraggingCommand { get; set; }

        /// <summary>
        /// The command to show main window.
        /// </summary>
        [XmlIgnore]
        public ICommand ShowMainWindowCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OverlayDataViewModel()
        {
            // Create commands.
            CreateCommands();
        }

        protected override void InitRoutine(params object[] parameters)
        {
        }

        protected override void SetDefaultsRoutine()
        {
        }

        protected override void DisposeRoutine()
        {
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Create commands.
        /// </summary>
        private void CreateCommands()
        {
            LockOverlayDraggingCommand = new RelayCommand(() => LockOverlayCommandMethod());
            ShowMainWindowCommand = new RelayCommand(ShowMainWindowCommandMethod);
        }

        /// <summary>
        /// Lock/Unlock Overlay dragging.
        /// </summary>
        private void LockOverlayCommandMethod()
        {
            IsDraggingLocked = !IsDraggingLocked;
        }

        /// <summary>
        /// Trigger to show main window.
        /// </summary>
        private void ShowMainWindowCommandMethod()
        {
            IoC.UI.ShowMainWindow();
        }

        #endregion
    }
}
