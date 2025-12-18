"use client";

import { useSessionStore, useMetricsStore } from "@/stores";
import { useEffect, useState } from "react";

function formatTime(seconds: number): string {
  const mins = Math.floor(seconds / 60);
  const secs = seconds % 60;
  return `${mins.toString().padStart(2, "0")}:${secs.toString().padStart(2, "0")}`;
}

export function TopBar() {
  const session = useSessionStore((s) => s.session);
  const isSessionActive = useSessionStore((s) => s.isSessionActive);
  const startedAt = useSessionStore((s) => s.startedAt);
  const stats = useMetricsStore((s) => s.stats);

  const [elapsed, setElapsed] = useState(0);

  useEffect(() => {
    if (!isSessionActive || !startedAt) {
      setElapsed(0);
      return;
    }

    const interval = setInterval(() => {
      setElapsed(Math.floor((Date.now() - startedAt) / 1000));
    }, 1000);

    return () => clearInterval(interval);
  }, [isSessionActive, startedAt]);

  return (
    <header className="h-12 bg-slate-900 border-b border-slate-700 flex items-center justify-between px-4">
      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          <span className="text-purple-500 font-bold text-lg">I#</span>
          <span className="text-slate-400 text-sm hidden sm:inline">I See Sharp</span>
        </div>

        {session && (
          <div className="flex items-center gap-2 pl-4 border-l border-slate-700">
            <span className="text-slate-300 font-medium">{session.title}</span>
            <span
              className={`text-xs px-2 py-0.5 rounded ${
                session.difficulty === "Easy"
                  ? "bg-green-500/20 text-green-400"
                  : session.difficulty === "Medium"
                    ? "bg-yellow-500/20 text-yellow-400"
                    : session.difficulty === "Hard"
                      ? "bg-orange-500/20 text-orange-400"
                      : "bg-red-500/20 text-red-400"
              }`}
            >
              {session.difficulty}
            </span>
          </div>
        )}
      </div>

      <div className="flex items-center gap-6">
        <div className="flex items-center gap-2 text-slate-400">
          <svg
            className="w-4 h-4"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z"
            />
          </svg>
          <span className="font-mono text-sm">{formatTime(elapsed)}</span>
        </div>

        <div className="flex items-center gap-2">
          <span className="text-slate-500 text-sm">WPM</span>
          <span className="font-mono text-sm text-purple-400 font-medium">
            {stats.currentWpm}
          </span>
        </div>

        <div className="flex items-center gap-2">
          <span className="text-slate-500 text-sm">Accuracy</span>
          <span
            className={`font-mono text-sm font-medium ${
              stats.accuracy >= 95
                ? "text-green-400"
                : stats.accuracy >= 85
                  ? "text-yellow-400"
                  : "text-red-400"
            }`}
          >
            {stats.accuracy}%
          </span>
        </div>

        <div className="flex items-center gap-2 text-slate-400">
          <span className="text-sm">
            {stats.linesCompleted}/{stats.totalLines} lines
          </span>
        </div>
      </div>
    </header>
  );
}
