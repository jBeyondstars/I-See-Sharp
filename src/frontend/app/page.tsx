import Link from "next/link";

export default function HomePage() {
  return (
    <main className="min-h-screen bg-slate-950 text-white">
      <div className="container mx-auto px-4 py-16">
        <div className="text-center max-w-3xl mx-auto">
          <div className="mb-8">
            <span className="text-purple-500 text-8xl font-bold">I#</span>
          </div>
          <h1 className="text-5xl font-bold mb-4">
            I See <span className="text-purple-500">Sharp</span>
          </h1>
          <p className="text-xl text-slate-400 mb-8">
            Master C# by typing real code. Track your progress, improve your speed, and level up your skills.
          </p>
          <div className="flex gap-4 justify-center mb-16">
            <Link
              href="/sessions"
              className="bg-purple-600 hover:bg-purple-500 px-8 py-3 rounded-lg font-semibold transition-colors"
            >
              Start Practicing
            </Link>
            <Link
              href="/leaderboard"
              className="border border-slate-700 hover:border-purple-500 hover:bg-slate-900 px-8 py-3 rounded-lg font-semibold transition-colors"
            >
              Leaderboard
            </Link>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-6 text-left">
            <div className="bg-slate-900 border border-slate-800 rounded-lg p-6">
              <div className="text-3xl mb-3">⌨️</div>
              <h3 className="text-lg font-semibold mb-2">Type Real Code</h3>
              <p className="text-slate-400 text-sm">
                Practice by typing actual C# code in an IDE-like environment. Build muscle memory for syntax and patterns.
              </p>
            </div>
            <div className="bg-slate-900 border border-slate-800 rounded-lg p-6">
              <div className="text-3xl mb-3">📊</div>
              <h3 className="text-lg font-semibold mb-2">Track Progress</h3>
              <p className="text-slate-400 text-sm">
                Monitor your WPM, accuracy, and lines written. Watch your skills improve over time with detailed stats.
              </p>
            </div>
            <div className="bg-slate-900 border border-slate-800 rounded-lg p-6">
              <div className="text-3xl mb-3">🎯</div>
              <h3 className="text-lg font-semibold mb-2">Multi-File Sessions</h3>
              <p className="text-slate-400 text-sm">
                Complete realistic coding sessions with multiple files - interfaces, classes, and programs working together.
              </p>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
