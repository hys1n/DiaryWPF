﻿using DiaryWPF.ViewModels;
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

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for AddTaskForm.xaml
    /// </summary>
    public partial class AddTaskForm : Window
    {
        public AddTaskForm()
        {
            InitializeComponent();
            AddTaskFormViewModel viewModel = new AddTaskFormViewModel();
            DataContext = viewModel;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }
        }
    }
}
