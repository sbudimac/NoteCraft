﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftRazorComponents
{
    public partial class UserComponent
    {
        public bool SignIn { get; set; } = true;
        public bool SignUp { get; set; } = false;

        public void SwitchToLogin()
        {
            SignIn = true;
            SignUp = false;
        }

        public void SwitchToRegister()
        {
            SignIn = false;
            SignUp = true;
        }
    }
}
