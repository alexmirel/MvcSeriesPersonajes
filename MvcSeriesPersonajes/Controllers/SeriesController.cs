using Microsoft.AspNetCore.Mvc;
using MvcSeriesPersonajes.Models;
using MvcSeriesPersonajes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSeriesPersonajes.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceApiSeries service;

        public SeriesController(ServiceApiSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();

            return View(series);
        }
    }
}
