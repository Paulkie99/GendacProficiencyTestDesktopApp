using System;
using System.Collections.Generic;
using System.Text;

namespace APIConsumer
{
    // Class used to determine whether product properties are valid
    public class InputValidator
    {
        Consumer parent; // Reference to Consumer class required to access dictionaries, in order to determine whether an Id or Name already exists
        public InputValidator(Consumer parent)
        {
            this.parent = parent;
        }

        // Check if given Id is valid
        public bool IsValidId(int id)
        {
            if (id < 0) // Open issue: is Id = 0 valid?
                return false;

            return !IsIdExists(id); // Check if Id exists, if true, the Id is invalid
        }

        // Check if given Id exists in Consumer dictionary
        public bool IsIdExists(int id)
        {
            Product product;
            parent.ProductIdDict.TryGetValue(id, out product);
            if (product != null)
                return true;

            return false;
        }

        // Check if given name is valid
        // Open issue: should name be allowed to be non-unique when editing?
        public bool IsValidName(string name)
        {
            // Name cannot be blank
            if (name.Length == 0)
                return false;

            // Check if Name exists, if true, Name is invalid
            return !IsNameExists(name);
        }

        // Check if given Name exists in Consumer dictionary
        private bool IsNameExists(string name)
        {
            Product product;
            parent.ProductNameDict.TryGetValue(name, out product);
            if (product != null)
                return true;

            return false;
        }

        // Check if category is within ProductCategory enum
        public bool IsValidCategory(int category)
        {
            if(category < (int) ProductCategory.CategoryA || category > (int) ProductCategory.CategoryC)
            {
                return false;
            }

            return true;
        }

        // Check if price is not less than or equal to zero
        public bool IsValidPrice(float price)
        {
            if (price <= 0)
                return false;

            return true;
        }
    }
}
