﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.API.Authorization
{
    public class MustOwnImageRequriement:IAuthorizationRequirement
    {
        public MustOwnImageRequriement()
        {

        }
    }
}
