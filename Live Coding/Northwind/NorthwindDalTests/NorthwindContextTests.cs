using Microsoft.EntityFrameworkCore;
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
        Customer customer = context.Customers.Include(cu => cu.Orders)
            .ThenInclude(od => od.OrderDetails).First();

        foreach (Order od in customer.Orders)
        {
            Console.WriteLine($"{od.OrderId}");
        }
        
        //Console.WriteLine(customer.Orders.Count());
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
}