﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MStest.Data.Entities;
using MStest.Data.Repositories;
using MStest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MStest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatRepository chatRepository;

        public HomeController(ILogger<HomeController> logger, IChatRepository chatRepository)
        {
            _logger = logger;
            this.chatRepository = chatRepository;
        }

        public IActionResult Index()
        {
            CurrentUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Chat()
        {
            return View(await chatRepository.GetChatConnections());
        }

        [Authorize]
        public async Task<List<ChatMessage>> GetChatMessages(string partnerId)
        {
            return await chatRepository.GetChatMessages(partnerId);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            CurrentUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
