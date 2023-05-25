using CrudModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;



namespace LoginPage.ViewModel
{
    public class InfoViewModel : ViewModelBase
    {
        public InfoViewModel()
        {
            CustomerEditDetails();
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);


        }
        private int _username;
        private string _firstname;
        private string _address;
        private bool _isReadOnly = true;
        private int _addressId;

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
            //    var url = $"https://localhost:7172/customer/EditCustomerById/{Username}";
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




            using (HttpClient client = new HttpClient())
            {
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(custModel);
                var stringContent = new StringContent(jsonString, UnicodeEncoding.UTF8, "application/json");

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                var response = await client.PutAsync($"https://localhost:7172/customer/EditCustomerById/{Username}", stringContent);

                var result = await response.Content.ReadAsStringAsync();
                watch.Stop();
                Console.WriteLine($"Execution Time : {watch.ElapsedMilliseconds} ms");

                if (result != null)
                {

                    Console.WriteLine("Updated Successfully");

                }
                else
                {
                    MessageBox.Show("Could not be updated please try again", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

                }



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
