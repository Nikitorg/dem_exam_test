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
    /// Логика взаимодействия для KlientWindow.xaml
    /// </summary>
    public partial class KlientWindow : Window
    {
        public KlientWindow()
        {
            InitializeComponent();
            loadDG();

            //ПРОВЕРКА, ВОШЛИ ГОСТЕМ ИЛИ ЮЗЕРОМ
            if (CurrentUser.пользователь == null)
                userTxt.Text = "Гость";
            else
                userTxt.Text = CurrentUser.пользователь.ФИО;
        }
        //загрузить список товаров
        public void loadDG()
        {
            dgProduct.ItemsSource = Context.dbContext.Товар.ToList();
        }

        //МЕТОД НАЖАТИЯ КНОПКИ "НАЗАД"
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.пользователь = null;
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }
    }
}
