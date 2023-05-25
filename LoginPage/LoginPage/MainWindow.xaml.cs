using LoginPage.View;

using System.Windows;
using System.Windows.Controls;


namespace LoginPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currView;
        private static int _viewIndex;
        private static UserControl viewHolder;

        public static int ViewIndex
        {
            get => _viewIndex;
            set
            {
                _viewIndex = value;
                //viewHolder.Content = new InfoView();
                switch (value)
                {
                    case 0:
                        viewHolder.Content = new LoginView(); ;
                        break;
                    case 1:
                        if (UserId == null)
                        {
                            MessageBox.Show("Please login to load User Info");
                            return;
                        }
                        viewHolder.Content = new InfoView();
                        break;
                    default:
                        viewHolder.Content = new LoginView();
                        break;

                }
            }
        }

        public static int? UserId { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            viewHolder = new UserControl();
            var login = new LoginView();
            viewHolder.Content = login;
            this.Content = viewHolder;
        }


        //public string CurrentView
        //{
        //    get => _currView;
        //    set
        //    {
        //        if (_currView == value)
        //        {
        //            return;
        //        }
        //        _currView = value;
        //        viewHolder.Content = new InfoView();
        //    }
        //}
    }
}
