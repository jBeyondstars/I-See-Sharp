import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "@/app/globals.css";

const inter = Inter({
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "I See Sharp - Learn C# by Practice",
  description:
    "A gamified platform to practice and master C# syntax, methods, and best practices through interactive coding exercises.",
  keywords: ["C#", "learn", "practice", "coding", "exercises", "gamification"],
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>{children}</body>
    </html>
  );
}
