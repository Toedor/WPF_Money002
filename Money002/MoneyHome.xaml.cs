using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Xml;
using System.Xml.Linq;

namespace Money002
{
    /// <summary>
    /// Interaction logic for MoneyHome.xaml
    /// </summary>
    public partial class MoneyHome : Page
    {
        List<Product> productList = new List<Product>(); // иниициализация списка продуктов, куда будет загружен xml
        Product selectedProduct = new Product(); // Инициализация текущего продукта(который выбран в combobox в данный момент)
        public readonly string xmlProductListPath = new Uri("Resources/xmlProductList.xml", UriKind.Relative).ToString();
        readonly string xmlExpenceBasePath = new Uri("Resources/xmlExpenseBase.xml", UriKind.Relative).ToString();
        readonly string xmlProductGroupListPath = new Uri("Resources/xmlProductGroupList.xml", UriKind.Relative).ToString();

        public class Product //Класс с информацией о конкретном продукте
        {
            public string prodName { get; set; }
            public string prodCountType { get; set; }
            public string prodMass { get; set; }
            public string prodPrice { get; set; }
            public string prodSum { get; set; }
            public string prodGroup { get; set; }
        }

        public MoneyHome()
        {
            InitializeComponent();
            CreateXmlFiles();
            datePickerExpence.SelectedDate = DateTime.Now;
            datePickerIncome.SelectedDate = DateTime.Now;
        }

