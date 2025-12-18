"use client";

import { useSessionStore, useMetricsStore } from "@/stores";
import Link from "next/link";

export function CompletionModal() {
  const session = useSessionStore((s) => s.session);
  const startedAt = useSessionStore((s) => s.startedAt);
  const stats = useMetricsStore((s) => s.stats);

  if (!session) return null;

  const totalTime = startedAt ? Math.floor((Date.now() - startedAt) / 1000) : 0;
  const minutes = Math.floor(totalTime / 60);
  const seconds = totalTime % 60;

  const baseScore = session.points;
  const accuracyBonus = stats.accuracy >= 95 ? Math.floor(baseScore * 0.2) : 0;
  const speedBonus = stats.averageWpm >= session.targetWpm ? Math.floor(baseScore * 0.15) : 0;
  const noErrorBonus = stats.totalErrors === 0 ? Math.floor(baseScore * 0.25) : 0;
  const totalScore = baseScore + accuracyBonus + speedBonus + noErrorBonus;

  return (
    <div className="fixed inset-0 bg-black/80 flex items-center justify-center z-50">
      <div className="bg-slate-900 rounded-lg border border-slate-700 p-8 max-w-md w-full mx-4 shadow-2xl">
        <div className="text-center mb-6">
          <div className="text-5xl mb-4">🎉</div>
          <h2 className="text-2xl font-bold text-white mb-2">Session Complete!</h2>
          <p className="text-slate-400">{session.title}</p>
        </div>

        <div className="space-y-4 mb-6">
          <div className="grid grid-cols-2 gap-4">
            <div className="bg-slate-800 rounded-lg p-4 text-center">
              <div className="text-3xl font-bold text-purple-400">
                {stats.averageWpm}
              </div>
              <div className="text-sm text-slate-500">Average WPM</div>
            </div>
            <div className="bg-slate-800 rounded-lg p-4 text-center">
              <div className="text-3xl font-bold text-green-400">
                {stats.accuracy}%
              </div>
              <div className="text-sm text-slate-500">Accuracy</div>
            </div>
          </div>

          <div className="bg-slate-800 rounded-lg p-4">
            <div className="flex justify-between items-center mb-3">
              <span className="text-slate-400">Time</span>
              <span className="text-white font-mono">
                {minutes}:{seconds.toString().padStart(2, "0")}
              </span>
            </div>
            <div className="flex justify-between items-center mb-3">
              <span className="text-slate-400">Lines Written</span>
              <span className="text-white">{stats.linesCompleted}</span>
            </div>
            <div className="flex justify-between items-center mb-3">
              <span className="text-slate-400">Characters Typed</span>
              <span className="text-white">{stats.charactersTyped}</span>
            </div>
            <div className="flex justify-between items-center">
              <span className="text-slate-400">Errors</span>
              <span className={stats.totalErrors > 0 ? "text-red-400" : "text-green-400"}>
                {stats.totalErrors}
              </span>
            </div>
          </div>

          <div className="bg-slate-800 rounded-lg p-4">
            <div className="text-sm text-slate-500 mb-3">Score Breakdown</div>
            <div className="space-y-2 text-sm">
              <div className="flex justify-between">
                <span className="text-slate-400">Base Score</span>
                <span className="text-white">+{baseScore}</span>
              </div>
              {accuracyBonus > 0 && (
                <div className="flex justify-between">
                  <span className="text-green-400">Accuracy Bonus (95%+)</span>
                  <span className="text-green-400">+{accuracyBonus}</span>
                </div>
              )}
              {speedBonus > 0 && (
                <div className="flex justify-between">
                  <span className="text-purple-400">Speed Bonus</span>
                  <span className="text-purple-400">+{speedBonus}</span>
                </div>
              )}
              {noErrorBonus > 0 && (
                <div className="flex justify-between">
                  <span className="text-yellow-400">Perfect Run!</span>
                  <span className="text-yellow-400">+{noErrorBonus}</span>
                </div>
              )}
              <div className="border-t border-slate-700 pt-2 mt-2 flex justify-between font-bold">
                <span className="text-white">Total Score</span>
                <span className="text-purple-400">{totalScore}</span>
              </div>
            </div>
          </div>
        </div>

        <div className="flex gap-3">
          <Link
            href="/sessions"
            className="flex-1 px-4 py-2 bg-slate-700 text-white rounded-lg text-center hover:bg-slate-600 transition-colors"
          >
            Back to Sessions
          </Link>
          <button
            onClick={() => window.location.reload()}
            className="flex-1 px-4 py-2 bg-purple-600 text-white rounded-lg hover:bg-purple-500 transition-colors"
          >
            Try Again
          </button>
        </div>
      </div>
    </div>
  );
}
