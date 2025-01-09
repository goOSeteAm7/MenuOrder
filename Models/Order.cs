using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MenuOrder.Models
{
    public class Order
    {
        // Список элементов заказа
        private readonly List<MenuItem> _items;
        // JSON представление элементов заказа
        private string _itemsJson;

        // Идентификатор заказа
        public int Id { get; set; }
        // Дата заказа
        public DateTime OrderDate { get; set; }
        // Статус заказа
        public string Status { get; set; } = "New";
        // Общая стоимость заказа
        public decimal TotalPrice { get; set; }

        // JSON представление элементов заказа
        public string ItemsJson
        {
            get => _itemsJson;
            set
            {
                _itemsJson = value;
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            Converters = { new JsonStringEnumConverter() }
                        };
                        var items = JsonSerializer.Deserialize<List<MenuItem>>(value, options);
                        _items.Clear();
                        if (items != null)
                        {
                            _items.AddRange(items);
                        }
                    }
                    catch (JsonException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"JSON Deserialization error: {ex.Message}");
                        _items.Clear();
                        _itemsJson = "[]";
                    }
                }
            }
        }

        // Конструктор по умолчанию
        public Order()
        {
            _items = new List<MenuItem>();
            OrderDate = DateTime.Now;
            TotalPrice = 0;
            _itemsJson = "[]";
        }

        // Метод для добавления элемента в заказ
        public void AddItem(MenuItem item)
        {
            _items.Add(item);
            CalculateTotalPrice();
            UpdateItemsJson();
        }

        // Метод для удаления элемента из заказа
        public void RemoveItem(MenuItem item)
        {
            _items.Remove(item);
            CalculateTotalPrice();
            UpdateItemsJson();
        }

        // Метод для очистки всех элементов заказа
        public void ClearItems()
        {
            _items.Clear();
            CalculateTotalPrice();
            UpdateItemsJson();
        }

        // Метод для получения списка элементов заказа
        public IReadOnlyList<MenuItem> GetItems()
        {
            return _items.AsReadOnly();
        }

        // Метод для расчета общей стоимости заказа
        private void CalculateTotalPrice()
        {
            TotalPrice = _items.Sum(item => item.CalculatePrice());
        }

        // Метод для обновления JSON представления элементов заказа
        private void UpdateItemsJson()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };
                _itemsJson = JsonSerializer.Serialize(_items, options);
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Serialization error: {ex.Message}");
                _itemsJson = "[]";
            }
        }
    }
}