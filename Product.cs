namespace FunctionApp_HttpExample
{
    public class Product
    {
        string productId;
        string productName;
        string productDescription;

        public Product ()
        {
            productId = "";
            productName = "";
            productDescription = "";
        }

        public Product(string id, string name, string desc)
        {
            productId = id;
            productName = name;
            productDescription = desc;
        }

        public string GetProductId()
        {
            return productId;
        }

        public string GetProductName()
        {
            return productName;
        }

        public string GetProductDescription()
        {
            return productDescription;
        }

        public string SetProductId(string id)
        {
            return productId = id;
        }

        public string SetProductName(string name)
        {
            return productName = name;
        }

        public string SetProductDescription(string desc)
        {
            return productDescription = desc;
        }
    }

    
}