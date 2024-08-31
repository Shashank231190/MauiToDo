using MauiToDo.Data;
using MauiToDo.Models;


namespace MauiToDo
{
    public partial class MainPage : ContentPage
    {
        string _todoListData = string.Empty;
        readonly DataBase _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new DataBase();

            _ = Initialize();
        }

        private async Task Initialize()
        {
            var todos = await _database.GetTodos();

            foreach(var todo in todos)
            {
                _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";
            }

            TodosLabel.Text = _todoListData;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var todo = new ToDoItem
            {
                Due = DueDatePicker.Date,
                Title = TodoTitleEntry.Text
            };

            var inserted = await _database.AddToDo(todo);
            if (inserted != 0)
            {
                _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";
                TodosLabel.Text = _todoListData;
                TodoTitleEntry.Text = String.Empty;
                DueDatePicker.Date = DateTime.Now;
            }
        }

      
    }

}
