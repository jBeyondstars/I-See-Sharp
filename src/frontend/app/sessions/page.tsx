import Link from "next/link";
import type { Metadata } from "next";
import type { SessionDifficulty, SessionCategory } from "@/types";

export const metadata: Metadata = {
  title: "Sessions | I See Sharp",
  description: "Browse and practice C# coding sessions",
};

interface SessionCardProps {
  title: string;
  slug: string;
  description: string;
  difficulty: SessionDifficulty;
  category: SessionCategory;
  points: number;
  estimatedMinutes: number;
  totalLines: number;
  totalFiles: number;
}

function SessionCard({
  title,
  slug,
  description,
  difficulty,
  category,
  points,
  estimatedMinutes,
  totalLines,
  totalFiles,
}: SessionCardProps) {
  const difficultyColors = {
    Easy: "bg-green-500/20 text-green-400 border-green-500/30",
    Medium: "bg-yellow-500/20 text-yellow-400 border-yellow-500/30",
    Hard: "bg-orange-500/20 text-orange-400 border-orange-500/30",
    Expert: "bg-red-500/20 text-red-400 border-red-500/30",
  };

  return (
    <Link href={`/sessions/${slug}`}>
      <div className="bg-slate-900 border border-slate-800 rounded-lg p-5 hover:border-purple-500/50 hover:bg-slate-800/50 transition-all group cursor-pointer">
        <div className="flex items-start justify-between mb-3">
          <h3 className="text-lg font-semibold text-white group-hover:text-purple-400 transition-colors">
            {title}
          </h3>
          <span
            className={`text-xs px-2 py-1 rounded border ${difficultyColors[difficulty]}`}
          >
            {difficulty}
          </span>
        </div>

        <p className="text-slate-400 text-sm mb-4 line-clamp-2">{description}</p>

        <div className="flex items-center gap-4 text-xs text-slate-500">
          <span className="flex items-center gap-1">
            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z" />
            </svg>
            {category}
          </span>
          <span className="flex items-center gap-1">
            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
            ~{estimatedMinutes} min
          </span>
          <span className="flex items-center gap-1">
            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
            </svg>
            {totalFiles} file{totalFiles > 1 ? "s" : ""}
          </span>
          <span className="flex items-center gap-1">
            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h7" />
            </svg>
            {totalLines} lines
          </span>
        </div>

        <div className="mt-4 pt-4 border-t border-slate-800 flex items-center justify-between">
          <span className="text-purple-400 font-semibold">{points} pts</span>
          <span className="text-slate-500 text-sm group-hover:text-purple-400 transition-colors">
            Start Session →
          </span>
        </div>
      </div>
    </Link>
  );
}

const mockSessions: SessionCardProps[] = [
  {
    title: "Hello World",
    slug: "hello-world",
    description: "Write your first C# program that prints a greeting to the console.",
    difficulty: "Easy",
    category: "Syntax",
    points: 10,
    estimatedMinutes: 2,
    totalLines: 1,
    totalFiles: 1,
  },
  {
    title: "Variables and Types",
    slug: "variables-and-types",
    description: "Learn to declare and use different variable types in C#.",
    difficulty: "Easy",
    category: "Variables",
    points: 15,
    estimatedMinutes: 3,
    totalLines: 7,
    totalFiles: 1,
  },
  {
    title: "Basic Calculator",
    slug: "basic-calculator",
    description: "Implement a simple calculator with basic operations using interfaces.",
    difficulty: "Easy",
    category: "Interfaces",
    points: 30,
    estimatedMinutes: 8,
    totalLines: 25,
    totalFiles: 3,
  },
  {
    title: "LINQ Basics",
    slug: "linq-basics",
    description: "Learn the fundamentals of LINQ for querying collections.",
    difficulty: "Medium",
    category: "Linq",
    points: 25,
    estimatedMinutes: 5,
    totalLines: 9,
    totalFiles: 1,
  },
  {
    title: "User Model",
    slug: "user-model",
    description: "Create a simple user model with properties and a constructor.",
    difficulty: "Easy",
    category: "Classes",
    points: 20,
    estimatedMinutes: 4,
    totalLines: 17,
    totalFiles: 1,
  },
  {
    title: "Async/Await Basics",
    slug: "async-await-basics",
    description: "Learn the basics of asynchronous programming in C#.",
    difficulty: "Medium",
    category: "AsyncAwait",
    points: 35,
    estimatedMinutes: 6,
    totalLines: 15,
    totalFiles: 1,
  },
  {
    title: "Pattern Matching",
    slug: "pattern-matching",
    description: "Explore C# pattern matching features for cleaner code.",
    difficulty: "Medium",
    category: "ModernCSharp",
    points: 30,
    estimatedMinutes: 5,
    totalLines: 12,
    totalFiles: 1,
  },
  {
    title: "Records in C#",
    slug: "records-csharp",
    description: "Learn to use records for immutable data types.",
    difficulty: "Medium",
    category: "ModernCSharp",
    points: 25,
    estimatedMinutes: 4,
    totalLines: 11,
    totalFiles: 1,
  },
  {
    title: "Exception Handling",
    slug: "exception-handling",
    description: "Master try-catch-finally blocks and custom exceptions.",
    difficulty: "Medium",
    category: "ErrorHandling",
    points: 25,
    estimatedMinutes: 5,
    totalLines: 17,
    totalFiles: 1,
  },
  {
    title: "Repository Pattern",
    slug: "repository-pattern",
    description: "Implement the repository pattern for data access abstraction.",
    difficulty: "Hard",
    category: "Patterns",
    points: 50,
    estimatedMinutes: 12,
    totalLines: 25,
    totalFiles: 3,
  },
];

export default function SessionsPage() {
  return (
    <main className="min-h-screen bg-slate-950">
      <div className="container mx-auto px-4 py-8">
        <div className="mb-8">
          <Link
            href="/"
            className="text-slate-500 hover:text-slate-300 text-sm mb-4 inline-flex items-center gap-1"
          >
            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 19l-7-7 7-7" />
            </svg>
            Back to Home
          </Link>
          <h1 className="text-4xl font-bold text-white mb-2">Coding Sessions</h1>
          <p className="text-slate-400">
            Practice your C# skills with guided coding sessions
          </p>
        </div>

        <div className="flex gap-4 mb-6 flex-wrap">
          <select className="bg-slate-900 border border-slate-700 rounded-lg px-4 py-2 text-slate-300 text-sm focus:outline-none focus:border-purple-500">
            <option value="">All Difficulties</option>
            <option value="Easy">Easy</option>
            <option value="Medium">Medium</option>
            <option value="Hard">Hard</option>
            <option value="Expert">Expert</option>
          </select>
          <select className="bg-slate-900 border border-slate-700 rounded-lg px-4 py-2 text-slate-300 text-sm focus:outline-none focus:border-purple-500">
            <option value="">All Categories</option>
            <option value="Syntax">Syntax</option>
            <option value="Variables">Variables</option>
            <option value="Classes">Classes</option>
            <option value="Interfaces">Interfaces</option>
            <option value="Linq">LINQ</option>
            <option value="AsyncAwait">Async/Await</option>
            <option value="ModernCSharp">Modern C#</option>
            <option value="Patterns">Patterns</option>
          </select>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {mockSessions.map((session) => (
            <SessionCard key={session.slug} {...session} />
          ))}
        </div>
      </div>
    </main>
  );
}
