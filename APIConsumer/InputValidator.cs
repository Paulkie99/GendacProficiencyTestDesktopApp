using System;
using System.Collections.Generic;
using System.Text;

namespace APIConsumer
{
    public class InputValidator
    {
        Consumer parent;
        public InputValidator(Consumer parent)
        {
            this.parent = parent;
        }

        public bool IsValidId(int id)
        {
            if (id < 0)
                return false;

            return !IsIdExists(id);
        }

        public bool IsIdExists(int id)
        {
            Product product;
            parent.ProductIdDict.TryGetValue(id, out product);
            if (product != null)
                return true;

            return false;
        }
        
        public bool IsValidName(string name)
        {
            if (name.Length == 0)
                return false;

            Product product;
            parent.ProductNameDict.TryGetValue(name, out product);
            if (product != null)
                return false;

            return true;
        }

        public bool IsValidCategory(int category)
        {
            if(category < (int) ProductCategory.CategoryA || category > (int) ProductCategory.CategoryC)
            {
                return false;
            }

            return true;
        }

        public bool IsValidPrice(float price)
        {
            if (price <= 0)
                return false;

            return true;
        }
    }
}
