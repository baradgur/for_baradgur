using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Response_Handler : Handler
    {
        List<Product> products = new List<Product>();
        private const string without_delivery = "\nWithout delivery.";
        private const string with_delivery = "\nWith delivery.";

        enum DeliveryOptions
        {
            delivery,
            pickup
        }

        public string Process_response(Response response)
        {
            switch (response.GetCommand())
            {
                case (int) Commands.exit:
                    return "exit";

                case (int) EXCEPTION.WRONG_COMMAND:
                    return "Wrong command";

                case (int) EXCEPTION.WRONG_ARGUMENT:
                    return "Wrong argument";

                case (int) Commands.create:
                    float price = 0f;
                    string name = "";
                    if (!response.TryFloatFromData(1, ref price) || !response.TryStringFromData(0, ref name))
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    var product = products.Find(p => p.name == name);
                    if (product == null)
                    {
                        product = new Product(name, price);
                        products.Add(product);
                    }
                    else
                    {
                        product.IncreaseCount();
                    }
                    return "Product created " + product;

                case (int) Commands.showcase:
                    return ShowAllProducts();

                case (int)Commands.setprice:
                    price = 0f;
                    name = "";
                    if (!response.TryFloatFromData(1, ref price) || !response.TryStringFromData(0, ref name))
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    product = products.Find(p => p.name == name);
                    if (product == null)
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    product.SetPrice(price);
                    return "new price for " + product;

                case (int)Commands.buy:
                    var price_index = 0;
                    name = "";
                    if (!response.TryIntFromData(1, ref price_index) || !response.TryStringFromData(0, ref name))
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    product = products.Find(p => p.name == name);
                    if (product == null || product.GetCount() <= 0 || price_index > 0 && !product.CheckDiscountIndex(price_index - 1))
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    product.DecreaseCount();
                    var delivery = without_delivery;
                    if (price_index > 0)
                    {
                        price = product.GetDiscountPrice(price_index - 1);
                    }
                    else
                    {
                        price = product.GetPrice();
                        var temp = "";
                        response.TryStringFromData(2, ref temp);
                        delivery = temp == Enum.GetName(typeof(DeliveryOptions), DeliveryOptions.delivery)
                            ? with_delivery
                            : without_delivery;
                    }

                    return "Sold " + product.name + " for " + price + delivery;

                case (int)Commands.discount:
                    var discount = 0f;
                    name = "";
                    if (!response.TryFloatFromData(1, ref discount) || !response.TryStringFromData(0, ref name))
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    product = products.Find(p => p.name == name);
                    if (product == null)
                        goto case (int)EXCEPTION.WRONG_ARGUMENT;
                    product.CreateDiscount(discount);
                    return product.ToString();

                case (int)Commands.help:
                    return GetHelp();
            }

            return "";
        }

        private string GetHelp()
        {
            return "\n-create [string name] [float price] - create product" +
                   "\n-showcase - show all products" +
                   "\n-setprice [string name] [float new_price] - set product price" +
                   "\n-buy [string name] [price index] [delivery options] - buy product" + 
                   "\n-discount [string name] [float value] - set product discount" +
                   "\n-exit";
        }

        private string ShowAllProducts()
        {
            string result = "All products:";
            foreach (var product in products)
            {
                if (product.GetCount() <= 0)
                    continue;
                result += "\n" + product;
            }

            return result;
        }
    }
}

