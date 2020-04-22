using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp.Controllers
{
    public class TestulController : Controller
    {
        //private IEventRepository _eventRepository;

        //public TestulController(IEventRepository eventRepository)
        //{
        //    //_eventRepository = new MockEventRepository();
        //    _eventRepository = eventRepository;
        //}

        private DBEventRepository _eventRepository;

        public TestulController()
        {

        }
        public IActionResult Index()
        {
            var model = _eventRepository.GetEvents();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event e)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = _eventRepository.Add(e);
            }

            return View();
        }
    }
}