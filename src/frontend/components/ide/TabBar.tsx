"use client";

import { useSessionStore } from "@/stores";

export function TabBar() {
  const session = useSessionStore((s) => s.session);
  const currentFileId = useSessionStore((s) => s.currentFileId);
  const fileProgress = useSessionStore((s) => s.fileProgress);
  const setCurrentFile = useSessionStore((s) => s.setCurrentFile);

  if (!session) return null;

  return (
    <div className="h-9 bg-slate-800 border-b border-slate-700 flex items-end overflow-x-auto">
      {session.files.map((file) => {
        const isActive = file.id === currentFileId;
        const progress = fileProgress.get(file.id);
        const status = progress?.status ?? "pending";

        return (
          <button
            key={file.id}
            onClick={() => setCurrentFile(file.id)}
            className={`flex items-center gap-2 px-4 py-1.5 text-sm border-r border-slate-700 transition-colors ${
              isActive
                ? "bg-slate-900 text-white border-t-2 border-t-purple-500"
                : "bg-slate-800 text-slate-400 hover:bg-slate-700/50 border-t-2 border-t-transparent"
            }`}
          >
            <span
              className={`w-2 h-2 rounded-full ${
                status === "completed"
                  ? "bg-green-400"
                  : status === "in_progress"
                    ? "bg-purple-400"
                    : "bg-slate-600"
              }`}
            />
            <span>{file.displayName}</span>
          </button>
        );
      })}
    </div>
  );
}
