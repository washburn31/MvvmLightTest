using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLightTest.Model;
using MvvmLightTest.Model.Message;
using MvvmLightTest.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmLightTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataAccessService dataAccessService;
        private ObservableCollection<Employees> employees;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataAccessService dataAccessService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            this.dataAccessService = dataAccessService;
            this.Employees = new ObservableCollection<Employees>();
            this.EmpInfo = new Employees();
            this.ReadAllCommand = new RelayCommand(this.GetEmployees);
            this.SaveCommand = new RelayCommand<Employees>(this.SaveEmployee);
            this.SearchCommand = new RelayCommand(this.SearchEmployee);
            this.SendEmployeeCommand = new RelayCommand<Employees>(SendEmployeeInfo);
            this.ReceiveEmployeeInfo();
        }

        public ObservableCollection<Employees> Employees
        {
            get { return this.employees; }
            set
            {
                this.employees = value;
                this.RaisePropertyChanged(nameof(this.Employees));
            }
        }

        private Employees empInfo;

        public Employees EmpInfo
        {
            get { return this.empInfo; }
            set
            {
                this.empInfo = value;
                this.RaisePropertyChanged(nameof(this.EmpInfo));
            }
        }

        private string empName;

        public string EmpName
        {
            get { return this.empName; }
            set
            {
                this.empName = value;
                this.RaisePropertyChanged(nameof(this.EmpName));
            }
        }

        public RelayCommand ReadAllCommand { get; set; }
        public RelayCommand<Employees> SaveCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }

        public RelayCommand<Employees> SendEmployeeCommand { get; set; }

        private void GetEmployees()
        {
            this.Employees.Clear();
            foreach (var item in dataAccessService.GetEmployees())
            {
                this.Employees.Add(item);
            }
        }

        /// <summary>
        /// Method to Save Employees. Once the Employee is added in the database
        /// it will be added in the Employees observable collection and Property Changed will be raised
        /// </summary>
        /// <param name="emp"></param>
        private void SaveEmployee(Employees emp)
        {
            this.EmpInfo.EmpNo = dataAccessService.CreateEmployee(emp);
            if (this.EmpInfo.EmpNo != 0)
            {
                this.Employees.Add(this.EmpInfo);
                this.RaisePropertyChanged("EmpInfo");
            }
        }

        /// <summary>
        /// The method to search Employee baseed upon the EmpName
        /// </summary>
        private void SearchEmployee()
        {
            this.Employees.Clear();
            var Res = from e in dataAccessService.GetEmployees()
                      where e.EmpName.IndexOf(this.EmpName, StringComparison.CurrentCultureIgnoreCase) >= 0
                      select e;
            foreach (var item in Res)
            {
                this.Employees.Add(item);
            }
        }

        /// <summary>
        /// The method to send the selected Employee from the DataGrid on UI
        /// to the View Model
        /// </summary>
        /// <param name="emp"></param>
        private void SendEmployeeInfo(Employees emp)
        {
            if (emp != null)
            {
                Messenger.Default.Send<MessageCommunicator>(new MessageCommunicator()
                {
                    Emp = emp
                });
            }
        }

        /// <summary>
        /// The Method used to Receive the send Employee from the DataGrid UI
        /// and assigning it the the EmpInfo Notifiable property so that
        /// it will be displayed on the other view
        /// </summary>
        private void ReceiveEmployeeInfo()
        {
            if (this.EmpInfo != null)
            {
                Messenger.Default.Register<MessageCommunicator>(this, (emp) =>
                {
                    this.EmpInfo = emp.Emp;
                });
            }
        }
    }
}