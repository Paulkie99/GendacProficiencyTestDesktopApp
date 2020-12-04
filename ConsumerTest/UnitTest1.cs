using NUnit.Framework;
using System.Threading.Tasks;
using System;

namespace ConsumerTest
{
    public class Tests
    {
        private APIConsumer.Consumer consumer;
        private string name;

        [SetUp]
        public void Setup()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            consumer = new APIConsumer.Consumer();
            consumer.boxHandler.ShowMessages = false;
        }

        [Test, Order(0)]
        public async Task TestGETAsync() // Test that list of products can be obtained successfully
        {
            await consumer.GetAsync("");
            Assert.True(consumer.IsSuccess);
        }

        [Test, Order(1)]
        public async Task TestPOSTAsync()
        {
            name = DateTime.Now.ToString();
            APIConsumer.Product product = new APIConsumer.Product(consumer.MaxId + 1, name, APIConsumer.ProductCategory.CategoryA, 1000); // This test executes after the GET test, so POST a product with an Id greater than the max in the database
            var PostTask = consumer.PostAsync(product);
            await PostTask;
            Assert.True(consumer.IsSuccess);
        }

        [Test, Order(2)]
        public async Task TestGETIdAsync() // This test executes after the POST test, so the product list should be populated with the product added in the POST test
        {
            var GetTask = consumer.GetAsync(consumer.MaxId.ToString());
            await GetTask;
            Assert.True(consumer.IsSuccess);
            Assert.True(consumer.ProductList.Count == 1);

            APIConsumer.Product product = consumer.ProductList[0];
            Assert.True(product.Name == name &&
                        product.Category == APIConsumer.ProductCategory.CategoryA &&
                        product.Price == 1000);
        }

        [Test, Order(3)]
        public async Task TestPUTAsync() // Test that the added product can be edited
        {
            name = DateTime.Now.ToString() + "1";
            APIConsumer.Product product = new APIConsumer.Product(consumer.MaxId, name, APIConsumer.ProductCategory.CategoryB, (float)1000.01);
            var PutTask = consumer.PutAsync(product);
            await PutTask;
            Assert.True(consumer.IsSuccess);

            product = consumer.ProductList[0];
            Assert.True(product.Name == name &&
                        product.Category == APIConsumer.ProductCategory.CategoryB &&
                        product.Price == (float) 1000.01);
        }

        [Test, Order(4)]
        public async Task TestDeleteAsync()
        {
            Assert.True(consumer.ProductList.Count == 1);

            int id = consumer.MaxId;

            var DeleteTask = consumer.DeleteAsync(id.ToString());
            await DeleteTask;
            Assert.True(consumer.IsSuccess);

            var GetTask = consumer.GetAsync(id.ToString());
            await GetTask;
            Assert.False(consumer.IsSuccess);

            Assert.True(consumer.ProductList.Count == 0);
        }
    }
}