export interface Position {
  line: number;
  column: number;
}

export interface TypingError {
  position: Position;
  expected: string;
  actual: string;
  timestamp: number;
}

export interface LiveStats {
  elapsedTime: number;
  estimatedRemaining: number;
  currentWpm: number;
  averageWpm: number;
  peakWpm: number;
  accuracy: number;
  totalErrors: number;
  consecutiveCorrect: number;
  linesCompleted: number;
  totalLines: number;
  filesCompleted: number;
  totalFiles: number;
  charactersTyped: number;
  totalCharacters: number;
}

export interface FileProgress {
  fileId: string;
  status: "pending" | "in_progress" | "completed";
  linesCompleted: number;
  totalLines: number;
  charactersTyped: number;
  totalCharacters: number;
  errors: number;
  startedAt: number | null;
  completedAt: number | null;
}

export interface SessionResult {
  sessionId: string;
  totalTime: number;
  finalWpm: number;
  finalAccuracy: number;
  totalErrors: number;
  linesTyped: number;
  charactersTyped: number;
  filesCompleted: number;
  baseScore: number;
  bonuses: Bonus[];
  totalScore: number;
  xpEarned: number;
  personalBest: boolean;
}

export interface Bonus {
  type: "speed" | "accuracy" | "streak" | "no_errors" | "first_try";
  label: string;
  points: number;
}

export const initialLiveStats: LiveStats = {
  elapsedTime: 0,
  estimatedRemaining: 0,
  currentWpm: 0,
  averageWpm: 0,
  peakWpm: 0,
  accuracy: 100,
  totalErrors: 0,
  consecutiveCorrect: 0,
  linesCompleted: 0,
  totalLines: 0,
  filesCompleted: 0,
  totalFiles: 0,
  charactersTyped: 0,
  totalCharacters: 0,
};
