import type { Metadata } from "next";
import Link from "next/link";

export const metadata: Metadata = {
  title: "Leaderboard | I See Sharp",
  description: "See the top C# learners and their scores.",
};

interface LeaderboardEntry {
  rank: number;
  username: string;
  totalScore: number;
  sessionsCompleted: number;
  averageWpm: number;
  averageAccuracy: number;
}

const mockLeaderboard: LeaderboardEntry[] = [
  { rank: 1, username: "CSharpMaster", totalScore: 2450, sessionsCompleted: 42, averageWpm: 65, averageAccuracy: 97 },
  { rank: 2, username: "DotNetDev", totalScore: 2180, sessionsCompleted: 38, averageWpm: 58, averageAccuracy: 95 },
  { rank: 3, username: "LinqLord", totalScore: 1920, sessionsCompleted: 35, averageWpm: 52, averageAccuracy: 94 },
  { rank: 4, username: "AsyncAce", totalScore: 1750, sessionsCompleted: 31, averageWpm: 48, averageAccuracy: 92 },
  { rank: 5, username: "PatternPro", totalScore: 1580, sessionsCompleted: 28, averageWpm: 45, averageAccuracy: 91 },
  { rank: 6, username: "GenericGuru", totalScore: 1420, sessionsCompleted: 25, averageWpm: 42, averageAccuracy: 90 },
  { rank: 7, username: "InterfaceIvan", totalScore: 1290, sessionsCompleted: 23, averageWpm: 40, averageAccuracy: 89 },
  { rank: 8, username: "ClassyCoder", totalScore: 1150, sessionsCompleted: 21, averageWpm: 38, averageAccuracy: 88 },
  { rank: 9, username: "MethodMaven", totalScore: 980, sessionsCompleted: 18, averageWpm: 35, averageAccuracy: 87 },
  { rank: 10, username: "NewbieDev", totalScore: 820, sessionsCompleted: 15, averageWpm: 32, averageAccuracy: 85 },
];

function getRankStyle(rank: number) {
  if (rank === 1) return "text-yellow-400";
  if (rank === 2) return "text-slate-300";
  if (rank === 3) return "text-amber-600";
  return "text-slate-500";
}

function getRankBadge(rank: number) {
  if (rank === 1) return "🥇";
  if (rank === 2) return "🥈";
  if (rank === 3) return "🥉";
  return rank.toString();
}

export default function LeaderboardPage() {
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
          <h1 className="text-4xl font-bold text-white mb-2">Leaderboard</h1>
          <p className="text-slate-400">Top C# practitioners ranked by total score</p>
        </div>

        <div className="bg-slate-900 border border-slate-800 rounded-lg overflow-hidden">
          <table className="w-full">
            <thead>
              <tr className="border-b border-slate-800">
                <th className="px-6 py-4 text-left text-xs font-semibold text-slate-500 uppercase tracking-wider">Rank</th>
                <th className="px-6 py-4 text-left text-xs font-semibold text-slate-500 uppercase tracking-wider">User</th>
                <th className="px-6 py-4 text-right text-xs font-semibold text-slate-500 uppercase tracking-wider">Score</th>
                <th className="px-6 py-4 text-right text-xs font-semibold text-slate-500 uppercase tracking-wider hidden sm:table-cell">Sessions</th>
                <th className="px-6 py-4 text-right text-xs font-semibold text-slate-500 uppercase tracking-wider hidden md:table-cell">Avg WPM</th>
                <th className="px-6 py-4 text-right text-xs font-semibold text-slate-500 uppercase tracking-wider hidden md:table-cell">Accuracy</th>
              </tr>
            </thead>
            <tbody>
              {mockLeaderboard.map((entry) => (
                <tr
                  key={entry.rank}
                  className="border-b border-slate-800/50 hover:bg-slate-800/30 transition-colors"
                >
                  <td className="px-6 py-4">
                    <span className={`text-lg font-bold ${getRankStyle(entry.rank)}`}>
                      {getRankBadge(entry.rank)}
                    </span>
                  </td>
                  <td className="px-6 py-4">
                    <span className="text-white font-medium">{entry.username}</span>
                  </td>
                  <td className="px-6 py-4 text-right">
                    <span className="text-purple-400 font-semibold">{entry.totalScore.toLocaleString()}</span>
                  </td>
                  <td className="px-6 py-4 text-right hidden sm:table-cell">
                    <span className="text-slate-400">{entry.sessionsCompleted}</span>
                  </td>
                  <td className="px-6 py-4 text-right hidden md:table-cell">
                    <span className="text-slate-400">{entry.averageWpm}</span>
                  </td>
                  <td className="px-6 py-4 text-right hidden md:table-cell">
                    <span className={entry.averageAccuracy >= 95 ? "text-green-400" : "text-slate-400"}>
                      {entry.averageAccuracy}%
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </main>
  );
}
