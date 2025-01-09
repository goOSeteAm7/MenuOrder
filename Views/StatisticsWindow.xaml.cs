using System.Linq;
using System.Windows;
using MenuOrder.Data;

namespace MenuOrder.Views
{
    public partial class StatisticsWindow : Window
    {
        // Контекст базы данных
        private readonly ApplicationDbContext _context;

        public StatisticsWindow(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            // Получаем все заказы из базы данных
            var orders = _context.Orders.ToList();
            
            // Общее количество заказов
            var totalOrders = orders.Count;
            TotalOrdersTextBlock.Text = totalOrders.ToString();

            // Общая выручка
            var totalRevenue = orders.Sum(o => o.TotalPrice);
            TotalRevenueTextBlock.Text = $"{totalRevenue:N2} ₽";

            // Средний чек
            var averageOrder = orders.Any() ? totalRevenue / orders.Count : 0;
            AverageOrderTextBlock.Text = $"{averageOrder:N2} ₽";
        }

        // Обработчик события для кнопки закрытия окна
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}