"use client";

import dynamic from "next/dynamic";

const CodeEditor = dynamic(() => import("@/components/code-editor"), {
  ssr: false,
  loading: () => (
    <div className="h-96 bg-gray-800 animate-pulse rounded-lg" />
  ),
});

export default function PracticePage() {
  return (
    <main className="min-h-screen bg-gray-900 text-white">
      <div className="container mx-auto px-4 py-8">
        <h1 className="text-4xl font-bold mb-8">Practice</h1>
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <div>
            <h2 className="text-xl font-semibold mb-4">Instructions</h2>
            <div className="bg-gray-800 rounded-lg p-4">
              <p className="text-gray-300">
                Write a method that returns the sum of two integers.
              </p>
            </div>
          </div>
          <div>
            <h2 className="text-xl font-semibold mb-4">Your Code</h2>
            <CodeEditor />
          </div>
        </div>
      </div>
    </main>
  );
}
