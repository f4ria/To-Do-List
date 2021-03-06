using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;
using CommonServiceLocator;
using Newtonsoft.Json;
using To_Do_List.Model;
using JimBobBennett.MvvmLight.AppCompat;
using GalaSoft.MvvmLight.Helpers;
using To_Do_List.ViewModel;

namespace To_Do_List
{
    [Activity(Label = "Task Page", Theme = "@style/AppTheme")]
    public partial class TaskActivity
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        private TaskViewModel Vm
        {
            get
            {
                return App.Locator.Task;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.task_details_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "Second" layout resource
            SetContentView(Resource.Layout.Task_details);

            SetSupportActionBar(Toolbar);
            Toolbar.InflateMenu(Resource.Layout.task_details_menu);

            var nav = (AppCompatNavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
            Vm.PreviousTask = nav.GetAndRemoveParameter(Intent) as Task;


            if (Vm.PreviousTask != null)
            {
                Vm.EditedTask = new Task(Vm.PreviousTask.Title, Vm.PreviousTask.Date, Vm.PreviousTask.Content);

                _bindings.Add(
                    this.SetBinding(
                        () => Vm.EditedTask.Title,
                        () => Title.Text,
                        BindingMode.TwoWay
                    ));

                var saveButton = (Button) FindViewById(Resource.Id.saveButton);

                //_bindings.Add(
                //this.SetBinding(
                //() => Vm.EditedTask.Date,
                //() => Date.DateTime));
                _bindings.Add(this.SetBinding(
                    () => Vm.EditedTask.Content,
                    () => Content.Text,
                    BindingMode.TwoWay));

                var taskChangedBinding = this.SetBinding(() => Vm.EditedTask);
                _bindings.Add(taskChangedBinding);
                saveButton.SetCommand(Vm.TaskChangedCommand, taskChangedBinding);
                //Title.Text = Vm.PreviousTask.Title;
                //if (Vm.PreviousTask.Content != "")
                //{
                //    Content.Text = Vm.PreviousTask.Content;
                //}

                //if (Vm.PreviousTask.Date.HasValue)
                //{
                //    Date.Text = ((DateTime)Vm.PreviousTask.Date).ToShortDateString();
                //}

            }
            // Retrieve navigation parameter and set as current "DataContext"
            //var nav = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
            //var p = nav.GetAndRemoveParameter<string>(Intent);

            //if (string.IsNullOrEmpty(p))
            //{
            //    NavigationParameterText.Text = "No navigation parameter";
            //}
            //else
            //{
            //    NavigationParameterText.Text = "Navigation parameter: " + p;
            //}

            //GoBackButton.Click += (s, e) => nav.GoBack();
        }
    }
}