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
    /// Логика взаимодействия для productManagerWindow.xaml
    /// </summary>
    public partial class productManagerWindow : Window
    {
        public productManagerWindow()
        {
            InitializeComponent();
            loadDG();
            loadCmBox();

            //ФИО СПРАВА В УГЛУ
            userTxt.Text = CurrentUser.пользователь.ФИО;
        }

        //загрузить список товаров
        public void loadDG()
        {
            dgProduct.ItemsSource = Context.dbContext.Товар.ToList();
        }
        //загрузить список поставщиков в комбобокс
        public void loadCmBox()
        {
            //грузим список поставщиков
            var listPostav = Context.dbContext.Товар.Select(a => a.Поставщик).Distinct().ToList();
            
            //добавляем к этому списку "Все поставщики", (0 - это порядковый номер, типо 0,1,2...)
            listPostav.Insert(0, "Все поставщики");

            //в комбобокс грузим список
            cmbPostav.ItemsSource = listPostav;
        }

        //МЕТОД НАЖАТИЯ КНОПКИ "НАЗАД"
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MenuWindow menuWindow = new MenuWindow();
            this.Close();
            menuWindow.ShowDialog();
        }

        //МЕТОД ВСЕХ ФИЛЬТРОВ ТОВАРОВ (КОД НЕПОНЯТНЫЙ (тут все через массив строки и т.д (дрочь кароче)), ТЯЖЕЛО ОБЪЯСНИТЬ, ТУПО ЗАПОНИТЕ)
        public void ApplyFilters()
        {
            //ПОДГРУЖАЕМ ДАННЫЕ (ПОКА ТОЛЬКО В ЗАПРОС, НЕ ИЗ БД)
            var query = Context.dbContext.Товар.AsQueryable();

            //ПОИСК(разбиваем в массив по словам (типо "Кросовки" "Rieker, "Женская" и т.д))
            var words = (selectProductTxtBox.Text ?? "")
                .ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //ПРОВЕКА, ЕСТЬ ЛИ В ПОИСКЕ ХОТЬ ЧЕ ТО
            if (words.Length > 0)
            {
                //В ЗАПРОС ДОБАВЛЯЕМ ПОИСК ПО ВСЕМ ПАРАМЕТРАМ (тут в коде не все параметры, можно добавить самостоятельно, мне лень)
                query = query.Where(a =>
                    words.All(z =>
                        a.НаименованиеТовара.ToLower().Contains(z) ||
                        a.КатегорияТовара.ToLower().Contains(z) ||
                        a.ОписаниеТовара.ToLower().Contains(z) ||
                        a.Производитель.ToLower().Contains(z) ||
                        a.Поставщик.ToLower().Contains(z)
                    )
                );
            }



            //ФИЛЬТРУЕМ ПО ВОЗРАСТАНИЮ И УБЫВАНИЮ

            //ПО ВОЗРАСТАНИЮ
            if (rbUp.IsChecked == true)
                query = query.OrderBy(a => a.КоличествоНаСкладе);

            //ПО УБЫВАНИЮ
            if (rbDown.IsChecked == true)
                query = query.OrderByDescending(a => a.КоличествоНаСкладе);



            //СОРТИРУЕМ ПО ПОСТАВЩИКАМ

            var selectedPostav = cmbPostav.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedPostav) && selectedPostav != "Все поставщики")
            {
                query = query.Where(a => a.Поставщик == selectedPostav);
            }

            //ТОЛЬКО ПОТОМ ГРУЗИМ В СПИСОК ToLIST() И ВЫВОДИМ.
            dgProduct.ItemsSource = query.ToList();
        }
        private void selectProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void checkCount_Rbtn(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }


        private void cmbPostav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
    }

}
