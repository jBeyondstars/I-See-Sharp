export type SessionDifficulty = "Easy" | "Medium" | "Hard" | "Expert";

export type SessionCategory =
  | "Syntax"
  | "Variables"
  | "ControlFlow"
  | "Methods"
  | "Classes"
  | "Interfaces"
  | "Inheritance"
  | "Generics"
  | "Collections"
  | "Linq"
  | "AsyncAwait"
  | "ErrorHandling"
  | "Patterns"
  | "ModernCSharp"
  | "FullProject";

export interface SessionFile {
  id: string;
  sessionId: string;
  path: string;
  displayName: string;
  language: string;
  targetContent: string;
  editableRegionsJson: string | null;
  sortOrder: number;
  totalLines: number;
  totalCharacters: number;
}

export interface Session {
  id: string;
  title: string;
  slug: string;
  description: string;
  instructions: string;
  objectivesJson: string | null;
  hintsJson: string | null;
  difficulty: SessionDifficulty;
  category: SessionCategory;
  points: number;
  estimatedMinutes: number;
  targetWpm: number;
  isActive: boolean;
  isPremium: boolean;
  totalAttempts: number;
  totalCompletions: number;
  averageAccuracy: number;
  averageWpm: number;
  totalLines: number;
  totalCharacters: number;
  files: SessionFile[];
}

export interface SessionListItem {
  id: string;
  title: string;
  slug: string;
  description: string;
  difficulty: SessionDifficulty;
  category: SessionCategory;
  points: number;
  estimatedMinutes: number;
  targetWpm: number;
  totalLines: number;
  totalFiles: number;
  totalAttempts: number;
  totalCompletions: number;
  averageAccuracy: number;
}

export interface EditableRegion {
  startLine: number;
  startColumn: number;
  endLine: number;
  endColumn: number;
  hint?: string;
}

export function parseEditableRegions(json: string | null): EditableRegion[] {
  if (!json) return [];
  try {
    return JSON.parse(json);
  } catch {
    return [];
  }
}

export function parseObjectives(json: string | null): string[] {
  if (!json) return [];
  try {
    return JSON.parse(json);
  } catch {
    return [];
  }
}

export function parseHints(json: string | null): string[] {
  if (!json) return [];
  try {
    return JSON.parse(json);
  } catch {
    return [];
  }
}
