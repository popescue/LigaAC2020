﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TimeController : Controller
    {
        public IActionResult Index()
        {
            Time t = new Time { time = DateTime.Now };
            /*Metoda1. ViewBag
            ViewBag.Message = t;*/
            /*Metoda2. ViewData
            ViewData["Message"] = t;*/

            //Metoda3. Model
            return View(t);
        }
    }
}