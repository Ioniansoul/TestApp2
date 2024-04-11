using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TestApp2.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }
}