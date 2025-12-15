import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Exercises - I See Sharp",
  description: "Browse C# coding exercises by category and difficulty level.",
};

export default function ExercisesPage() {
  return (
    <main className="min-h-screen bg-gray-900 text-white">
      <div className="container mx-auto px-4 py-8">
        <h1 className="text-4xl font-bold mb-8">Exercises</h1>
        <p className="text-gray-400">Coming soon...</p>
      </div>
    </main>
  );
}
