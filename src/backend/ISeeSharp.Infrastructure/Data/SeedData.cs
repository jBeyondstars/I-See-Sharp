using ISeeSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISeeSharp.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Sessions.AnyAsync())
        {
            return;
        }

        var sessions = CreateSessions();

        foreach (var session in sessions)
        {
            await context.Sessions.AddAsync(session);
        }

        await context.SaveChangesAsync();
    }

    private static List<Session> CreateSessions()
    {
        var sessions = new List<Session>();

        // Session 1: Hello World
        var helloWorld = Session.Create(
            title: "Hello World",
            slug: "hello-world",
            description: "Write your first C# program that prints a greeting to the console.",
            instructions: "Create a simple console application that outputs 'Hello, World!' to the screen.",
            difficulty: SessionDifficulty.Easy,
            category: SessionCategory.Syntax,
            points: 10,
            estimatedMinutes: 2,
            targetWpm: 30);

        var helloWorldFile = SessionFile.Create(
            sessionId: helloWorld.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"Console.WriteLine(""Hello, World!"");",
            sortOrder: 0);

        helloWorld.AddFile(helloWorldFile);
        sessions.Add(helloWorld);

        // Session 2: Variables and Types
        var variablesTypes = Session.Create(
            title: "Variables and Types",
            slug: "variables-and-types",
            description: "Learn to declare and use different variable types in C#.",
            instructions: "Declare variables of different types and assign values to them.",
            difficulty: SessionDifficulty.Easy,
            category: SessionCategory.Variables,
            points: 15,
            estimatedMinutes: 3,
            targetWpm: 35);

        var variablesFile = SessionFile.Create(
            sessionId: variablesTypes.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"int age = 25;
string name = ""John"";
double salary = 50000.50;
bool isActive = true;

Console.WriteLine($""Name: {name}, Age: {age}"");
Console.WriteLine($""Salary: {salary}, Active: {isActive}"");",
            sortOrder: 0);

        variablesTypes.AddFile(variablesFile);
        sessions.Add(variablesTypes);

        // Session 3: Basic Calculator (Multi-file)
        var calculator = Session.Create(
            title: "Basic Calculator",
            slug: "basic-calculator",
            description: "Implement a simple calculator with basic operations using interfaces.",
            instructions: "Create an interface for calculator operations and implement it in a class.",
            difficulty: SessionDifficulty.Easy,
            category: SessionCategory.Interfaces,
            points: 30,
            estimatedMinutes: 8,
            targetWpm: 35);

        var calcInterface = SessionFile.Create(
            sessionId: calculator.Id,
            path: "ICalculator.cs",
            displayName: "ICalculator.cs",
            targetContent: @"namespace Calculator;

public interface ICalculator
{
    int Add(int a, int b);
    int Subtract(int a, int b);
    int Multiply(int a, int b);
    int Divide(int a, int b);
}",
            sortOrder: 0);

        var calcClass = SessionFile.Create(
            sessionId: calculator.Id,
            path: "Calculator.cs",
            displayName: "Calculator.cs",
            targetContent: @"namespace Calculator;

public class Calculator : ICalculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public int Divide(int a, int b)
    {
        return a / b;
    }
}",
            sortOrder: 1);

        var calcProgram = SessionFile.Create(
            sessionId: calculator.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"using Calculator;

var calc = new Calculator();
Console.WriteLine(calc.Add(5, 3));
Console.WriteLine(calc.Subtract(10, 4));
Console.WriteLine(calc.Multiply(6, 7));
Console.WriteLine(calc.Divide(20, 5));",
            sortOrder: 2);

        calculator.AddFile(calcInterface);
        calculator.AddFile(calcClass);
        calculator.AddFile(calcProgram);
        sessions.Add(calculator);

        // Session 4: LINQ Basics
        var linqBasics = Session.Create(
            title: "LINQ Basics",
            slug: "linq-basics",
            description: "Learn the fundamentals of LINQ for querying collections.",
            instructions: "Use LINQ methods to filter, transform, and aggregate data.",
            difficulty: SessionDifficulty.Medium,
            category: SessionCategory.Linq,
            points: 25,
            estimatedMinutes: 5,
            targetWpm: 40);

        var linqFile = SessionFile.Create(
            sessionId: linqBasics.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNumbers = numbers.Where(n => n % 2 == 0);
var doubled = numbers.Select(n => n * 2);
var sum = numbers.Sum();
var average = numbers.Average();

Console.WriteLine($""Even: {string.Join("", "", evenNumbers)}"");
Console.WriteLine($""Doubled: {string.Join("", "", doubled)}"");
Console.WriteLine($""Sum: {sum}, Average: {average}"");",
            sortOrder: 0);

        linqBasics.AddFile(linqFile);
        sessions.Add(linqBasics);

        // Session 5: User Model
        var userModel = Session.Create(
            title: "User Model",
            slug: "user-model",
            description: "Create a simple user model with properties and a constructor.",
            instructions: "Define a User class with common properties like Id, Name, and Email.",
            difficulty: SessionDifficulty.Easy,
            category: SessionCategory.Classes,
            points: 20,
            estimatedMinutes: 4,
            targetWpm: 35);

        var userModelFile = SessionFile.Create(
            sessionId: userModel.Id,
            path: "User.cs",
            displayName: "User.cs",
            targetContent: @"public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $""User {Id}: {Name} ({Email})"";
    }
}",
            sortOrder: 0);

        userModel.AddFile(userModelFile);
        sessions.Add(userModel);

        // Session 6: Async/Await Basics
        var asyncBasics = Session.Create(
            title: "Async/Await Basics",
            slug: "async-await-basics",
            description: "Learn the basics of asynchronous programming in C#.",
            instructions: "Create async methods and use await to handle asynchronous operations.",
            difficulty: SessionDifficulty.Medium,
            category: SessionCategory.AsyncAwait,
            points: 35,
            estimatedMinutes: 6,
            targetWpm: 40);

        var asyncFile = SessionFile.Create(
            sessionId: asyncBasics.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"await RunAsync();

async Task RunAsync()
{
    Console.WriteLine(""Starting..."");

    var result = await FetchDataAsync();
    Console.WriteLine(result);

    Console.WriteLine(""Done!"");
}

async Task<string> FetchDataAsync()
{
    await Task.Delay(1000);
    return ""Data fetched successfully!"";
}",
            sortOrder: 0);

        asyncBasics.AddFile(asyncFile);
        sessions.Add(asyncBasics);

        // Session 7: Pattern Matching
        var patternMatching = Session.Create(
            title: "Pattern Matching",
            slug: "pattern-matching",
            description: "Explore C# pattern matching features for cleaner code.",
            instructions: "Use switch expressions and pattern matching to handle different cases.",
            difficulty: SessionDifficulty.Medium,
            category: SessionCategory.ModernCSharp,
            points: 30,
            estimatedMinutes: 5,
            targetWpm: 40);

        var patternFile = SessionFile.Create(
            sessionId: patternMatching.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"string GetDiscount(object customer) => customer switch
{
    string s when s == ""VIP"" => ""30% discount"",
    int age when age < 18 => ""Student discount: 20%"",
    int age when age > 65 => ""Senior discount: 25%"",
    null => ""No discount"",
    _ => ""Standard: 10%""
};

Console.WriteLine(GetDiscount(""VIP""));
Console.WriteLine(GetDiscount(15));
Console.WriteLine(GetDiscount(70));
Console.WriteLine(GetDiscount(""Regular""));",
            sortOrder: 0);

        patternMatching.AddFile(patternFile);
        sessions.Add(patternMatching);

        // Session 8: Records
        var records = Session.Create(
            title: "Records in C#",
            slug: "records-csharp",
            description: "Learn to use records for immutable data types.",
            instructions: "Create record types and understand their features.",
            difficulty: SessionDifficulty.Medium,
            category: SessionCategory.ModernCSharp,
            points: 25,
            estimatedMinutes: 4,
            targetWpm: 40);

        var recordsFile = SessionFile.Create(
            sessionId: records.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"public record Person(string FirstName, string LastName);

public record Employee(string FirstName, string LastName, string Department)
    : Person(FirstName, LastName);

var person = new Person(""John"", ""Doe"");
var employee = new Employee(""Jane"", ""Smith"", ""Engineering"");

Console.WriteLine(person);
Console.WriteLine(employee);

var updated = person with { LastName = ""Johnson"" };
Console.WriteLine(updated);",
            sortOrder: 0);

        records.AddFile(recordsFile);
        sessions.Add(records);

        // Session 9: Exception Handling
        var exceptionHandling = Session.Create(
            title: "Exception Handling",
            slug: "exception-handling",
            description: "Master try-catch-finally blocks and custom exceptions.",
            instructions: "Implement proper error handling patterns in C#.",
            difficulty: SessionDifficulty.Medium,
            category: SessionCategory.ErrorHandling,
            points: 25,
            estimatedMinutes: 5,
            targetWpm: 38);

        var exceptionFile = SessionFile.Create(
            sessionId: exceptionHandling.Id,
            path: "Program.cs",
            displayName: "Program.cs",
            targetContent: @"try
{
    var result = Divide(10, 0);
    Console.WriteLine(result);
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($""Error: {ex.Message}"");
}
finally
{
    Console.WriteLine(""Operation completed"");
}

int Divide(int a, int b)
{
    if (b == 0)
        throw new DivideByZeroException(""Cannot divide by zero"");
    return a / b;
}",
            sortOrder: 0);

        exceptionHandling.AddFile(exceptionFile);
        sessions.Add(exceptionHandling);

        // Session 10: Repository Pattern (Multi-file)
        var repositoryPattern = Session.Create(
            title: "Repository Pattern",
            slug: "repository-pattern",
            description: "Implement the repository pattern for data access abstraction.",
            instructions: "Create a generic repository interface and implementation.",
            difficulty: SessionDifficulty.Hard,
            category: SessionCategory.Patterns,
            points: 50,
            estimatedMinutes: 12,
            targetWpm: 42);

        var entityFile = SessionFile.Create(
            sessionId: repositoryPattern.Id,
            path: "Entity.cs",
            displayName: "Entity.cs",
            targetContent: @"public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}",
            sortOrder: 0);

        var repoInterface = SessionFile.Create(
            sessionId: repositoryPattern.Id,
            path: "IRepository.cs",
            displayName: "IRepository.cs",
            targetContent: @"public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}",
            sortOrder: 1);

        var productEntity = SessionFile.Create(
            sessionId: repositoryPattern.Id,
            path: "Product.cs",
            displayName: "Product.cs",
            targetContent: @"public class Product : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}",
            sortOrder: 2);

        repositoryPattern.AddFile(entityFile);
        repositoryPattern.AddFile(repoInterface);
        repositoryPattern.AddFile(productEntity);
        sessions.Add(repositoryPattern);

        return sessions;
    }
}
