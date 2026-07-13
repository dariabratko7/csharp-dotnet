using Microsoft.EntityFrameworkCore;

namespace ShopParallelism
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<bool> OrderProductAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || product.AvailableQuantity < quantity) return false;

            product.AvailableQuantity -= quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateProductAsync(Product clientProduct)
        {
            _context.Entry(clientProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine(" Зміни успішно збережено першим адміністратором!");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("\n [КОНФЛІКТ ПАРАЛЕЛІЗМУ]: Інший адміністратор вже змінив цей товар, поки ви його редагували.");

                var entry = ex.Entries.Single();

                var proposedValues = entry.CurrentValues;

                var databaseValues = await entry.GetDatabaseValuesAsync();

                if (databaseValues == null)
                {
                    Console.WriteLine("Помилка: Товар взагалі видалено іншим користувачем.");
                    throw;
                }

                Console.WriteLine("--- Дані в базі даних (актуальні): ---");
                Console.WriteLine($"- Назва: {databaseValues["Name"]}, Ціна: {databaseValues["Price"]}, Опис: {databaseValues["Description"]}");

                Console.WriteLine("--- Ваші дані (відхилені): ---");
                Console.WriteLine($"- Назва: {proposedValues["Name"]}, Ціна: {proposedValues["Price"]}, Опис: {proposedValues["Description"]}");

                throw new Exception("Операцію скасовано через конфлікт паралелізму. Оновіть дані.");
            }
        }
    }
}