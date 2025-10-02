using Microsoft.Data.SqlClient;


namespace TradingCompanyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductDal productDal = new ProductDal();
            CustomerDal customerDal = new CustomerDal();
            OrderDal orderDal = new OrderDal();

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Manage Orders");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        EntityMenu("Product", productDal);
                        break;
                    case "2":
                        EntityMenu("Customer", customerDal);
                        break;
                    case "3":
                        EntityMenu("Order", orderDal);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to main menu...");
                    Console.ReadKey();
                }
            }
        }

        static void EntityMenu<T>(string entityName, IEntityDal<T> dal)
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine($"=== {entityName} Menu ===");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Get All");
                Console.WriteLine("3. Get By ID");
                Console.WriteLine("4. Update");
                Console.WriteLine("5. Delete");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        dal.Create(EnterEntity<T>());
                        Console.WriteLine($"{entityName} created!");
                        break;
                    case "2":
                        List<T> all = dal.GetAll();
                        foreach (var item in all)
                            Console.WriteLine(item);
                        break;
                    case "3":
                        Console.Write("Enter ID: ");
                        int id = int.Parse(Console.ReadLine());
                        var entity = dal.GetByID(id);
                         //Console.WriteLine(entity ?? $"{entityName} not found.");
                        break;
                    case "4":
                        Console.Write("Enter ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        T entityToUpdate = dal.GetByID(updateId);
                        if (entityToUpdate != null)
                        {
                            dal.Update(EnterEntity<T>());
                            Console.WriteLine($"{entityName} updated!");
                        }
                        else
                        {
                            Console.WriteLine($"{entityName} not found.");
                        }
                        break;
                    case "5":
                        Console.Write("Enter ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        bool deleted = dal.Delete(deleteId);
                        Console.WriteLine(deleted ? $"{entityName} deleted!" : $"{entityName} not found.");
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // This is a placeholder method for entering entity data
        static T EnterEntity<T>()
        {
            if (typeof(T) == typeof(Product))
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Price: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Quantity: ");
                int qty = int.Parse(Console.ReadLine());

                return (T)(object)new Product { Name = name, Price = price, Quantity = qty };
            }

            if (typeof(T) == typeof(Customer))
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();

                return (T)(object)new Customer { Name = name, Email = email };
            }

            if (typeof(T) == typeof(Order))
            {
                Console.Write("Product ID: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());
                Console.Write("Quantity: ");
                int qty = int.Parse(Console.ReadLine());

                return (T)(object)new Order { ProductID = productId, CustomerID = customerId, Quantity = qty };
            }

            return default(T);
        }
    }

    // Entities
    public class Product { public int ProductID; public string Name; public decimal Price; public int Quantity; public override string ToString() => $"{ProductID}: {Name}, {Price}, {Quantity}"; }
    public class Customer { public int CustomerID; public string Name; public string Email; public override string ToString() => $"{CustomerID}: {Name}, {Email}"; }
    public class Order { public int OrderID; public int ProductID; public int CustomerID; public int Quantity; public override string ToString() => $"{OrderID}: Product {ProductID}, Customer {CustomerID}, Qty {Quantity}"; }

    // DAL Interfaces
    public interface IEntityDal<T>
    {
        T Create(T entity);
        List<T> GetAll();
        T GetByID(int id);
        T Update(T entity);
        bool Delete(int id);
    }

    // Example DAL classes (placeholders)
    public class ProductDal : IEntityDal<Product> { public Product Create(Product entity) => entity; public List<Product> GetAll() => new List<Product>(); public Product GetByID(int id) => null; public Product Update(Product entity) => entity; public bool Delete(int id) => true; }
    public class CustomerDal : IEntityDal<Customer> { public Customer Create(Customer entity) => entity; public List<Customer> GetAll() => new List<Customer>(); public Customer GetByID(int id) => null; public Customer Update(Customer entity) => entity; public bool Delete(int id) => true; }
    public class OrderDal : IEntityDal<Order> { public Order Create(Order entity) => entity; public List<Order> GetAll() => new List<Order>(); public Order GetByID(int id) => null; public Order Update(Order entity) => entity; public bool Delete(int id) => true; }
}
