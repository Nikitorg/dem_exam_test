using demExam.Classes;
using demExam.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demExam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Создаем отдельный метод для авторизации, с целью упрощения дальнейшего тестирования. Передаем 2 параметра - Логин, пароль
        public void autorization(string login, string password)
        {
            try
            {
                //получаем первого пользователя из БД, удовлетворяющего условию.
                var user = Context.dbContext.Пользователь.FirstOrDefault(a => a.Логин == login && a.Пароль == password);

                //Проверка, Есть ли такой пользователь в БД
                if (user != null)
                {
                    //Сохраняем текущего пользователя в классе
                    CurrentUser.пользователь = user;
                    MessageBox.Show("Добро пожаловать, " + user.ФИО.ToString(), "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Проверяем роль
                    switch (user.РольСотрудника)
                    {
                        case "Авторизированный клиент":
                            
                            

                            //открываем окно(зависимость от роли)
                            KlientWindow klientWindow = new KlientWindow();
                            this.Close();
                            klientWindow.ShowDialog();

                            break;
                        case "Менеджер":

                            //открываем окно(зависимость от роли)
                            MenuWindow menuWindow = new MenuWindow();
                            this.Close();
                            menuWindow.ShowDialog();
                            break;
                        case "Администратор":

                            //открываем окно(зависимость от роли)
                            MenuWindow menuWindow1 = new MenuWindow();
                            this.Close();
                            menuWindow1.ShowDialog();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверно введен логин или пароль!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void btnAutoriz_Click(object sender, RoutedEventArgs e)
        {   //Можно раскоментировать данные метод, Чтобы 100 раз не заполнять поля логин и пароль (вставить данные своих пользователей)
            autorization("94d5ous@gmail.com", "uzWC67");

            //Передаем данные из TextBox в метод
            //autorization(logTxtBox.Text.Trim(), passPsBox.Password.Trim());
        }

        private void btnAutorizGost_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добро пожаловать, Гость!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            KlientWindow klientWindow = new KlientWindow();
            this.Close();
            klientWindow.ShowDialog();
            
        }
    }
}
