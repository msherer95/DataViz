using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataViz.Contracts;
using DataViz.TableImport;
using System.IO;
using DataViz.Query;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataViz
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
