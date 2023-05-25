using LoginPage.View;

using System.Windows;
using System.Windows.Controls;


namespace LoginPage
{

    public partial class MainWindow : Window
    {
        private static int _viewIndex;
        private static UserControl viewHolder;

        public static int ViewIndex
        {
            get => _viewIndex;
            set
            {
                _viewIndex = value;

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
        public static string token { get; set; }
        public static string refreshToken { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            viewHolder = new UserControl();
            var login = new LoginView();
            viewHolder.Content = login;
            this.Content = viewHolder;
        }

    }
}
