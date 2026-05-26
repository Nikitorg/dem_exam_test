using demExam.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace demExam.Windows
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            userTxt.Text = CurrentUser.пользователь.ФИО;
        }
        // кнопка "НАЗАД"
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.пользователь = null;
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }
        //т.к меню одно, СДЕЛАЛ ПРОВЕРКУ РОЛИ. Можно сделать разные меню для админа и менеджера, но запутаетесь в окнах, так проще
        public bool isCheckRole()
        {
            if (CurrentUser.пользователь.РольСотрудника == "Менеджер")
                return true;
            else
                return false;
        }

        //ОТКРЫТИЕ ОКНА ТОВАРОВ В МЕНЮ
        private void productView_Click(object sender, RoutedEventArgs e)
        {
            if(isCheckRole() == true)
            {
                productManagerWindow productManagerWindow = new productManagerWindow();
                this.Close();
                productManagerWindow.ShowDialog();
            }
            else
            {
                adminProductWindow adminProductWindow  = new adminProductWindow();
                this.Close();
                adminProductWindow.ShowDialog();
            }
        }

        //ОТКРЫТИЕ ОКНА ЗАКАЗОВ В МЕНЮ
        private void orderVIew_Click(object sender, RoutedEventArgs e)
        {
            if (isCheckRole() == true)
            {
                orderManagerWindow orderManagerWindow = new orderManagerWindow();
                this.Close();
                orderManagerWindow.ShowDialog();
            }
            else
            {
                orderAdminWindow orderAdminWindow = new orderAdminWindow();
                this.Close();
                orderAdminWindow.ShowDialog();
            }
        }
    }
}
