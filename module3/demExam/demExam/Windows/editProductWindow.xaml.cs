using demExam.Classes;
using demExam.dbModel;
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
    /// Логика взаимодействия для editProductWindow.xaml
    /// </summary>
    public partial class editProductWindow : Window
    {
        public Товар текущийТовар;
        public List<TextBox> текстовыеПоля;
        public List<ComboBox> выпадющиеСписки;
        public editProductWindow(Товар товар)
        {
            InitializeComponent();
            текущийТовар = товар;
            loadCmbBox();
            выпадющиеСписки = new List<ComboBox>();
            текстовыеПоля = new List<TextBox>();
            loadTxtBoxComboBoxForList();
            loadTxt();

            //ФИО СПРАВА В УГЛУ
            userTxt.Text = CurrentUser.пользователь.ФИО;

        }

        //ЗАГРУЖАЕМ ТЕКСТОВЫЕ ПОЛЯ
        public void loadTxt()
        {
            nameProdctTxtBox.Text = текущийТовар.НаименованиеТовара;
            articleProductTxtBox.Text = текущийТовар.Артикул;
            infoProductTxtBox.Text = текущийТовар.ОписаниеТовара;
            postavProductTxtBox.Text = текущийТовар.Поставщик;
            priceProductTxtBox.Text = текущийТовар.Цена.ToString();
            edProductTxtBox.Text = текущийТовар.ЕдиницаИзмерения;
            countProductTxtBox.Text = текущийТовар.КоличествоНаСкладе.ToString();
            actionProductTxtBox.Text = текущийТовар.ДействующаяСкидка.ToString();
            catCmbBox.Text = текущийТовар.КатегорияТовара;
            proizCmbBox.Text = текущийТовар.Производитель;
        }

        //ЗАГРУЖАЕМ textboxЫ и комбобоксы в список, чтобы выводить конкретные ошибки(например: Заполните поле - Наименование товара). Если вам похуй - пропустите этот шаг, и удалите вызов данного метода в конктрукторе.
        public void loadTxtBoxComboBoxForList()
        {
            текстовыеПоля.Add(nameProdctTxtBox);
            текстовыеПоля.Add(articleProductTxtBox);
            текстовыеПоля.Add(infoProductTxtBox);
            текстовыеПоля.Add(postavProductTxtBox);
            текстовыеПоля.Add(priceProductTxtBox);
            текстовыеПоля.Add(edProductTxtBox);
            текстовыеПоля.Add(countProductTxtBox);
            текстовыеПоля.Add(actionProductTxtBox);

            выпадющиеСписки.Add(catCmbBox);
            выпадющиеСписки.Add(proizCmbBox);
        }

        //ВЫХОД
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            adminProductWindow adminProductWindow = new adminProductWindow();
            this.Close();
            adminProductWindow.ShowDialog();
        }

        //загрузка комбобоксов
        public void loadCmbBox()
        {
            catCmbBox.ItemsSource = Context.dbContext.Товар.Select(a => a.КатегорияТовара).Distinct().ToList();
            proizCmbBox.ItemsSource = Context.dbContext.Товар.Select(a => a.Производитель).Distinct().ToList();
        }

        //СОХРАНЕНИЕ ТОВАРА(НАЖАТИЕ НА КНОПКУ)
        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            //МИЛЛИАРД ПРОВЕРОК ПРИ СОХРАНЕНИИ
            try
            {
                foreach (var item in текстовыеПоля)
                {
                    if (item.Text == "")
                    {
                        MessageBox.Show("Выберете элемент в поле - " + item.Tag, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                foreach (var item in выпадющиеСписки)
                {
                    if (item.Text == "")
                    {
                        MessageBox.Show("Выберете элемент в поле - " + item.Tag, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (Convert.ToDecimal(priceProductTxtBox.Text) < 0)
                {
                    MessageBox.Show("Цена не может быть отрицательной", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (Convert.ToDecimal(countProductTxtBox.Text) < 0)
                {
                    MessageBox.Show("Количество не может быть отрицательным", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (Convert.ToDecimal(actionProductTxtBox.Text) < 0 || Convert.ToDecimal(actionProductTxtBox.Text) > 100)
                {
                    MessageBox.Show("Скидка не может быть отрицательной, а также быть больше 100%", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //СОХРАНЕНИЕ ИЗМЕНЕННЫХ ДАННЫХ

                текущийТовар.НаименованиеТовара = nameProdctTxtBox.Text;
                текущийТовар.Поставщик = postavProductTxtBox.Text;
                текущийТовар.Производитель = proizCmbBox.Text;
                текущийТовар.КатегорияТовара = catCmbBox.Text;
                текущийТовар.ЕдиницаИзмерения = edProductTxtBox.Text;
                текущийТовар.Цена = Convert.ToDouble(priceProductTxtBox.Text);
                текущийТовар.ДействующаяСкидка = Convert.ToInt32(actionProductTxtBox.Text);
                текущийТовар.КоличествоНаСкладе = Convert.ToInt32(countProductTxtBox.Text);
                текущийТовар.ОписаниеТовара = infoProductTxtBox.Text;

                Context.dbContext.SaveChanges();

                MessageBox.Show("Изменение товара успешно сохранен!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                adminProductWindow adminProductWindow = new adminProductWindow();
                this.Close();
                adminProductWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var article = Context.dbContext.СоставЗаказа.Select(a => a.АртикулЗаказа).Distinct().ToList();
            foreach(var item in article)
            {
                if (текущийТовар.Артикул == item)
                {
                    MessageBox.Show("Нельзя удалить товар, входящий в состав заказов!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            Context.dbContext.Товар.Remove(текущийТовар);
            Context.dbContext.SaveChanges();

            MessageBox.Show("Товар успешно удален!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            adminProductWindow adminProductWindow = new adminProductWindow();
            this.Close();
            adminProductWindow.ShowDialog();

        }
    }
}
