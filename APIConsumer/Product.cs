using System;
using System.Collections.Generic;
using System.Text;

namespace APIConsumer
{
    public enum ProductCategory
    {
        CategoryA,
        CategoryB,
        CategoryC
    };

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public float Price { get; set; }
    }
}
