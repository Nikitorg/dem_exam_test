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
    /// Логика взаимодействия для addProductWindow.xaml
    /// </summary>
    public partial class addProductWindow : Window
    {
        public List<TextBox> текстовыеПоля;
        public List<ComboBox> выпадющиеСписки;
        public addProductWindow()
        {
            InitializeComponent();
            loadCmbBox();
            выпадющиеСписки = new List<ComboBox>();
            текстовыеПоля = new List<TextBox>();
            loadTxtBoxComboBoxForList();

            //ФИО СПРАВА В УГЛУ
            userTxt.Text = CurrentUser.пользователь.ФИО;
            
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

        //МЕТОД НАЖАТИЯ КНОПКИ "НАЗАД"
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

                if (Convert.ToDecimal(priceProductTxtBox.Text) <0)
                {
                    MessageBox.Show("Цена не может быть отрицательной" , "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                //ДОБАВЛЕНИЕ И СОХРАНЕНИЕ ТОВАРОВ
                Товар товар = new Товар
                {
                    ID = (Context.dbContext.Товар.Select(a => a.ID).Max()) + 1,
                    Артикул = articleProductTxtBox.Text,
                    НаименованиеТовара = nameProdctTxtBox.Text,
                    Поставщик = postavProductTxtBox.Text,
                    Производитель = proizCmbBox.Text,
                    КатегорияТовара = catCmbBox.Text,
                    ЕдиницаИзмерения = edProductTxtBox.Text,
                    Цена = Convert.ToDouble(priceProductTxtBox.Text),
                    ДействующаяСкидка = Convert.ToInt32(actionProductTxtBox.Text),
                    КоличествоНаСкладе = Convert.ToInt32(countProductTxtBox.Text),
                    ОписаниеТовара = infoProductTxtBox.Text,
                    Фото = "picture.png" //ФОТО СДЕЛАЛ ЗАГЛУШКОЙ, т.к ПОЛЮБОМУ НИКТО НЕ БУДЕТ УЧИТЬ, КАК ДОБАВЛЯТЬ ФОТО, ПОХУЙ, СДЕЛАЕМ ТАК
                };
                Context.dbContext.Товар.Add(товар);
                Context.dbContext.SaveChanges();

                MessageBox.Show("Товар успешно сохранен!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                adminProductWindow adminProductWindow = new adminProductWindow();
                this.Close();
                adminProductWindow.ShowDialog();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
