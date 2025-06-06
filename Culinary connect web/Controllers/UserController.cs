﻿using culinaryConnect.BusinessLogic.Services;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using culinaryConnect.BusinessLogic;
using Culinary_connect_web.Filters;
using culinaryConnect.Web.Filters;

namespace Culinary_connect_web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController()
        {
            var bl = new BusinessLogic();
            _userService = bl.GetUserService();
        }
        [AuthorizeAdmin]
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
    }
}