using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FinanceHub.Controllers;
using System.IO;
using System.IO.Abstractions;
using FinanceHub.DataBase;
using FinanceHub.Views;


namespace FinanceHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal Users _users;
        internal resetToInputView _resetInputView;
        public MainWindow()
        {
            _users = new Users(new FileSystem(), new DBWrapper());
            _resetInputView = () =>
            {
                InputGrid.Children.Clear();
            };
            InitializeComponent();

            var myTabHolder = new TabHolder(new FileSystem());
            FinanceHubSettings settings = myTabHolder.GetStartingTab();
            Dispatcher.BeginInvoke((Action)(() => MyTabControl.SelectedIndex = settings.CurrentTab));

           
            User? myUser = _users.GetCurrentUser();
            if (myUser == null)
            {
               AddUser MyAddUser = new AddUser(_users, _resetInputView);
                InputGrid.Children.Add(MyAddUser);
           
            }

        }

       private void OnTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int current = ((TabControl)sender).SelectedIndex;
            var myTabHolder = new TabHolder(new FileSystem());
            myTabHolder.SaveTab(current);
        }



        public void resetCallBack()
        {
            InputGrid.Children.Clear();
            InputData MyAddUser = new InputData(_users);
            InputGrid.Children.Add(MyAddUser);
        }
      


        private void muSwitchUser_click(object sender, RoutedEventArgs e)
        {
            resetToInputView callback = resetCallBack;
            SwitchUser mySwitchUser = new SwitchUser(_users, callback);
            InputGrid.Children.Clear();
            InputGrid.Children.Add(mySwitchUser);
        }

        private void muCreateUser_click(object sender, RoutedEventArgs e)
        {
            AddUser MyAddUser = new AddUser(_users, _resetInputView);
            InputGrid.Children.Clear();
            InputGrid.Children.Add(MyAddUser);
        }

        private void muDeleteuser_click(object sender, RoutedEventArgs e)
        {
            DeleteUser MyDeleteUser = new DeleteUser(_users, _resetInputView);
            InputGrid.Children.Clear();
            InputGrid.Children.Add(MyDeleteUser);
        }

    }

    public delegate void resetToInputView();
}