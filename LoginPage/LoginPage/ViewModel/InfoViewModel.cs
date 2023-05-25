using CrudModels;
using LoginPage.View;
using Newtonsoft.Json;
using Petnet.ActivityTracker;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace LoginPage.ViewModel
{
    public class InfoViewModel : ViewModelBase
    {
        public InfoViewModel()
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Info view Started");

            _activityTracker = new ActivityTracker();
            TrackActivity();
            CustomerEditDetails();
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }




        private DispatcherTimer _dispatcherTimer;
        private int _username;
        private string _firstname;
        private string _address;
        private bool _isReadOnly = true;
        private int _addressId;
        private string _errorMessage;
        public IActivityTracker _activityTracker { get; set; }
        private DateTime timerStart;


        // check the activity
        private void TrackActivity()
        {
            if (_dispatcherTimer != null)
            {
                StopDispatcherTimer();
            }
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += CheckActivity;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
            timerStart = DateTime.Now;
        }



        private void CheckActivity(object sender, EventArgs e)
        {
            var lastActivity = _activityTracker.LastActivity;
            var logoutTime = lastActivity.AddSeconds(10);
            var maxlogoutTime = lastActivity.AddSeconds(15);



            if (DateTime.Now > maxlogoutTime)
            {
                Console.WriteLine("Token has Expired");
                Console.WriteLine(DateTime.Now);
                InfoView.m_counter = 0;
                MessageBox.Show("Token expired please press ok to login again", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                // InfoView.m_counter = 0;
                MainWindow.ViewIndex = 3;

            }
            else if (DateTime.Now < logoutTime)
            {
                Console.WriteLine("Token is valid");
                Console.WriteLine(DateTime.Now);
            }

            else if (DateTime.Now > logoutTime && DateTime.Now < maxlogoutTime && (DateTime.Now - timerStart).TotalSeconds > 10)
            {
                InfoView.m_counter = 0;
                Console.WriteLine(DateTime.Now);
                SendToken();
                Console.WriteLine("Token is regenetated");
                _dispatcherTimer.Stop();
                _dispatcherTimer.Start();
                timerStart = DateTime.Now;
            }
        }



        private void StopDispatcherTimer()
        {
            _dispatcherTimer.Tick -= CheckActivity;
            _dispatcherTimer.Stop();
        }




        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public int Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
                OnPropertyChanged("Firstname");
            }

        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }



        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public int AddressId
        {
            get
            {
                return _addressId;
            }
            set
            {
                _addressId = value;

            }
        }


        // Commands
        public ICommand SaveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RecoverPasswordCommand { get; }

        public ICommand ShowPasswordCommand { get; }

        public ICommand RememberPasswordCommand { get; }



        public void ExecuteEditCommand(object obj)
        {
            IsReadOnly = false;

        }




        public async void UpdateCustomer(int Username)
        {

            var custModel = new CustomerEditDetails
            {
                Id = Username,
                FirstName = Firstname,
                AddressId = AddressId,
                Address = new AddressDetails
                {
                    AddressId = AddressId,
                    Address = Address
                }

            };


            bool checkExpiredToken(string ReceivedAccessToken, string ReceivedRefreshToken)
            {
                if (ReceivedRefreshToken == MainWindow.refreshToken && ReceivedAccessToken == MainWindow.token)
                {
                    return true;
                }
                return false;
            }




            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + MainWindow.token);
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(custModel);
                var stringContent = new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json");

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                var response = await client.PutAsync($"https://localhost:7172/customer/EditCustomerById/{Username}", stringContent);

                var result = await response.Content.ReadAsStringAsync();
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                watch.Reset();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Updated SuccessFully");
                }
                else
                {
                    //  MessageBox.Show("Token expired please press ok to login again", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    Console.WriteLine("Token Expired Please login again");
                    MainWindow.ViewIndex = 3;
                }

            }

        }




        // post method to send the previous refresh token to the server side to check the validity


        public async void SendToken()
        {
            var newUrl = "https://localhost:7172/customer/ReceivedOldToken";
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Custom", MainWindow.refreshToken);
                var msg = new HttpRequestMessage(HttpMethod.Get, newUrl);
                msg.Headers.Add("User-Agent", "C# Program");
                var res = client.SendAsync(msg).Result;
                var content = await res.Content.ReadAsStringAsync();
                var stringContent = Convert.ToString(content);

                var contentResponse = JsonConvert.DeserializeObject<List<string>>(stringContent);
                var firstResponse = contentResponse[0];
                var secondResponse = contentResponse[1];
                var AccessToken = Convert.ToString(firstResponse);
                var RefreshToken = Convert.ToString(secondResponse);
                MainWindow.token = AccessToken;
                MainWindow.refreshToken = RefreshToken;

            }
        }




        public void ExecuteSaveCommand(object obj)
        {
            UpdateCustomer(Username);
        }



        private void GetDetails(CustomerEditDetails customer)
        {
            Username = customer.Id;
            Firstname = customer.FirstName;
            Address = customer.Address?.Address;
            AddressId = customer.AddressId;
        }


        public async void CustomerEditDetails()
        {
            var url = $"https://localhost:7172/customer/GetFullCustomerDetailById/{MainWindow.UserId}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + MainWindow.token);
                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Add("User-Agent", "C# Program");
                var res = client.SendAsync(msg).Result;
                var content = await res.Content.ReadAsStringAsync();
                var contentResponse = JsonConvert.DeserializeObject<CustomerEditDetails>(content);
                GetDetails(contentResponse);
            }
        }




        private bool CanExecuteEditCommand(object obj)
        {
            bool canExecute;
            if ((Username.GetType() != typeof(int)) || (Firstname == "" || Address == ""))
            {
                canExecute = false;
            }
            else
            {
                canExecute = true;
            }
            return canExecute;
        }



        private bool CanExecuteSaveCommand(object obj)
        {
            bool canExecute;
            if (Firstname == "" || Address == "")
            {
                canExecute = false;
            }
            else
            {
                canExecute = true;
            }
            return canExecute;
        }

    }
}
