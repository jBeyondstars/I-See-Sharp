"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { IDE } from "@/components/ide";
import { useSessionStore, useMetricsStore } from "@/stores";
import type { Session } from "@/types";

const mockSessionsData: Record<string, Session> = {
  "hello-world": {
    id: "1",
    title: "Hello World",
    slug: "hello-world",
    description: "Write your first C# program that prints a greeting to the console.",
    instructions: "Create a simple console application that outputs 'Hello, World!' to the screen.",
    objectivesJson: null,
    hintsJson: null,
    difficulty: "Easy",
    category: "Syntax",
    points: 10,
    estimatedMinutes: 2,
    targetWpm: 30,
    isActive: true,
    isPremium: false,
    totalAttempts: 0,
    totalCompletions: 0,
    averageAccuracy: 0,
    averageWpm: 0,
    totalLines: 1,
    totalCharacters: 36,
    files: [
      {
        id: "1-1",
        sessionId: "1",
        path: "Program.cs",
        displayName: "Program.cs",
        language: "csharp",
        targetContent: 'Console.WriteLine("Hello, World!");',
        editableRegionsJson: null,
        sortOrder: 0,
        totalLines: 1,
        totalCharacters: 36,
      },
    ],
  },
  "variables-and-types": {
    id: "2",
    title: "Variables and Types",
    slug: "variables-and-types",
    description: "Learn to declare and use different variable types in C#.",
    instructions: "Declare variables of different types and assign values to them.",
    objectivesJson: null,
    hintsJson: null,
    difficulty: "Easy",
    category: "Variables",
    points: 15,
    estimatedMinutes: 3,
    targetWpm: 35,
    isActive: true,
    isPremium: false,
    totalAttempts: 0,
    totalCompletions: 0,
    averageAccuracy: 0,
    averageWpm: 0,
    totalLines: 7,
    totalCharacters: 180,
    files: [
      {
        id: "2-1",
        sessionId: "2",
        path: "Program.cs",
        displayName: "Program.cs",
        language: "csharp",
        targetContent: `int age = 25;
string name = "John";
double salary = 50000.50;
bool isActive = true;

Console.WriteLine($"Name: {name}, Age: {age}");
Console.WriteLine($"Salary: {salary}, Active: {isActive}");`,
        editableRegionsJson: null,
        sortOrder: 0,
        totalLines: 7,
        totalCharacters: 180,
      },
    ],
  },
  "basic-calculator": {
    id: "3",
    title: "Basic Calculator",
    slug: "basic-calculator",
    description: "Implement a simple calculator with basic operations using interfaces.",
    instructions: "Create an interface for calculator operations and implement it in a class.",
    objectivesJson: null,
    hintsJson: null,
    difficulty: "Easy",
    category: "Interfaces",
    points: 30,
    estimatedMinutes: 8,
    targetWpm: 35,
    isActive: true,
    isPremium: false,
    totalAttempts: 0,
    totalCompletions: 0,
    averageAccuracy: 0,
    averageWpm: 0,
    totalLines: 35,
    totalCharacters: 650,
    files: [
      {
        id: "3-1",
        sessionId: "3",
        path: "ICalculator.cs",
        displayName: "ICalculator.cs",
        language: "csharp",
        targetContent: `namespace Calculator;

public interface ICalculator
{
    int Add(int a, int b);
    int Subtract(int a, int b);
    int Multiply(int a, int b);
    int Divide(int a, int b);
}`,
        editableRegionsJson: null,
        sortOrder: 0,
        totalLines: 10,
        totalCharacters: 180,
      },
      {
        id: "3-2",
        sessionId: "3",
        path: "Calculator.cs",
        displayName: "Calculator.cs",
        language: "csharp",
        targetContent: `namespace Calculator;

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
}`,
        editableRegionsJson: null,
        sortOrder: 1,
        totalLines: 23,
        totalCharacters: 400,
      },
      {
        id: "3-3",
        sessionId: "3",
        path: "Program.cs",
        displayName: "Program.cs",
        language: "csharp",
        targetContent: `using Calculator;

var calc = new Calculator();
Console.WriteLine(calc.Add(5, 3));
Console.WriteLine(calc.Subtract(10, 4));
Console.WriteLine(calc.Multiply(6, 7));
Console.WriteLine(calc.Divide(20, 5));`,
        editableRegionsJson: null,
        sortOrder: 2,
        totalLines: 7,
        totalCharacters: 200,
      },
    ],
  },
  "linq-basics": {
    id: "4",
    title: "LINQ Basics",
    slug: "linq-basics",
    description: "Learn the fundamentals of LINQ for querying collections.",
    instructions: "Use LINQ methods to filter, transform, and aggregate data.",
    objectivesJson: null,
    hintsJson: null,
    difficulty: "Medium",
    category: "Linq",
    points: 25,
    estimatedMinutes: 5,
    targetWpm: 40,
    isActive: true,
    isPremium: false,
    totalAttempts: 0,
    totalCompletions: 0,
    averageAccuracy: 0,
    averageWpm: 0,
    totalLines: 9,
    totalCharacters: 350,
    files: [
      {
        id: "4-1",
        sessionId: "4",
        path: "Program.cs",
        displayName: "Program.cs",
        language: "csharp",
        targetContent: `var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNumbers = numbers.Where(n => n % 2 == 0);
var doubled = numbers.Select(n => n * 2);
var sum = numbers.Sum();
var average = numbers.Average();

Console.WriteLine($"Even: {string.Join(", ", evenNumbers)}");
Console.WriteLine($"Doubled: {string.Join(", ", doubled)}");
Console.WriteLine($"Sum: {sum}, Average: {average}");`,
        editableRegionsJson: null,
        sortOrder: 0,
        totalLines: 9,
        totalCharacters: 350,
      },
    ],
  },
};

export default function SessionPage() {
  const params = useParams();
  const slug = params.slug as string;
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const setSession = useSessionStore((s) => s.setSession);
  const resetSession = useSessionStore((s) => s.resetSession);
  const reset = useMetricsStore((s) => s.reset);

  useEffect(() => {
    resetSession();
    reset();

    const session = mockSessionsData[slug];
    if (session) {
      setSession(session);
      setLoading(false);
    } else {
      setError("Session not found");
      setLoading(false);
    }
  }, [slug, setSession, resetSession, reset]);

  if (loading) {
    return (
      <div className="h-screen w-screen bg-slate-950 flex items-center justify-center">
        <div className="text-center">
          <div className="w-12 h-12 border-4 border-purple-500 border-t-transparent rounded-full animate-spin mx-auto mb-4" />
          <p className="text-slate-400">Loading session...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="h-screen w-screen bg-slate-950 flex items-center justify-center">
        <div className="text-center">
          <p className="text-red-400 mb-4">{error}</p>
          <a href="/sessions" className="text-purple-400 hover:underline">
            Back to sessions
          </a>
        </div>
      </div>
    );
  }

  return <IDE />;
}
