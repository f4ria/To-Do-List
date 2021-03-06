using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using JimBobBennett.MvvmLight.AppCompat;
using To_Do_List.Model;

namespace To_Do_List.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private readonly ITaskService _taskService;
        private readonly INavigationService _navigationService;
        private RelayCommand _incrementCommand;
        private ObservableCollection<Task> _tasks;
        private RelayCommand<Task> _navigateCommand;
        private bool _runClock;
        private RelayCommand<string> _showDialogCommand;

        /// <summary>
        /// Gets the IncrementCommand.
        /// Use the "mvvmr*" snippet group to create more such commands.
        /// </summary>
        //public RelayCommand IncrementCommand
        //{
        //    get
        //    {
        //        return _incrementCommand
        //               ?? (_incrementCommand = new RelayCommand(
        //                   () =>
        //                   {
        //                       WelcomeTitle = string.Format("Clicked {0} time(s)", ++_index);
        //                   }));
        //    }
        //}

        /// <summary>
        /// Gets the NavigateCommand.
        /// Goes to the second page, using the navigation service.
        /// Use the "mvvmr*" snippet group to create more such commands.
        /// </summary>
        public RelayCommand<Task> NavigateCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand<Task>(
                           task =>
                           {
                               if (!NavigateCommand.CanExecute(task))
                               {
                                   return;
                               }

                               _navigationService.NavigateTo(
                                              ViewModelLocator.TaskPageKey,
                                              task);
                            },
                           task => task != null));

            }
        }

        /// <summary>
        /// Gets the ShowDialogCommand.
        /// Use the "mvvmr*" snippet group to create more such commands.
        /// </summary>
        public RelayCommand<string> ShowDialogCommand
        {
            get
            {
                return _showDialogCommand
                       ?? (_showDialogCommand = new RelayCommand<string>(
                           async text =>
                           {
                               var dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
                               await dialogService.ShowMessage(
                                   "This is a message displayed by the dialog service | " + text,
                                   "Dialog sample",
                                   "OK",
                                   () =>
                                   {
                                       Debug.WriteLine("This code is only executed after the dialog closes (1)");
                                   });

                               Debug.WriteLine("This code is only executed after the dialog closes (2)");
                           },
                           text => !string.IsNullOrEmpty(text))); // This line disables the button if the text is null or empty
            }
        }

        /// <summary>
        /// Sets and gets the WelcomeTitle property.
        /// Changes to this property's value raise the PropertyChanged event.
        /// Use the "mvvminpc*" snippet group to create more such properties.
        /// </summary>
        public ObservableCollection<Task> Tasks
        {
            get
            {

                return _tasks;
            }
            set
            {
                Set(ref _tasks, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            ITaskService taskService,
            INavigationService navigationService)
        {
            _taskService = taskService;
            _navigationService = navigationService;

            _tasks = new ObservableCollection<Task>();

            _tasks.Add(new Task("Do the dishes", DateTime.Now));
            _tasks.Add(new Task("Buy some fruit", DateTime.Now));
            _tasks.Add(new Task("Replace the gas bottle"));
            _tasks.Add(new Task("Buy a programmer T-shirt", "One that actually makes you look cool"));
            _tasks.Add(new Task("Buy some fruit", DateTime.Now));
            _tasks.Add(new Task("Do the dishes", DateTime.Now));
            _tasks.Add(new Task("Buy some fruit", DateTime.Now));
            _tasks.Add(new Task("Replace the gas bottle"));
            _tasks.Add(new Task("Buy a programmer T-shirt", DateTime.Now, "One that actually makes you look cool"));
            _tasks.Add(new Task("Buy some fruit", DateTime.Now));
            _tasks.Add(new Task("Do the dishes", DateTime.Now));
            _tasks.Add(new Task("Buy some fruit", DateTime.Now));
            _tasks.Add(new Task("Replace the gas bottle"));

            //_taskService.GetTasks(
            //    (item, error) =>
            //    {
            //        if (error != null)
            //        {
            //            // Report error here
            //            return;
            //        }

            //        WelcomeTitle = item.Title;
            //    });
        }

        /// <summary>
        /// This method demonstrates how to use the DispatcherHelper to
        /// dispatch an instruction from a background thread
        /// back to the main thread.
        /// </summary>
        //public void StartClock()
        //{
        //    _runClock = true;

        //    System.Threading.Tasks.Task.Run(
        //        async () =>
        //        {
        //            while (_runClock)
        //            {
        //                DispatcherHelper.CheckBeginInvokeOnUI(
        //                    () =>
        //                    {
        //                        Clock = DateTime.Now.ToString("HH:mm:ss");
        //                    });

        //                await System.Threading.Tasks.Task.Delay(1000);
        //            }
        //        });
        //}


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

        //private RelayCommand _sendMessageCommand;

        ///// <summary>
        ///// Gets the SendMessageCommand.
        ///// </summary>
        //public RelayCommand SendMessageCommand
        //{
        //    get
        //    {
        //        return _sendMessageCommand
        //            ?? (_sendMessageCommand = new RelayCommand(
        //            () =>
        //            {
        //                // Any object can send messages.
        //                // For this simple demo, the message is received by App.xaml.cs
        //                // (see line 98).
        //                // This message type also allows a reply to be sent.

        //                Messenger.Default.Send(
        //                    new NotificationMessageAction<string>(
        //                        "AnyNotification",
        //                        reply =>
        //                        {
        //                            WelcomeTitle = reply;
        //                        }));
        //            }));
        //    }
        //}
    }
}