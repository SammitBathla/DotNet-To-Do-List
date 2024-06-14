using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace todolist.Models
{
    public class ToDO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}