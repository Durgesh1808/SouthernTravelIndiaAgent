﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.DTO
{
    /// <summary>
    /// /// This class represents the result of an agent's forgot password operation.
    /// </summary>
    public class Agent_ForgotPasswordResult
    {
        public string UserId { get; set; }

        public string Password { get; set; }

        public string Fname { get; set; }
    }
}