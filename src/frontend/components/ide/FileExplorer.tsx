"use client";

import { useSessionStore } from "@/stores";
import type { SessionFile } from "@/types";

interface FileItemProps {
  file: SessionFile;
  isActive: boolean;
  status: "pending" | "in_progress" | "completed";
  onClick: () => void;
}

function FileItem({ file, isActive, status, onClick }: FileItemProps) {
  const statusIcon =
    status === "completed" ? (
      <span className="text-green-400">●</span>
    ) : status === "in_progress" ? (
      <span className="text-purple-400">◐</span>
    ) : (
      <span className="text-slate-600">○</span>
    );

  return (
    <button
      onClick={onClick}
      className={`w-full flex items-center gap-2 px-3 py-1.5 text-sm text-left hover:bg-slate-700/50 transition-colors ${
        isActive ? "bg-slate-700/70 text-white" : "text-slate-400"
      }`}
    >
      {statusIcon}
      <svg
        className="w-4 h-4 text-purple-400 flex-shrink-0"
        fill="none"
        stroke="currentColor"
        viewBox="0 0 24 24"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth={2}
          d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"
        />
      </svg>
      <span className="truncate">{file.displayName}</span>
    </button>
  );
}

export function FileExplorer() {
  const session = useSessionStore((s) => s.session);
  const currentFileId = useSessionStore((s) => s.currentFileId);
  const fileProgress = useSessionStore((s) => s.fileProgress);
  const setCurrentFile = useSessionStore((s) => s.setCurrentFile);

  if (!session) return null;

  const groupedFiles = session.files.reduce(
    (acc, file) => {
      const parts = file.path.split("/");
      const folder = parts.length > 1 ? parts.slice(0, -1).join("/") : "root";
      if (!acc[folder]) acc[folder] = [];
      acc[folder].push(file);
      return acc;
    },
    {} as Record<string, SessionFile[]>
  );

  return (
    <aside className="w-56 bg-slate-900 border-r border-slate-700 flex flex-col">
      <div className="px-3 py-2 text-xs font-semibold text-slate-500 uppercase tracking-wider border-b border-slate-700">
        Explorer
      </div>

      <div className="flex-1 overflow-y-auto py-2">
        {Object.entries(groupedFiles).map(([folder, files]) => (
          <div key={folder}>
            {folder !== "root" && (
              <div className="flex items-center gap-1 px-3 py-1 text-xs text-slate-500">
                <svg
                  className="w-3 h-3"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M19 9l-7 7-7-7"
                  />
                </svg>
                <span>{folder}</span>
              </div>
            )}
            {files.map((file) => (
              <FileItem
                key={file.id}
                file={file}
                isActive={file.id === currentFileId}
                status={fileProgress.get(file.id)?.status ?? "pending"}
                onClick={() => setCurrentFile(file.id)}
              />
            ))}
          </div>
        ))}
      </div>

      <div className="border-t border-slate-700">
        <div className="px-3 py-2 text-xs font-semibold text-slate-500 uppercase tracking-wider">
          Instructions
        </div>
        <div className="px-3 py-2 text-sm text-slate-400 max-h-40 overflow-y-auto">
          {session.instructions}
        </div>
      </div>
    </aside>
  );
}
