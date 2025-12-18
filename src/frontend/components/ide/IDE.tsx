"use client";

import { TopBar } from "./TopBar";
import { FileExplorer } from "./FileExplorer";
import { TabBar } from "./TabBar";
import { BottomPanel } from "./BottomPanel";
import { GhostTextEditor } from "./GhostTextEditor";
import { CompletionModal } from "./CompletionModal";
import { useSessionStore } from "@/stores";

export function IDE() {
  const isSessionComplete = useSessionStore((s) => s.isSessionComplete);

  return (
    <div className="h-screen w-screen flex flex-col bg-slate-950 overflow-hidden">
      <TopBar />

      <div className="flex-1 flex overflow-hidden">
        <FileExplorer />

        <main className="flex-1 flex flex-col overflow-hidden">
          <TabBar />

          <div className="flex-1 overflow-hidden">
            <GhostTextEditor />
          </div>
        </main>
      </div>

      <BottomPanel />

      {isSessionComplete && <CompletionModal />}
    </div>
  );
}
