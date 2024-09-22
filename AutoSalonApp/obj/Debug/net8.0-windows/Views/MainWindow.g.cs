﻿#pragma checksum "..\..\..\..\Views\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B847AA268CE720C88D21954D2EF2863DE36BF2DF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoSalonApp;
using AutoSalonApp.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace AutoSalonApp.Views {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid CarsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid OrdersDataGrid;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlaceOrderButton;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditOrderButton;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchAllButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddCarButton;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditCarButton;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchCarTextBox;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchAllCarsButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AutoSalonApp;component/views/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Views\MainWindow.xaml"
            ((AutoSalonApp.Views.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 30 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateDatabaseButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 31 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteDatabaseButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 32 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveToJSONButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 33 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenSalesChartWindowButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CarsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.OrdersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            this.PlaceOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\..\Views\MainWindow.xaml"
            this.PlaceOrderButton.Click += new System.Windows.RoutedEventHandler(this.PlaceOrderButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.EditOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\Views\MainWindow.xaml"
            this.EditOrderButton.Click += new System.Windows.RoutedEventHandler(this.EditOrderButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.SearchAllButton = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\..\Views\MainWindow.xaml"
            this.SearchAllButton.Click += new System.Windows.RoutedEventHandler(this.SearchAllButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 91 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.AddCarButton = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\..\Views\MainWindow.xaml"
            this.AddCarButton.Click += new System.Windows.RoutedEventHandler(this.AddCarButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.EditCarButton = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\..\Views\MainWindow.xaml"
            this.EditCarButton.Click += new System.Windows.RoutedEventHandler(this.EditCarButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.SearchCarTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.SearchAllCarsButton = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\Views\MainWindow.xaml"
            this.SearchAllCarsButton.Click += new System.Windows.RoutedEventHandler(this.SearchAllCarsButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 102 "..\..\..\..\Views\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchCarButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

