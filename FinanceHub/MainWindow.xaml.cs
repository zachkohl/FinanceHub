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
using FinanceHub.Services;
using System.IO;
using System.IO.Abstractions;
using FinanceHub.DataBase;
using FinanceHub.View;


namespace FinanceHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal UsersService _users=null!;
        internal resetToInputView _resetInputView=null!;
        public MainWindow()
        {
            //_users = new Users(new FileSystem(), new DBWrapper());
            //_users.GetCurrentUser();
            //_resetInputView = () =>   
            //{
            //    InputGrid.Children.Clear();
            //    InputDataArea area = new InputDataArea(_users);
            //    InputGrid.Children.Add(area);
            //};
            InitializeComponent();

            //var myTabHolder = new TabHolder(new FileSystem());
            //FinanceHubSettings settings = myTabHolder.GetStartingTab();
            //Dispatcher.BeginInvoke((Action)(() => MyTabControl.SelectedIndex = settings.CurrentTab));
            //_resetInputView();
            //DataTabHolder.Children.Add(new DataTabView(_users));
            //User? myUser = _users.GetCurrentUser();
            //if (myUser == null)
            //{
            //   AddUser MyAddUser = new AddUser(_users, _resetInputView);
            //    InputGrid.Children.Add(MyAddUser);
           
            //}

        }

       //private void OnTabSelectionChanged(object sender, SelectionChangedEventArgs e)
       // {
       //     int current = ((TabControl)sender).SelectedIndex;
       //     var myTabHolder = new TabHolder(new FileSystem());
       //     myTabHolder.SaveTab(current);
       // }



      


       // private void muSwitchUser_click(object sender, RoutedEventArgs e)
       // {
       //     //SwitchUser mySwitchUser = new SwitchUser(_users, _resetInputView);
       //     //InputGrid.Children.Clear();
       //     //InputGrid.Children.Add(mySwitchUser);
       // }

       // private void muCreateUser_click(object sender, RoutedEventArgs e)
       // {
       //     //AddUser MyAddUser = new AddUser(_users, _resetInputView);
       //     //InputGrid.Children.Clear();
       //     //InputGrid.Children.Add(MyAddUser);
       // }

       // private void muDeleteuser_click(object sender, RoutedEventArgs e)
       // {
       //     //DeleteUser MyDeleteUser = new DeleteUser(_users, _resetInputView);
       //     //InputGrid.Children.Clear();
       //     //InputGrid.Children.Add(MyDeleteUser);
       // }

    }

    public delegate void resetToInputView();
}