export default function HomePage() {
  return (
    <main className="min-h-screen bg-gradient-to-b from-gray-900 to-gray-800 text-white">
      <div className="container mx-auto px-4 py-16">
        <div className="text-center">
          <h1 className="text-6xl font-bold mb-4">
            I See <span className="text-purple-500">Sharp</span>
          </h1>
          <p className="text-xl text-gray-300 mb-8">
            Master C# through gamified coding exercises
          </p>
          <div className="flex gap-4 justify-center">
            <a
              href="/practice"
              className="bg-purple-600 hover:bg-purple-700 px-8 py-3 rounded-lg font-semibold transition"
            >
              Start Practice
            </a>
            <a
              href="/exercises"
              className="border border-purple-600 hover:bg-purple-600/20 px-8 py-3 rounded-lg font-semibold transition"
            >
              Browse Exercises
            </a>
          </div>
        </div>
      </div>
    </main>
  );
}
