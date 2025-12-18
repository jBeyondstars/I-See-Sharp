import { create } from "zustand";
import type { LiveStats, TypingError, Position } from "@/types";
import { initialLiveStats } from "@/types";

interface MetricsState {
  stats: LiveStats;
  errors: TypingError[];
  typingHistory: number[];
  lastUpdateTime: number;

  updateStats: (partial: Partial<LiveStats>) => void;
  recordKeystroke: (timestamp: number) => void;
  recordError: (error: TypingError) => void;
  calculateWpm: () => number;
  reset: () => void;
  initSession: (totalLines: number, totalFiles: number, totalCharacters: number) => void;
}

const WPM_WINDOW_MS = 30000;
const CHARS_PER_WORD = 5;

export const useMetricsStore = create<MetricsState>((set, get) => ({
  stats: initialLiveStats,
  errors: [],
  typingHistory: [],
  lastUpdateTime: 0,

  updateStats: (partial) => {
    set((state) => ({
      stats: { ...state.stats, ...partial },
      lastUpdateTime: Date.now(),
    }));
  },

  recordKeystroke: (timestamp) => {
    const { typingHistory, stats } = get();
    const now = timestamp;
    const windowStart = now - WPM_WINDOW_MS;

    const recentKeystrokes = [...typingHistory, now].filter(
      (t) => t > windowStart
    );

    const currentWpm =
      recentKeystrokes.length > 1
        ? Math.round(
            (recentKeystrokes.length / CHARS_PER_WORD) *
              (60000 / WPM_WINDOW_MS)
          )
        : 0;

    const newCharsTyped = stats.charactersTyped + 1;
    const totalCorrect = newCharsTyped - stats.totalErrors;
    const accuracy = newCharsTyped > 0
      ? Math.round((totalCorrect / newCharsTyped) * 100)
      : 100;

    set({
      typingHistory: recentKeystrokes,
      stats: {
        ...stats,
        charactersTyped: newCharsTyped,
        currentWpm,
        averageWpm: Math.round((stats.averageWpm + currentWpm) / 2) || currentWpm,
        peakWpm: Math.max(stats.peakWpm, currentWpm),
        accuracy,
        consecutiveCorrect: stats.consecutiveCorrect + 1,
      },
    });
  },

  recordError: (error) => {
    const { errors, stats } = get();
    const newCharsTyped = stats.charactersTyped + 1;
    const totalErrors = stats.totalErrors + 1;
    const totalCorrect = newCharsTyped - totalErrors;
    const accuracy = newCharsTyped > 0
      ? Math.round((totalCorrect / newCharsTyped) * 100)
      : 100;

    set({
      errors: [...errors, error],
      stats: {
        ...stats,
        charactersTyped: newCharsTyped,
        totalErrors,
        accuracy,
        consecutiveCorrect: 0,
      },
    });
  },

  calculateWpm: () => {
    const { typingHistory } = get();
    const now = Date.now();
    const windowStart = now - WPM_WINDOW_MS;
    const recentKeystrokes = typingHistory.filter((t) => t > windowStart);

    if (recentKeystrokes.length < 2) return 0;

    return Math.round(
      (recentKeystrokes.length / CHARS_PER_WORD) * (60000 / WPM_WINDOW_MS)
    );
  },

  reset: () => {
    set({
      stats: initialLiveStats,
      errors: [],
      typingHistory: [],
      lastUpdateTime: 0,
    });
  },

  initSession: (totalLines, totalFiles, totalCharacters) => {
    set({
      stats: {
        ...initialLiveStats,
        totalLines,
        totalFiles,
        totalCharacters,
      },
      errors: [],
      typingHistory: [],
      lastUpdateTime: Date.now(),
    });
  },
}));
