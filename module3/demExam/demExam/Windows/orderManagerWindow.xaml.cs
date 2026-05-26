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
    /// Логика взаимодействия для orderManagerWindow.xaml
    /// </summary>
    public partial class orderManagerWindow : Window
    {
        public orderManagerWindow()
        {
            InitializeComponent();
            loadDG();
            //ФИО СПРАВА В УГЛУ
            userTxt.Text = CurrentUser.пользователь.ФИО;
        }

        //МЕТОД НАЖАТИЯ КНОПКИ "НАЗАД"
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            MenuWindow menuWindow = new MenuWindow();
            this.Close();
            menuWindow.ShowDialog();
        }

        //Загрузка датагрида
        public void loadDG()
        {
            dgOrder.ItemsSource = Context.dbContext.Заказ.ToList();
        }
    }
}
