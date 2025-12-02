using Avalonia.Controls;
using Success.Services;
using System.Collections.Generic;  
using System.Linq;               
using Success.Models;       

namespace Success;

public partial class MainWindow : Window
{
    private readonly UserService _users = new();

    private List<User> _allUsers = new();
    private int _page = 1;
    private int _pageSize = 2;

    public MainWindow()
    {
        InitializeComponent();
        _allUsers = _users.GetAllUsers();
        LoadPage();
    }

    public void ReloadUsers()
{
    _allUsers = _users.GetAllUsers();
    _page = 1;           // можно сбросить на 1-ю страницу
    LoadPage();
}

    private void LoadPage()
    {
        var items = _allUsers
            .Skip((_page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        UsersListBox.ItemsSource = items;
    }

    private void Next_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_page * _pageSize < _allUsers.Count)
        {
            _page++;
            LoadPage();
        }
    }

    private void Prev_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_page > 1)
        {
            _page--;
            LoadPage();
        }
    }

    private void CreateUser_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var createUserWindow = new CreateUserWindow(_users, this);
            createUserWindow.Show();
    }
}