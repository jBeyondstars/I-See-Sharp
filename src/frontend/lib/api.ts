import type { Session, SessionListItem } from "@/types";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5000/api";

async function fetchApi<T>(endpoint: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...options?.headers,
    },
  });

  if (!response.ok) {
    throw new Error(`API error: ${response.status} ${response.statusText}`);
  }

  return response.json();
}

export async function getSessions(): Promise<SessionListItem[]> {
  return fetchApi<SessionListItem[]>("/sessions");
}

export async function getSessionBySlug(slug: string): Promise<Session> {
  return fetchApi<Session>(`/sessions/${slug}`);
}

export interface SubmitResultPayload {
  sessionId: string;
  totalTime: number;
  wpm: number;
  accuracy: number;
  errorCount: number;
  linesTyped: number;
  charactersTyped: number;
  filesCompleted: number;
}

export async function submitSessionResult(payload: SubmitResultPayload): Promise<void> {
  await fetchApi("/sessions/results", {
    method: "POST",
    body: JSON.stringify(payload),
  });
}
