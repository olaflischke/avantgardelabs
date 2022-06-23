using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NorthwindDal.Model;

namespace NorthwindDalTests;

public class Tests
{
    private NorthwindContext context;

    [SetUp]
    public void Setup()
    {
        context = new NorthwindContext();
        context.Log = LogIt;
    }

    private void LogIt(string logString)
    {
        Console.WriteLine(logString);
    }

    [Test]
    public void CountCustomers()
    {
        int customerCount = context.Customers.Count();
        Console.WriteLine($"Anzahl Kunden: {customerCount}");
        Assert.AreEqual(91, customerCount);
    }

    [Test]
    public void GetFirstCustomer()
    {
        //Customer customer = context.Customers.First();
        Customer customer = context.Customers.Where(cu => cu.CustomerId == "ANATR").First();

        Console.WriteLine($"{customer.CompanyName}, {customer.CustomerId}");
    }

    [Test]
    public void GetFirstCustomerWithOrders()
    {
        Customer customer = context.Customers.First(); //.Include(cu => cu.Orders)
        //.ThenInclude(od => od.OrderDetails).First();

        foreach (Order od in customer.Orders)
        {
            Console.WriteLine($"{od.OrderId}");
        }

        Console.WriteLine(customer.Orders.Count());
    }

    [Test]
    public void GetCountries()
    {
        var countries = context.Customers.Select(cu => cu.Country).Distinct();

        foreach (string land in countries)
        {
            Console.WriteLine(land);
        }
    }

    [Test]
    public void CustomerWithLazyOrders()
    {
        // "Alfreds Futterkiste" laden
        Customer customer = context.Customers.Find("ALFKI");

        // Order-Count von Alfred: 0
        Console.WriteLine(customer.Orders.Count());

        // Einige Orders laden, darunter die von Alfred 
        List<Order> orders = context.Orders.Where(od => od.Customer == customer || od.CustomerId == "ANATR").ToList();

        // Order-Count von Alfred: 6
        Console.WriteLine(customer.Orders.Count());
    }

    [Test]
    public void CustomerWithLazyOrdersOhneTracking()
    {
        // "Alfreds Futterkiste" laden
        Customer customer = context.Customers.Find("ALFKI");

        // Order-Count von Alfred: 0
        Console.WriteLine(customer.Orders.Count());

        // Einige Orders laden, darunter die von Alfred 
        List<Order> orders = context.Orders.AsNoTracking()
            .Where(od => od.Customer == customer || od.CustomerId == "ANATR").ToList();

        // Order-Count von Alfred: 0
        Console.WriteLine(customer.Orders.Count());

        // Orders jetzt doch dem Context hinzufügen
        context.AttachRange(orders);
    }

    [Test]
    public void ChangeCustomer()
    {
        Customer alfki = context.Customers.Find("ALFKI");

        // Unchanged
        Console.WriteLine($"ALFKI: {context.Entry(alfki).State}");

        alfki.ContactName = "Maria Maier";

        // Modified
        Console.WriteLine($"ALFKI: {context.Entry(alfki).State}");

        context.SaveChanges();

        // Unchanged
        Console.WriteLine($"ALFKI: {context.Entry(alfki).State}");
    }

    [Test]
    public void DualContexts()
    {
        NorthwindContext ctx1 = new();
        ctx1.Log = LogIt;
        NorthwindContext ctx2 = new();
        ctx2.Log = LogIt;

        Customer alfki1 = ctx1.Customers.Find("ALFKI");
        //Customer alfki2 = ctx2.Customers.Find("ALFKI");

        Customer anatr2 = ctx2.Customers.Find("ANATR");

        Console.WriteLine($"Before Attach: {ctx2.Entry(anatr2).State}");

        ctx1.Attach(anatr2);

        Console.WriteLine($"After Attach (ctx1): {ctx1.Entry(anatr2).State}");
        Console.WriteLine($"After Attach (ctx2): {ctx2.Entry(anatr2).State}");

        anatr2.ContactName = "Charlotte";

        Console.WriteLine($"After Change (ctx1): {ctx1.Entry(anatr2).State}");
        Console.WriteLine($"After Change (ctx2): {ctx2.Entry(anatr2).State}");

        ctx1.SaveChanges();
        Console.WriteLine("Context 1 saved.");
        if (ctx2.Entry(anatr2).CurrentValues != ctx2.Entry(anatr2).GetDatabaseValues())
        {
            ctx2.SaveChanges();
            Console.WriteLine("Context 2 saved");
        }

        Console.WriteLine($"After Save (ctx1): {ctx1.Entry(anatr2).State}");
        Console.WriteLine($"After Save (ctx2): {ctx2.Entry(anatr2).State}");
    }

    [Test]
    public void ChangeCustomerWithOverride()
    {
        Customer alfki = context.Customers.Find("ALFKI");

        alfki.ContactName = "Maria Anders";

        try
        {
            context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Client wins - trotzdem überschreiben
            // context.Entry(alfki).OriginalValues.SetValues(context.Entry(alfki).GetDatabaseValues());
            // context.SaveChanges();

            // Database wins - aktuelle Werte laden
            context.Entry(alfki).Reload();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [Test]
    public void UpdateCustomerWithOrders()
    {
        Customer alfki = context.Customers.Include(cu => cu.Orders).First(cu => cu.CustomerId == "ALFKI");
        //alfki.ContactName = "Marius Paul";
        //context.Entry(alfki).State = EntityState.Unchanged;

        Order lastOrder = alfki.Orders.Last();

        lastOrder.ShippedDate = new DateOnly(2006, 06, 23);

        //context.SaveChanges();
        Console.WriteLine($"ALFKI: {context.Entry(alfki).State}");

        foreach (Order order in alfki.Orders)
        {
            Console.WriteLine($"Order: {order.OrderId} - {context.Entry(order).State}");
        }

        Console.WriteLine("Updating...");
        context.Update(lastOrder);

        Console.WriteLine($"ALFKI: {context.Entry(alfki).State}");
        Console.WriteLine($"LastOrder: {context.Entry(lastOrder).State}");
        
        foreach (Order order in alfki.Orders)
        {
            Console.WriteLine($"Order: {order.OrderId} - {context.Entry(order).State}");
        }

        Console.WriteLine("Saving...");
        context.SaveChanges();
    }
}