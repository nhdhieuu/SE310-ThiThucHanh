﻿using System;
using System.Collections.Generic;

namespace ThiThucHanh.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Role { get; set; }
}