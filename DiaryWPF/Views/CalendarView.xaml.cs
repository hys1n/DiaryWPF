﻿using DiaryWPF.ViewModels;
using System.Windows.Controls;

namespace DiaryWPF.Views
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
            CalendarViewModel viewModel = new CalendarViewModel();
            DataContext = viewModel;
        }
    }
}