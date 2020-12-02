namespace APIConsumer
{
    public enum ProductCategory
    {
        CategoryA = 1,
        CategoryB,
        CategoryC,
    };

    // Serializable Product Class with relevant properties
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public float Price { get; set; }

        public Product(int Id, string Name, ProductCategory Category, float Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Category = Category;
            this.Price = Price;
        }
    }
}
