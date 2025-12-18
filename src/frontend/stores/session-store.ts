import { create } from "zustand";
import type { Session, SessionFile } from "@/types";
import type { FileProgress } from "@/types";

interface SessionState {
  session: Session | null;
  currentFileId: string | null;
  fileProgress: Map<string, FileProgress>;
  isSessionActive: boolean;
  isSessionComplete: boolean;
  startedAt: number | null;

  setSession: (session: Session) => void;
  setCurrentFile: (fileId: string) => void;
  startSession: () => void;
  completeFile: (fileId: string) => void;
  completeSession: () => void;
  updateFileProgress: (fileId: string, progress: Partial<FileProgress>) => void;
  resetSession: () => void;
  getCurrentFile: () => SessionFile | null;
  getNextFile: () => SessionFile | null;
  getFileProgress: (fileId: string) => FileProgress | null;
}

export const useSessionStore = create<SessionState>((set, get) => ({
  session: null,
  currentFileId: null,
  fileProgress: new Map(),
  isSessionActive: false,
  isSessionComplete: false,
  startedAt: null,

  setSession: (session) => {
    const progress = new Map<string, FileProgress>();
    session.files.forEach((file, index) => {
      progress.set(file.id, {
        fileId: file.id,
        status: index === 0 ? "in_progress" : "pending",
        linesCompleted: 0,
        totalLines: file.totalLines,
        charactersTyped: 0,
        totalCharacters: file.totalCharacters,
        errors: 0,
        startedAt: index === 0 ? Date.now() : null,
        completedAt: null,
      });
    });

    set({
      session,
      currentFileId: session.files[0]?.id ?? null,
      fileProgress: progress,
      isSessionActive: false,
      isSessionComplete: false,
      startedAt: null,
    });
  },

  setCurrentFile: (fileId) => {
    const { fileProgress } = get();
    const progress = fileProgress.get(fileId);

    if (progress && progress.status === "pending") {
      const newProgress = new Map(fileProgress);
      newProgress.set(fileId, {
        ...progress,
        status: "in_progress",
        startedAt: Date.now(),
      });
      set({ currentFileId: fileId, fileProgress: newProgress });
    } else {
      set({ currentFileId: fileId });
    }
  },

  startSession: () => {
    set({
      isSessionActive: true,
      startedAt: Date.now(),
    });
  },

  completeFile: (fileId) => {
    const { fileProgress, session } = get();
    const newProgress = new Map(fileProgress);
    const progress = newProgress.get(fileId);

    if (progress) {
      newProgress.set(fileId, {
        ...progress,
        status: "completed",
        completedAt: Date.now(),
      });
    }

    const nextFile = session?.files.find(
      (f) => newProgress.get(f.id)?.status === "pending"
    );

    if (nextFile) {
      newProgress.set(nextFile.id, {
        ...newProgress.get(nextFile.id)!,
        status: "in_progress",
        startedAt: Date.now(),
      });
      set({ fileProgress: newProgress, currentFileId: nextFile.id });
    } else {
      set({ fileProgress: newProgress });
    }
  },

  completeSession: () => {
    set({ isSessionActive: false, isSessionComplete: true });
  },

  updateFileProgress: (fileId, progressUpdate) => {
    const { fileProgress } = get();
    const currentProgress = fileProgress.get(fileId);
    if (currentProgress) {
      const newProgress = new Map(fileProgress);
      newProgress.set(fileId, { ...currentProgress, ...progressUpdate });
      set({ fileProgress: newProgress });
    }
  },

  resetSession: () => {
    set({
      session: null,
      currentFileId: null,
      fileProgress: new Map(),
      isSessionActive: false,
      isSessionComplete: false,
      startedAt: null,
    });
  },

  getCurrentFile: () => {
    const { session, currentFileId } = get();
    if (!session || !currentFileId) return null;
    return session.files.find((f) => f.id === currentFileId) ?? null;
  },

  getNextFile: () => {
    const { session, currentFileId } = get();
    if (!session || !currentFileId) return null;
    const currentIndex = session.files.findIndex((f) => f.id === currentFileId);
    return session.files[currentIndex + 1] ?? null;
  },

  getFileProgress: (fileId) => {
    return get().fileProgress.get(fileId) ?? null;
  },
}));
