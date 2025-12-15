# I See Sharp

A gamified web application to practice and master C# syntax, methods, and best practices through interactive coding exercises.

## Tech Stack

- **Frontend**: Next.js 15, React 19, TypeScript, Tailwind CSS, Monaco Editor
- **Backend**: .NET 10, ASP.NET Core, Entity Framework Core
- **Database**: PostgreSQL
- **Architecture**: Clean Architecture, CQRS with MediatR

## Project Structure

```
I-See-Sharp/
├── src/
│   ├── frontend/                 # Next.js 15 application
│   │   ├── app/                  # App Router pages
│   │   ├── components/           # React components
│   │   └── lib/                  # Utilities
│   │
│   └── backend/                  # .NET 10 solution
│       ├── ISeeSharp.Api/        # Web API (Controllers, Program.cs)
│       ├── ISeeSharp.Application/# Use cases, CQRS handlers
│       ├── ISeeSharp.Domain/     # Entities, Interfaces
│       └── ISeeSharp.Infrastructure/ # EF Core, Repositories
│
├── docker-compose.yml            # PostgreSQL container
└── README.md
```

## Prerequisites

- Node.js 20+
- .NET 10 SDK
- Docker (for PostgreSQL)

## Getting Started

### 1. Start the database

```bash
docker-compose up -d
```

### 2. Run the backend

```bash
cd src/backend
dotnet restore
dotnet ef database update --project ISeeSharp.Infrastructure --startup-project ISeeSharp.Api
dotnet run --project ISeeSharp.Api
```

The API will be available at `https://localhost:5001` (Swagger UI: `/swagger`).

### 3. Run the frontend

```bash
cd src/frontend
npm install
npm run dev
```

The app will be available at `http://localhost:3000`.

## Features (Planned)

- [ ] Interactive code editor with C# syntax highlighting
- [ ] Categorized exercises (Syntax, LINQ, Async/Await, etc.)
- [ ] Difficulty levels (Beginner to Expert)
- [ ] Score system and leaderboard
- [ ] User profiles and progress tracking
- [ ] Gamification elements (achievements, streaks)
