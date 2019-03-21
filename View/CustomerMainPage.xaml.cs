﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zeus;

namespace Dohko
{
    /// <summary>
    /// Lógica de interacción para CustomerMainPage.xaml
    /// </summary>
    public partial class CustomerMainPage : Page
    {
        public CustomerMainPage()
        {
            DataContext = MainWindowViewModel.GetInstance(null, null);

            InitializeComponent();
            CustomersSearchTextBox.Focus();
        }

        private void KeyUpNoSymbolsEvent(object sender, KeyEventArgs e)
        {
            ((TextBox)sender).Text = Formatter.RemoveInvalidCharacters(((TextBox)sender).Text, out bool status);
            if (status)
            {
                MainWindowViewModel.GetInstance(null, null).Code = "Símbolo inválido!";
            }
        }

        private void KeyUpNoSymbolsNoSpaceEvent(object sender, KeyEventArgs e)
        {
            ((TextBox)sender).Text = Formatter.RemoveInvalidCharacters(((TextBox)sender).Text, out var status);
            if (status)
            {
                MainWindowViewModel.GetInstance(null, null).Code = "Símbolo inválido!";
            }
            ((TextBox)sender).Text = Formatter.RemoveWhiteSpace(((TextBox)sender).Text, out status);
            if (status)
            {
                MainWindowViewModel.GetInstance(null, null).Code = "Espacio inválido!";
            }
        }

        private void VendorData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}