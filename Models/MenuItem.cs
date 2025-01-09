using System;
using System.Text.Json.Serialization;

namespace MenuOrder.Models
{
    // Базовый класс для демонстрации наследования
    [JsonDerivedType(typeof(Dish), typeDiscriminator: "dish")]
    [JsonDerivedType(typeof(Beverage), typeDiscriminator: "beverage")]
    public abstract class MenuItem
    {
        // Идентификатор элемента меню
        public int Id { get; set; }
        // Название элемента меню
        public string Name { get; set; }
        // Описание элемента меню
        public string Description { get; set; }
        // Цена элемента меню
        public decimal Price { get; set; }
        // Категория элемента меню
        public string Category { get; set; }

        // Виртуальный метод для расчета цены
        public virtual decimal CalculatePrice()
        {
            return Price;
        }
    }

    public class Dish : MenuItem
    {
        // Время приготовления блюда
        public int CookingTime { get; set; }

        // Переопределенный метод для расчета цены блюда
        public override decimal CalculatePrice()
        {
            return Price * 1.1m;
        }
    }

    public class Beverage : MenuItem
    {
        // Объем напитка
        public int Volume { get; set; }
        // Является ли напиток алкогольным
        public bool IsAlcoholic { get; set; }

        // Переопределенный метод для расчета цены напитка
        public override decimal CalculatePrice()
        {
            return IsAlcoholic ? Price * 1.2m : Price;
        }
    }
}