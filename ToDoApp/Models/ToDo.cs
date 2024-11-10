using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

    
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public string UserId { get; set; }


        public virtual User User { get; set; }
    }
}