        private void ClkAddExpense(object sender, RoutedEventArgs e) //Добавляет расход
        {
            if (float.TryParse(priceFieldOutput.Text, out float price))
            {
                AddProdToProductList();
                AddExpanseToXml();
            }
            else
            {
                MessageBox.Show("Вы не указали, или неверно указали одно или несколько полей", "Предупреждение...", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClkAddIncome(object sender, RoutedEventArgs e) //Метод добавления дохода
        {
            //Тут будет какой-то говнокод
        }

        private void ProductComboBox_Initialized(object sender, EventArgs e) //Когда инициализируется combobox со списком товаров
        {
            LoadProductXmlToProductCombobox(xmlProductListPath);
        }

        private void DelProdFromProductList(object sender, RoutedEventArgs e) //нажатие кнопки удаления продукта
        {
            if (comboBoxProductList.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Действительно удалить {comboBoxProductList.Text}?", "Удаление...", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        DelProdFromProductList(comboBoxProductList, sender as Button);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else MessageBox.Show($"Вы ничего не выбрали", "Удаление...", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e) //Деиствия при изменении текста в combobox со списком продуктов
        {
            if (comboBoxProductList.SelectedItem != null) //Если поле combobox не пустное
            {
                if ((productList.Find(itemFind => itemFind.prodName == comboBoxProductList.SelectedItem.ToString())) != null) //выполняем проверку при поиске в списке
                {
                    selectedProduct = productList.Find(Product => Product.prodName == comboBoxProductList.SelectedItem.ToString()); //заполняем поля текущего выбранного продукта
                    comboBoxProductCountTypeInput.Text = selectedProduct.prodCountType;
                    textBoxProductCountInput.Text = selectedProduct.prodMass;
                    comboBoxProductList.Text = selectedProduct.prodName;
                    textBoxProductSumInput.Text = selectedProduct.prodSum;
                    comboBoxProductGroup.Text = selectedProduct.prodGroup;

                }
            }
            //else Console.WriteLine($"Получено исключение: productComboBox.SelectedItem вернул null, т.е. ничего не выбрано"); //Если поле combobox пустное
        }

        private void viewExpense_Click(object sender, RoutedEventArgs e) //Переход в окно просмотра расходов
        {
            ExpanceReport expanceReport = new ExpanceReport();
            this.NavigationService.Navigate(expanceReport);
        }

        private void viewIncome_Click(object sender, RoutedEventArgs e)
        {
            MoneyReport moneyReport = new MoneyReport();
            this.NavigationService.Navigate(moneyReport);
        }

        private void TotalSum_TextChanged(object sender, TextChangedEventArgs e)
        {
            sumFieldOutput.Text = textBoxProductSumInput.Text;
            CalculatePrice();
        }

        private void productMass_TextChanged(object sender, TextChangedEventArgs e)
        {
            countFieldOutput.Text = textBoxProductCountInput.Text;
            CalculatePrice();
        }

        private void CalculatePrice()
        {
            if (float.TryParse(countFieldOutput.Text, out float countf) && float.TryParse(sumFieldOutput.Text, out float sumf))
            {
                if (countf > 0 && sumf > 0)
                {
                    float calcPrice = sumf / countf;
                    priceFieldOutput.Text = Math.Round(calcPrice, 2).ToString();
                }
                else
                {
                    priceFieldOutput.Text = "###";
                }
            }
            else
            {
                priceFieldOutput.Text = "###";
            }
        }

        private void CountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBoxProductCountTypeInput.SelectedIndex)
            {
                case 0:
                    textBlockCountType.Text = "л";
                    break;
                case 1:
                    textBlockCountType.Text = "кг";
                    break;
                case 2:
                    textBlockCountType.Text = "шт";
                    break;
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void LoadProductXmlToProductCombobox(String productListPath) //Подгружаем xml в combobox и в список продуктов(ProductList).
        {
            if (File.Exists(productListPath))
            {
                XmlDocument xmlProductList = new XmlDocument();
                xmlProductList.Load(productListPath); //Загружаем xml по пути.
                XmlElement xProd = xmlProductList.DocumentElement; // Загружаем корень xml документа
                foreach (XmlElement xnode in xProd) //Начинаем перебирать xml документ
                {
                    Product addingProduct = new Product(); //Инициализируем новый экземпляр конкретного продукта перед его заполнением и добавлением в список продуктов
                    XmlNode searchProdName = xnode.Attributes.GetNamedItem("name"); //Ищем имя продукта в xml
                    if (searchProdName != null) // Если имя не пустое, то назначаем его полю prodName
                    {
                        addingProduct.prodName = searchProdName.Value;
                    }
                    foreach (XmlNode searchProdItems in xnode.ChildNodes) //Теперь пробегаемся по вложенным Items найденого продукта 
                    {
                        if (searchProdItems.Name == "countType") //единица измерения
                        {
                            addingProduct.prodCountType = searchProdItems.InnerText;
                        }
                        if (searchProdItems.Name == "mass") // Количество
                        {
                            addingProduct.prodMass = searchProdItems.InnerText;
                        }
                        if (searchProdItems.Name == "price") //цена
                        {
                            addingProduct.prodPrice = searchProdItems.InnerText;
                        }
                        if (searchProdItems.Name == "sum") //сумма
                        {
                            addingProduct.prodSum = searchProdItems.InnerText;
                        }
                        if(searchProdItems.Name == "group") //группа товаров
                        {
                            addingProduct.prodGroup = searchProdItems.InnerText;
                        }
                    }
                    productList.Add(addingProduct); //Добалем найденый и заполненый продукт в список productList
                    comboBoxProductList.Items.Add(addingProduct.prodName); //Добавлем имя продукта в combobox
                    Console.WriteLine(addingProduct.prodGroup);
                    
                }

            }
        }
        void SaveNewProductToXml(Product newProduct) //Метод сохранени нового продукта в xml файле
        {
            XmlDocument xmlProductList = new XmlDocument(); //Создали новый экземпляр xml документа
            xmlProductList.Load(xmlProductListPath); //Загрузили xml документ
            XmlElement xmlProdListElement = xmlProductList.DocumentElement; //Получил корень документа

            XmlElement productElement = xmlProductList.CreateElement("product"); //В корне создал элемент product
            XmlAttribute productNameAttribute = xmlProductList.CreateAttribute("name"); //Для элемента product создал атрибут name
            XmlElement productCountTypeElement = xmlProductList.CreateElement("countType"); //В элементе product создал элемент countType
            XmlElement productMassElement = xmlProductList.CreateElement("mass"); //В элементе product создал элемент mass
            XmlElement productPriceElement = xmlProductList.CreateElement("price"); //В элементе product создал элемент price
            XmlElement productSumElement = xmlProductList.CreateElement("sum"); //В элементе product создал элемент sum

            XmlText productNameText = xmlProductList.CreateTextNode(newProduct.prodName); //Создал текст для помещения в элемент
            XmlText productCountTypeText = xmlProductList.CreateTextNode(newProduct.prodCountType); //Создал текст для помещения в элемент
            XmlText productMassText = xmlProductList.CreateTextNode(newProduct.prodMass); //Создал текст для помещения в элемент
            XmlText productPriceText = xmlProductList.CreateTextNode(newProduct.prodPrice); //Создал текст для помещения в элемент
            XmlText productSumText = xmlProductList.CreateTextNode(newProduct.prodSum); //Создал текст для помещения в элемент

            productNameAttribute.AppendChild(productNameText); //Поместил текст в атрибут элемента
            productCountTypeElement.AppendChild(productCountTypeText); //Поместил текст в элемент
            productMassElement.AppendChild(productMassText); //Поместил текст в элемент
            productPriceElement.AppendChild(productPriceText); //Поместил текст в элемент
            productSumElement.AppendChild(productSumText); //Поместил текст в элемент

            productElement.Attributes.Append(productNameAttribute); //Добавил атрибут name элементу product
            productElement.AppendChild(productCountTypeElement); //Добавил элемент countType в элемент product
            productElement.AppendChild(productMassElement); //Добавил элемент mass в элемент product
            productElement.AppendChild(productPriceElement);
            productElement.AppendChild(productSumElement);

            xmlProdListElement.AppendChild(productElement); //Поместил всё в xml
            xmlProductList.Save(xmlProductListPath); //Save xml to local folder
        }
        void DelProdFromProductList(ComboBox comboBox, Button btnClicked) //Метод удаления из productList и xmlProductList
        {
            XDocument xmlProductList = XDocument.Load(xmlProductListPath);
            if (btnClicked.Name.Equals("btnDeleteProduct"))
            {
                if (comboBox.SelectedItem != null) //Если поле combobox не пустное
                {
                    if ((productList.Find(itemFind => itemFind.prodName == comboBox.SelectedItem.ToString())) != null)
                    {
                        productList.Remove(productList.Find(Product => Product.prodName == comboBox.Text));
                    }
                }

                foreach (XElement xNode in xmlProductList.Root.Nodes())
                {
                    if (xNode.Attribute("name").Value == comboBox.Text)
                    {
                        xNode.Remove();
                        xmlProductList.Save(xmlProductListPath);
                        comboBoxProductList.Items.Remove(comboBox.SelectedValue);
                    }
                }
            }
            else if (btnClicked.Name.Equals("btnDeleteProductGroup")) //Удаляем группу
            {
                Console.WriteLine($"УДАЛЕНИЕ ГРУППЫ");
            }
        }
        void AddProdToProductList() //Проверка списка продуктов на наличие дублтикатов
        {
            for (int i = 0; i < productList.Count; i++)
            {
                if (comboBoxProductList.Text.Equals(productList[i].prodName))
                {
                    break;
                }
                else if (i + 1 == productList.Count)
                {
                    AddToProdList();
                }
            }
            if (productList.Count == 0) AddToProdList();
        }
        void AddExpanseToXml() //Добавление расходов в Xml БД расходов
        {
            XDocument xmlExpanseBase = XDocument.Load(xmlExpenceBasePath);
            XElement expances = xmlExpanseBase.Element("expances");

            XElement expance = new XElement("expance");
            XElement expanceDate = new XElement("date", datePickerExpence.SelectedDate.Value.Date.ToShortDateString());
            XElement expanceTime = new XElement("time", DateTime.Now.ToLongTimeString());
            XElement expanceProduct = new XElement("product", comboBoxProductList.Text);
            XElement expanceCount = new XElement("count", textBoxProductCountInput.Text);
            XElement expanceCountType = new XElement("countType", textBlockCountType.Text);
            XElement expanceSum = new XElement("sum", textBoxProductSumInput.Text);
            XElement expancePrice = new XElement("price", priceFieldOutput.Text);

            expance.Add(expanceDate);
            expance.Add(expanceTime);
            expance.Add(expanceProduct);
            expance.Add(expanceCount);
            expance.Add(expanceCountType);
            expance.Add(expanceSum);
            expance.Add(expancePrice);
            expances.Add(expance);
            xmlExpanseBase.Save(xmlExpenceBasePath);

            ResetAllFields();
        }
        void CreateXmlFiles() //Проверка на наличие Xml файлов. И если файлов нет, то создаём их.
        {
            if (!File.Exists(xmlExpenceBasePath))
            {
                XDocument xmlExpanseBase = new XDocument();
                XElement expances = new XElement("expances");
                xmlExpanseBase.Add(expances);
                xmlExpanseBase.Save(xmlExpenceBasePath);
            }
            if (!File.Exists(xmlProductListPath))
            {
                XDocument xmlProductList = new XDocument();
                XElement products = new XElement("products");
                xmlProductList.Add(products);
                xmlProductList.Save(xmlProductListPath);
            }
            if (!File.Exists(xmlProductGroupListPath))
            {
                XDocument xmlProductGroupList = new XDocument();
                XElement groups = new XElement("groups");
                xmlProductGroupList.Add(groups);
                xmlProductGroupList.Save(xmlProductGroupListPath);
            }
        }
        void AddToProdList() //Добавление нового продукта в список продуктов.
        {
            if (comboBoxProductList.Text.Trim().Length > 0)
            {
                MessageBoxResult result = MessageBox.Show($"{comboBoxProductList.Text}\nТакого продукта ещё нет, Добавить в список?", "Добаление...", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            comboBoxProductList.Items.Add(comboBoxProductList.Text);
                            Product newProduct = new Product();
                            newProduct.prodName = comboBoxProductList.Text;
                            newProduct.prodMass = textBoxProductCountInput.Text;
                            newProduct.prodCountType = comboBoxProductCountTypeInput.Text;
                            newProduct.prodPrice = priceFieldOutput.Text;
                            newProduct.prodSum = textBoxProductSumInput.Text;
                            productList.Add(newProduct);
                            SaveNewProductToXml(newProduct);
                            MessageBox.Show($"Продукт {comboBoxProductList.Text} добавлен!", "Добаление...", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show($"Вы ничего не выбрали,\nлибо написали одни пробелы", "Добаление...", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }
        void ResetAllFields() //обнуляет все поля
        {
            comboBoxProductCountTypeInput.Text = null;
            comboBoxProductList.Text = null;
            countFieldOutput.Text = null;
            textBoxProductSumInput.Text = null;
            sumFieldOutput.Text = null;
            priceFieldOutput.Text = null;
            textBlockCountType.Text = null;
            textBoxProductCountInput.Text = null;
            comboBoxProductGroup.Text = null;
        }

        private void SelectAllText(object sender, RoutedEventArgs e) //Выделение всего текста в в поле TextBox при переходе в него курсора.
        {
            TextBox text = sender as TextBox;
            if (text != null) text.SelectAll();
        }

        private void DelGroupFromProductGroupList(object sender, RoutedEventArgs e)
        {
        }

        private void ProductGroupComboBox_Initialized(object sender, EventArgs e)
        {
            XDocument xmlProductGroup = XDocument.Load(xmlProductGroupListPath);
            foreach(XElement xNode in xmlProductGroup.Root.Nodes())
            {
                comboBoxProductGroup.Items.Add(xNode.Value);
            }
        }
    }
}

