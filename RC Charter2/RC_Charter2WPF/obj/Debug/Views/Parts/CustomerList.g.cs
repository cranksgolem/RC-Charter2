﻿#pragma checksum "..\..\..\..\Views\Parts\CustomerList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C0EEB9019771997AFE72C7003AEAA1C1534C2059"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RC_Charter2WPF.Views.Parts;
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


namespace RC_Charter2WPF.Views.Parts {
    
    
    /// <summary>
    /// CustomerList
    /// </summary>
    public partial class CustomerList : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\Views\Parts\CustomerList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxCustomerSearch;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Views\Parts\CustomerList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TblSearchLabel;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\Parts\CustomerList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddCustomer;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Views\Parts\CustomerList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnEditCustomer;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Views\Parts\CustomerList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDeleteCustomer;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\Parts\CustomerList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox LbxCustomers;
        
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
            System.Uri resourceLocater = new System.Uri("/RC_Charter2WPF;component/views/parts/customerlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Parts\CustomerList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.TbxCustomerSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\..\..\Views\Parts\CustomerList.xaml"
            this.TbxCustomerSearch.GotFocus += new System.Windows.RoutedEventHandler(this.TbxCustomerSearch_GotFocus);
            
            #line default
            #line hidden
            
            #line 31 "..\..\..\..\Views\Parts\CustomerList.xaml"
            this.TbxCustomerSearch.LostFocus += new System.Windows.RoutedEventHandler(this.TbxCustomerSearch_LostFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TblSearchLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.BtnAddCustomer = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.BtnEditCustomer = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.BtnDeleteCustomer = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.LbxCustomers = ((System.Windows.Controls.ListBox)(target));
            
            #line 95 "..\..\..\..\Views\Parts\CustomerList.xaml"
            this.LbxCustomers.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LbxCustomers_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

