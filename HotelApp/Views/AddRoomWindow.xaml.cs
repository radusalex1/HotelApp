﻿using System.Windows;

namespace HotelApp.Views
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public AddRoomWindow()
        {
            InitializeComponent();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            UpdateRoomsPage updateRoomsPage = new UpdateRoomsPage();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = updateRoomsPage;
            App.Current.MainWindow.Show();
        }
    }
}
