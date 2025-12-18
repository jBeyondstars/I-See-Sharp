"use client";

import { useEffect, useRef, useCallback, useState } from "react";
import { useSessionStore, useMetricsStore } from "@/stores";

interface Line {
  content: string;
  typed: string;
  isComplete: boolean;
}

export function GhostTextEditor() {
  const containerRef = useRef<HTMLDivElement>(null);
  const session = useSessionStore((s) => s.session);
  const currentFileId = useSessionStore((s) => s.currentFileId);
  const getCurrentFile = useSessionStore((s) => s.getCurrentFile);
  const isSessionActive = useSessionStore((s) => s.isSessionActive);
  const startSession = useSessionStore((s) => s.startSession);
  const completeFile = useSessionStore((s) => s.completeFile);
  const completeSession = useSessionStore((s) => s.completeSession);
  const getNextFile = useSessionStore((s) => s.getNextFile);
  const updateFileProgress = useSessionStore((s) => s.updateFileProgress);

  const recordKeystroke = useMetricsStore((s) => s.recordKeystroke);
  const recordError = useMetricsStore((s) => s.recordError);
  const updateStats = useMetricsStore((s) => s.updateStats);
  const initSession = useMetricsStore((s) => s.initSession);

  const [lines, setLines] = useState<Line[]>([]);
  const [currentLine, setCurrentLine] = useState(0);
  const [cursorPosition, setCursorPosition] = useState(0);
  const [hasStarted, setHasStarted] = useState(false);

  const currentFile = getCurrentFile();

  useEffect(() => {
    if (!currentFile) return;

    const targetLines = currentFile.targetContent.split("\n");
    setLines(
      targetLines.map((content) => ({
        content,
        typed: "",
        isComplete: false,
      }))
    );
    setCurrentLine(0);
    setCursorPosition(0);
  }, [currentFile, currentFileId]);

  useEffect(() => {
    if (session && !hasStarted) {
      initSession(
        session.totalLines,
        session.files.length,
        session.totalCharacters
      );
    }
  }, [session, hasStarted, initSession]);

  const handleKeyDown = useCallback(
    (e: KeyboardEvent) => {
      if (!currentFile || lines.length === 0) return;

      if (e.ctrlKey || e.altKey || e.metaKey) return;
      if (["Shift", "Control", "Alt", "Meta", "CapsLock", "Escape"].includes(e.key)) return;

      e.preventDefault();

      if (!isSessionActive && !hasStarted) {
        startSession();
        setHasStarted(true);
      }

      const line = lines[currentLine];
      if (!line) return;

      const expectedChar = line.content[cursorPosition];
      const isLastCharOfLine = cursorPosition >= line.content.length;
      const isLastLine = currentLine >= lines.length - 1;

      if (e.key === "Enter") {
        if (isLastCharOfLine || line.content.trim() === "") {
          if (isLastLine) {
            handleFileComplete();
          } else {
            moveToNextLine();
          }
        } else {
          recordError({
            position: { line: currentLine, column: cursorPosition },
            expected: expectedChar || "newline",
            actual: "Enter",
            timestamp: Date.now(),
          });
        }
        return;
      }

      if (e.key === "Tab") {
        const spacesToType = "    ";
        const expectedSpaces = line.content.slice(
          cursorPosition,
          cursorPosition + 4
        );

        if (expectedSpaces === spacesToType || expectedSpaces.startsWith("    ")) {
          const newTyped = line.typed + spacesToType.slice(0, expectedSpaces.length);
          updateLine(currentLine, newTyped);
          setCursorPosition((p) => p + expectedSpaces.length);
          for (let i = 0; i < expectedSpaces.length; i++) {
            recordKeystroke(Date.now());
          }
        }
        return;
      }

      if (e.key === "Backspace") {
        if (cursorPosition > 0) {
          const newTyped = line.typed.slice(0, -1);
          updateLine(currentLine, newTyped);
          setCursorPosition((p) => p - 1);
        }
        return;
      }

      if (e.key.length === 1) {
        if (isLastCharOfLine) {
          return;
        }

        if (e.key === expectedChar) {
          const newTyped = line.typed + e.key;
          updateLine(currentLine, newTyped);
          setCursorPosition((p) => p + 1);
          recordKeystroke(Date.now());

          if (cursorPosition + 1 >= line.content.length) {
            updateLine(currentLine, newTyped, true);
            updateStats({ linesCompleted: currentLine + 1 });
          }
        } else {
          recordError({
            position: { line: currentLine, column: cursorPosition },
            expected: expectedChar,
            actual: e.key,
            timestamp: Date.now(),
          });
        }
      }
    },
    [
      currentFile,
      lines,
      currentLine,
      cursorPosition,
      isSessionActive,
      hasStarted,
      startSession,
      recordKeystroke,
      recordError,
      updateStats,
    ]
  );

  const updateLine = (index: number, typed: string, isComplete = false) => {
    setLines((prev) =>
      prev.map((line, i) =>
        i === index ? { ...line, typed, isComplete } : line
      )
    );
  };

  const moveToNextLine = () => {
    const completedLines = currentLine + 1;
    updateStats({ linesCompleted: completedLines });

    if (currentLine < lines.length - 1) {
      setCurrentLine((l) => l + 1);
      setCursorPosition(0);
      updateLine(currentLine, lines[currentLine].typed, true);
    }
  };

  const handleFileComplete = () => {
    if (!currentFile || !currentFileId) return;

    completeFile(currentFileId);

    const nextFile = getNextFile();
    if (!nextFile) {
      completeSession();
    }
  };

  useEffect(() => {
    const container = containerRef.current;
    if (!container) return;

    container.focus();
    container.addEventListener("keydown", handleKeyDown);
    return () => container.removeEventListener("keydown", handleKeyDown);
  }, [handleKeyDown]);

  if (!currentFile) {
    return (
      <div className="h-full flex items-center justify-center text-slate-500">
        No file selected
      </div>
    );
  }

  return (
    <div
      ref={containerRef}
      tabIndex={0}
      className="h-full bg-slate-950 overflow-auto focus:outline-none font-mono text-sm"
    >
      {!hasStarted && (
        <div className="absolute inset-0 bg-slate-950/90 flex items-center justify-center z-10">
          <div className="text-center">
            <p className="text-slate-400 mb-2">Press any key to start</p>
            <p className="text-slate-600 text-sm">
              Type the code exactly as shown
            </p>
          </div>
        </div>
      )}

      <div className="p-4">
        {lines.map((line, lineIndex) => (
          <div
            key={lineIndex}
            className={`flex leading-6 ${
              lineIndex === currentLine ? "bg-slate-900/50" : ""
            }`}
          >
            <span className="w-12 text-right pr-4 text-slate-600 select-none">
              {lineIndex + 1}
            </span>
            <div className="flex-1 relative">
              <span className="text-slate-600 whitespace-pre">
                {line.content}
              </span>

              <span className="absolute left-0 top-0 whitespace-pre">
                {line.typed.split("").map((char, charIndex) => {
                  const expected = line.content[charIndex];
                  const isCorrect = char === expected;
                  return (
                    <span
                      key={charIndex}
                      className={isCorrect ? "text-slate-200" : "text-red-500 bg-red-500/20"}
                    >
                      {char}
                    </span>
                  );
                })}
                {lineIndex === currentLine && (
                  <span className="bg-purple-500 animate-pulse w-0.5 inline-block h-5 align-middle" />
                )}
              </span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
