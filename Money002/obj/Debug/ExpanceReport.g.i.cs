﻿#pragma checksum "..\..\ExpanceReport.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6C2F1689B4F3C9A94B958B006E76881C7CAFFF2B1AB39E0502E3F747A7FC05FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Money002;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Money002 {
    
    
    /// <summary>
    /// ExpanceReport
    /// </summary>
    public partial class ExpanceReport : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridExpenceTable;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lDate;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lProductName;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lCount;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSum;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lPrice;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxProductList;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxProductGroupList;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerFilter;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lExpancedAll;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\ExpanceReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMainWindow;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Money002;component/expancereport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ExpanceReport.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dataGridExpenceTable = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\ExpanceReport.xaml"
            this.dataGridExpenceTable.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ExpanceTableDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 35 "..\..\ExpanceReport.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Generate_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 36 "..\..\ExpanceReport.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Clear_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lDate = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lProductName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lCount = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lSum = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lPrice = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.comboBoxProductList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 65 "..\..\ExpanceReport.xaml"
            this.comboBoxProductList.Initialized += new System.EventHandler(this.LoadProductXmlToProductCombobox);
            
            #line default
            #line hidden
            return;
            case 10:
            this.comboBoxProductGroupList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.datePickerFilter = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 12:
            
            #line 68 "..\..\ExpanceReport.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Set_Filter);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 69 "..\..\ExpanceReport.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Reset_Filter);
            
            #line default
            #line hidden
            return;
            case 14:
            this.lExpancedAll = ((System.Windows.Controls.Label)(target));
            return;
            case 15:
            this.btnMainWindow = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\ExpanceReport.xaml"
            this.btnMainWindow.Click += new System.Windows.RoutedEventHandler(this.BackToMainWindow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

