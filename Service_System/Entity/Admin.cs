﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_System.Entity
{
    public class Admin
    {
        private string Login;
        public string GetLogin() => Login;

        public void SetLogin(string login) => this.Login = login;
    }
}
