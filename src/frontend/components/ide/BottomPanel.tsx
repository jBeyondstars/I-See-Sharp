"use client";

import { useSessionStore, useMetricsStore } from "@/stores";

export function BottomPanel() {
  const session = useSessionStore((s) => s.session);
  const fileProgress = useSessionStore((s) => s.fileProgress);
  const stats = useMetricsStore((s) => s.stats);

  if (!session) return null;

  const completedFiles = Array.from(fileProgress.values()).filter(
    (p) => p.status === "completed"
  ).length;

  const progressPercent =
    stats.totalCharacters > 0
      ? Math.round((stats.charactersTyped / stats.totalCharacters) * 100)
      : 0;

  return (
    <footer className="h-8 bg-slate-900 border-t border-slate-700 flex items-center justify-between px-4 text-xs">
      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          <span className="text-green-400">●</span>
          <span className="text-slate-400">Ready</span>
        </div>

        <div className="flex items-center gap-2 text-slate-500">
          <span>C#</span>
          <span>|</span>
          <span>UTF-8</span>
        </div>
      </div>

      <div className="flex items-center gap-6">
        <div className="flex items-center gap-2">
          <span className="text-slate-500">Files:</span>
          <span className="text-slate-300">
            {completedFiles}/{session.files.length}
          </span>
        </div>

        <div className="flex items-center gap-2">
          <span className="text-slate-500">Lines:</span>
          <span className="text-slate-300">
            {stats.linesCompleted}/{stats.totalLines}
          </span>
        </div>

        <div className="flex items-center gap-2">
          <span className="text-slate-500">Errors:</span>
          <span
            className={stats.totalErrors > 0 ? "text-red-400" : "text-slate-300"}
          >
            {stats.totalErrors}
          </span>
        </div>

        <div className="flex items-center gap-2 min-w-[120px]">
          <div className="flex-1 h-1.5 bg-slate-700 rounded-full overflow-hidden">
            <div
              className="h-full bg-purple-500 transition-all duration-300"
              style={{ width: `${progressPercent}%` }}
            />
          </div>
          <span className="text-slate-400 w-8 text-right">{progressPercent}%</span>
        </div>
      </div>
    </footer>
  );
}
