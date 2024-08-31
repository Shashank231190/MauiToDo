using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiToDo.Models
{
    public class ToDoItem
    {
        //this annotation was possible because of packages we installed sqlite
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string? Title { get; set; }

        public DateTime Due { get; set; }
        public bool Done { get; set; } = false;

        
    }
}
