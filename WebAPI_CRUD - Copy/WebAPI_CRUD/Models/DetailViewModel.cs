using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_CRUD.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }

    }
}