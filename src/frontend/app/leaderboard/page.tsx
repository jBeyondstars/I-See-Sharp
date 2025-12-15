import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Leaderboard - I See Sharp",
  description: "See the top C# learners and their scores.",
};

export default function LeaderboardPage() {
  return (
    <main className="min-h-screen bg-gray-900 text-white">
      <div className="container mx-auto px-4 py-8">
        <h1 className="text-4xl font-bold mb-8">Leaderboard</h1>
        <p className="text-gray-400">Coming soon...</p>
      </div>
    </main>
  );
}
