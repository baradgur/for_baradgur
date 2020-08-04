using System.Collections.Generic;
using System.Linq;

namespace task6
{
    class Product
    {
        private float _price;
        private int _count;
        public readonly string name;
        private SortedSet<float> _discounts = new SortedSet<float>();
        public Product(string name, float price)
        {
            this.name = name;
            if (price < 0)
                price = 0;
            _price = price;
            _count = 1;
        }

        public float GetPrice()
        {
            return _price;
        }

        private float GetPrice(float discount)
        {
            return _price - _price * discount / 100;
        }

        public void SetPrice(float value)
        {
            _price = value;
        }

        public void IncreaseCount()
        {
            _count++;
        }

        public void DecreaseCount()
        {
            _count--;
            if (_count <= 0)
                _count = 0;
        }

        public int GetCount()
        {
            return _count;
        }

        public void CreateDiscount(float value)
        {
            _discounts.Add(value);
        }

        public float GetDiscountPrice(int index)
        {
            return GetPrice( _discounts.ElementAt(index));
        }

        public bool CheckDiscountIndex(int index)
        {
            return index < _discounts.Count && index >= 0;
        }

        public override string ToString()
        {
            var prices = "" + _price;
            foreach (var discount in _discounts)
            {
                prices += " " + GetPrice(discount);
            }
            return name + "\nprices " + prices + "\ncount " + _count;
        }
    }
}
