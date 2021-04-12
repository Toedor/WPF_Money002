using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Money002
{
    /// <summary>
    /// Interaction logic for ExpanceReport.xaml
    /// </summary>
    public partial class ExpanceReport : Page
    {
        List<ExpanceElement> expanceElements = new List<ExpanceElement>();

        readonly string xmlExpenseBasePath = new Uri("Resources/xmlExpenseBase.xml", UriKind.Relative).ToString();

        class ExpanceElement
        {
            public string expanseDate { get; set; }
            public string expanseTime { get; set; }
            public string expanseName { get; set; }
            public float expanseCount { get; set; }
            public string expanseCountType { get; set; }
            public float expanseSum { get; set; }
            public float expansePrice { get; set; }
        }
        public ExpanceReport()
        {
            InitializeComponent();
            LoadFromXmlToExpanceElements();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dataGridExpenceTable.Items.Clear();
        }
        private void ExpanceTableDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ExpanceElement element in expanceElements)
            {
                if (element == dataGridExpenceTable.SelectedItem)
                {
                    lProductName.Content = element.expanseName;
                    lDate.Content = element.expanseDate + " " + element.expanseTime;
                    lCount.Content = element.expanseCount + " " + element.expanseCountType;
                    lSum.Content = element.expanseSum;
                    lPrice.Content = element.expansePrice;
                }
            }
        }
        private void Set_Filter(object sender, RoutedEventArgs e)
        {
            if (datePickerFilter.SelectedDate != null && comboBoxProductList.SelectedItem != null)
            {
                SetFilterForProductName(comboBoxProductList.Text, datePickerFilter.SelectedDate.Value.ToShortDateString());
            }
            else if (datePickerFilter.SelectedDate == null && comboBoxProductList.SelectedItem != null)
            {
                SetFilterForProductName(comboBoxProductList.Text);
            }
            else if (datePickerFilter.SelectedDate != null && comboBoxProductList.SelectedItem == null)
            {
                SetFilterForProductName(datePickerFilter.SelectedDate.Value.ToShortDateString());
            }
            else if (datePickerFilter.SelectedDate == null && comboBoxProductList.SelectedItem == null)
            {
                lExpancedAll.Content = "0";
                GenerateReport();
            }
        }
        private void LoadProductXmlToProductCombobox(object sender, EventArgs e)
        {
            LoadProductXmlToProductCombobox();
        }

        void LoadFromXmlToExpanceElements()
        {
            XDocument xdoc = XDocument.Load(xmlExpenseBasePath);
            foreach (XElement expance in xdoc.Element("expances").Elements("expance"))
            {
                ExpanceElement addingExpance = new ExpanceElement();
                addingExpance.expanseDate = expance.Element("date").Value + " " + expance.Element("time").Value;
                addingExpance.expanseTime = expance.Element("time").Value;
                addingExpance.expanseName = expance.Element("product").Value;
                addingExpance.expanseCount = float.Parse(expance.Element("count").Value);
                addingExpance.expanseCountType = expance.Element("countType").Value;
                addingExpance.expanseSum = float.Parse(expance.Element("sum").Value);
                addingExpance.expansePrice = float.Parse(expance.Element("price").Value);
                expanceElements.Add(addingExpance);
            }
        }
        void GenerateReport()
        {
            lExpancedAll.Content = "0";
            dataGridExpenceTable.Items.Clear();
            foreach (ExpanceElement expanceElement in expanceElements)
            {
                dataGridExpenceTable.Items.Add(expanceElement);
                ExpancedToPeriod(expanceElement);
            }
            
        }
        void SetFilterForProductName(string productSearching)
        {
            lExpancedAll.Content = "0";
            dataGridExpenceTable.Items.Clear();
            //List<ExpanceElement> sortElements = new List<ExpanceElement>();
            for (int i = 0; i < expanceElements.Count; i++)
            {
                if (productSearching.Equals(expanceElements[i].expanseName))
                {
                    //sortElements.Add(expanceElements[i]);
                    dataGridExpenceTable.Items.Add(expanceElements[i]);
                    ExpancedToPeriod(expanceElements[i]);
                }
                if (expanceElements[i].expanseDate.Contains(productSearching))
                {
                    dataGridExpenceTable.Items.Add(expanceElements[i]);
                    ExpancedToPeriod(expanceElements[i]);
                }
            }
        }
        void SetFilterForProductName(string productName, string productDate)
        {
            lExpancedAll.Content = "0";
            dataGridExpenceTable.Items.Clear();
            //List<ExpanceElement> sortElements = new List<ExpanceElement>();
            for (int i = 0; i < expanceElements.Count; i++)
            {
                if (productName.Equals(expanceElements[i].expanseName))
                {
                    if (expanceElements[i].expanseDate.Contains(productDate))
                    {
                        //sortElements.Add(expanceElements[i]);
                        dataGridExpenceTable.Items.Add(expanceElements[i]);
                        ExpancedToPeriod(expanceElements[i]);
                    }
                }
            }
        }


        private void LoadProductXmlToProductCombobox()
        {
            MoneyHome moneyHome = new MoneyHome();
            if (File.Exists(moneyHome.xmlProductListPath))
            {
                XmlDocument xmlProductList = new XmlDocument();
                xmlProductList.Load(moneyHome.xmlProductListPath); //Загружаем xml по пути.
                XmlElement xProd = xmlProductList.DocumentElement; // Загружаем корень xml документа
                foreach (XmlElement xnode in xProd) //Начинаем перебирать xml документ
                {
                    XmlNode searchProdName = xnode.Attributes.GetNamedItem("name"); //Ищем имя продукта в xml
                    if (searchProdName != null) // Если имя не пустое, то назначаем его полю prodName
                    {
                        comboBoxProductList.Items.Add(searchProdName.Value);
                    }
                }
            }
        }

        private void Reset_Filter(object sender, RoutedEventArgs e)
        {
            comboBoxProductList.SelectedItem = null;
            datePickerFilter.SelectedDate = null;
        }

        private void BackToMainWindow(object sender, RoutedEventArgs e)
        {
            MoneyHome moneyHome = new MoneyHome();
            this.NavigationService.Navigate(moneyHome);
        }
        void ExpancedToPeriod(ExpanceElement exp)
        {
                lExpancedAll.Content = float.Parse(lExpancedAll.Content.ToString()) + exp.expanseSum;
        }
    }
}
